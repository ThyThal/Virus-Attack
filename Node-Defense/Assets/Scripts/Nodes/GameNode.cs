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
    private int vertex;
    private PowerUp powerUp;
    [SerializeField] public GameNodeType Type;
    [SerializeField] int life;
    [SerializeField] public bool isInfected;
    [SerializeField] public List<GameObject> virus;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private float timer;
    [SerializeField] private float originalTimer;
    [SerializeField] private int damage;
    [SerializeField] private GameObject targetVirus;

    [Header("Line Renderer")]
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private Vector3 vectorToTarget;
    [SerializeField] private Vector3 targetPos;
    [SerializeField] private Vector3 nodePos;
    [SerializeField] private float scale;

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
            lineRenderer = GetComponent<LineRenderer>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        originalTimer = timer;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if(Type != GameNodeType.Internet)
            lineRenderer.SetPosition(1, Vector3.zero);
        targetVirus = GameObject.FindGameObjectWithTag("Virus");
        if (targetVirus != null && !isInfected)
        {
            targetPos = targetVirus.transform.position;
            nodePos = transform.position;
            vectorToTarget = (targetPos - nodePos) * scale;
            if (Type != GameNodeType.Internet)
                lineRenderer.SetPosition(1, vectorToTarget);
            if (timer <= 0)
            {
                Attack(targetVirus.GetComponent<Virus>());
                timer = originalTimer;
            }
        }
        /*foreach (GameObject v in virus)
        {
            targetVirus = v;
            Virus scriptVirus = v.GetComponent<Virus>();

            if (targetVirus.target == null)
            {
                var nextNode = NextNode().gameNode;
                targetVirus.target = nextNode;
            }

            if (targetVirus != null)
            {
                if (timer <= 0)
                {
                    //Attack(targetVirus);
                    timer = 2;
                }
            }


        }*/
    }

    private void Attack(Virus v)
    {
        int damageDone = damage;
        if (powerUp != null && powerUp.type == PowerUpType.Antivirus)
            damage = damage * 2;
        v.GetDamage(damageDone);
    }

    public GameNode NextNode()
    {
        //Node nextNode = LevelManager.instance.FindNext(); 
        return null;
    }

    public void GetDamage(int damage)
    {
        if (life > 0)
        {
            if (powerUp != null && powerUp.type == PowerUpType.FireWall)
                damage = damage / 2;
            life -= damage;
        }

        if (life <= 0)
        {
            HasDied();
        }
    }

    public void HasDied()
    {
        isInfected = true;
        if (powerUp != null)
        {
            Destroy(powerUp.gameObject);
            powerUp = null;
        }
        GameManager.Instance.score -= 17;
        this.GetComponent<Button>().interactable = false;
        spriteRenderer.color = new Color(1f, 0.47f, 0.47f);
    }
}
