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
    private int treePosition;
    [SerializeField] private PowerUp powerUp;
    [SerializeField] GameNodeType Type;
    [SerializeField] int life;
    [SerializeField] public bool isInfected;
    [SerializeField] public List<GameObject> virus;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private float timer;
    [SerializeField] private float originalTimer;
    [SerializeField] private int damage;
    [SerializeField] private GameObject targetVirus;
    [SerializeField] private Transform render;
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private Vector3 vector;

    public int TreePosition
    {
        get
        {
            return treePosition;
        }

        set
        {
            treePosition = value;
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
        render = GetComponentInChildren<Transform>();
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
            vector = targetVirus.transform.position - render.position;
            Debug.Log(targetVirus.transform.position+" "+render.position);
            if (Type != GameNodeType.Internet)
                lineRenderer.SetPosition(1, vector);
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

        v.GetDamage(damage);
    }

    public Node NextNode()
    {
        if (Type == GameNodeType.Server) return null;
        Node nextNode = LevelManager.instance.nodes.Find(treePosition++); 
        /*Node actualNode = LevelManager.instance.nodes.Find(treePosition);
        bool Boolean = (Random.value > 0.5f);*/
        /*if (Boolean)
        {
            nextNode = actualNode.leftNode != null ? actualNode.leftNode : actualNode.rightNode;
        }
        else
        {
            nextNode = actualNode.rightNode != null ? actualNode.rightNode : actualNode.leftNode;
        }*/
        return nextNode;
    }

    public void GetDamage(int damage)
    {
        if (life > 0)
            life -= damage;
        else if(life <= 0)
            HasDied();
    }

    public void HasDied()
    {
        isInfected = true;

        spriteRenderer.color = new Color(1f, 0.47f, 0.47f);
    }
}
