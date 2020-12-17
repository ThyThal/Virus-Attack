using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum GameNodeType
{
    BasicNode = 1,
    Internet = 2,
    Server = 3
};

public class GameNode : MonoBehaviour, IGameNode
{
    [Header("Combat Stats")]
    [SerializeField] int life;
    [SerializeField] private float resistance = 1;
    [SerializeField] private int damage;
    [SerializeField] private int damageMultiplier = 2;
    [SerializeField] private float attackTimer;
    [SerializeField] private int score;
    [SerializeField] private ProgressBar healthBar;

    [Header("Extras")]
    [SerializeField] public GameNodeType Type;
    [SerializeField] public bool isInfected;
    [SerializeField] private float originalAttackTimer;
    [SerializeField] private GameObject targetVirus;
    [SerializeField] private Virus currentTarget;

    [Header("Line Renderer")]
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private Vector3 vectorToTarget;
    [SerializeField] private Vector3 targetPos;
    [SerializeField] private Vector3 nodePos;
    [SerializeField] private float scale;
    [SerializeField] private float originalDamage;
    [SerializeField] private PowerUp powerUp;
    [SerializeField] public int powerUpLevel;

    [Header("Audio")]
    [SerializeField] private AudioSource _sourceAttack;
    [SerializeField] private AudioSource _sourceDie;
    [SerializeField] public AudioSource _sourceUpgrade;
    [SerializeField] public AudioClip _upgradeAudio;


    [Header("Edges")]
    [SerializeField] public List<GameObject> edgesRenderers;

    private int vertex;
    private SpriteRenderer spriteRenderer;

    public int Vertex
    {
        get
        {
            return vertex;
        }

        set
        {
            vertex = value;
        }
    }

    public PowerUp PowerUp
    {
        get
        {
            return powerUp;
        }

        set
        {
            powerUp = value;
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        if (Type != GameNodeType.Internet)
        {
            lineRenderer = GetComponent<LineRenderer>();
            healthBar.maximum = life;
            healthBar.current = life;
        }
        else
        {
            ChangeEdges();
            healthBar.current = 0;
        }

        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        var delay = Random.Range(0f, 1f);
        originalDamage = damage;
        originalAttackTimer = attackTimer + delay;
    }

    // Update is called once per frame
    void Update()
    {        
        if(Type != GameNodeType.Internet)
        {
            lineRenderer.SetPosition(1, Vector3.zero);
        }

        FindTargetVirus();
        
        if (currentTarget != null && !isInfected)
        {
            attackTimer -= Time.deltaTime;
            targetPos = currentTarget.transform.position;
            nodePos = transform.position;
            vectorToTarget = (targetPos - nodePos) * scale;

            if (Type != GameNodeType.Internet)
            {
                lineRenderer.SetPosition(1, vectorToTarget);
            }

            if (attackTimer <= 0 && currentTarget.hasSpawned)
            {
                attackTimer = originalAttackTimer;
                Attack(currentTarget);
            }
        }
    }

    private void FindTargetVirus()
    {
        if (currentTarget == null)
        {
            targetVirus = GameObject.FindGameObjectWithTag("Virus");

            if ((targetVirus != null) && (targetVirus.GetComponent<Virus>().hasSpawned == true))
            {
                currentTarget = targetVirus.GetComponent<Virus>();
            }
        }
    }

    private void Attack(Virus v)
    {
        if (powerUp != null)
        {
            if (powerUp.type == PowerUpType.Antivirus)
            {
                damage = (int)(originalDamage * (damageMultiplier + powerUpLevel/2));
            }
        }

        _sourceAttack.Play();
        v.GetDamage(damage);
    }

    public void GetDamage(int dmg)
    {
        if (life > 0)
        {
            var ogDamage = dmg;
            dmg = (int)(dmg / (resistance + (powerUpLevel/3))); // GET DAMAGE FORMULA

            if (powerUp != null && powerUp.type == PowerUpType.FireWall)
                dmg = dmg / 2;

            life -= dmg;
            healthBar.current = life;
        }

        if (life <= 0)
        {
            HasDied();
        }
    }

    public void HasDied()
    {
        isInfected = true;
        ChangeEdges();
        if (powerUp != null)
        {
            Destroy(powerUp.gameObject);
            powerUp = null;
        }
        GameManager.Instance.ScoreUpdate(score);
        this.GetComponent<Button>().interactable = false;
        spriteRenderer.color = new Color(1f, 0.47f, 0.47f);
        _sourceDie.Play();
        healthBar.transform.GetComponent<Image>().color = new Color(1f, 0.47f, 0.47f);
    }

    private void ChangeEdges()
    {
        LineRenderer edge;
        for (int i = 0; i < edgesRenderers.Count; i++)
        {
            edge = edgesRenderers[i].GetComponent<LineRenderer>();
            edge.startColor = (Color.red);
            edge.endColor = (Color.magenta);
        }
    }
}
