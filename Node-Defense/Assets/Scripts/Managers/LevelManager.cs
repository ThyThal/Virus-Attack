using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] public int nodeAmount;
    public Grafo nodesGraph;
    public GameNode nodeInternet;
    public GameNode nodeServer;
    public WaveManager waveManager;
    public bool isServerInfected;
    private const int NODES_FOR_Y = 3;
    private const int NODE_POSITION_DIFERENCESS = 100;
    public Transform nodesParent;
    private bool gameFinished;
    public Dictionary<string, string> dictionary;
    public List<GameObject> nodesPrefabs;
    static public LevelManager instance;

    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        GameManager.Instance.score = 0;
        waveManager = GetComponent<WaveManager>();
        nodesGraph = new Grafo();
        int i = 0;
        var internet = Instantiate(nodesPrefabs[1], nodesParent);
        Vector2 position = new Vector2(-300, 0);
        internet.transform.localPosition = position;
        internet.GetComponent<GameNode>().Vertex = NodeManager.instance.vertex[0];
        nodeInternet = internet.GetComponent<GameNode>();
        nodesGraph.AddVertex(nodeInternet.Vertex);
        i++;
        int j = NODES_FOR_Y;
        for (; i < NodeManager.instance.vertex.Length-1; i++)
        {
            j++;
            if (j >= NODES_FOR_Y)
            {
                position.x += NODE_POSITION_DIFERENCESS;
                j = 0;
            }
            position.y = Random.Range(-200, 200);
            var basicNode = Instantiate(nodesPrefabs[0], nodesParent);
            basicNode.transform.localPosition = position;
            basicNode.GetComponent<GameNode>().Vertex = NodeManager.instance.vertex[i];
            nodesGraph.AddVertex(basicNode.GetComponent<GameNode>().Vertex);
        }
        i++;
        position.x += NODE_POSITION_DIFERENCESS;
        position.y = 0;
        var server = Instantiate(nodesPrefabs[2], nodesParent);
        server.transform.localPosition = position;
        server.GetComponent<GameNode>().Vertex = NodeManager.instance.vertex[i];
        nodeServer = server.GetComponent<GameNode>();
        nodesGraph.AddVertex(nodeServer.Vertex);
    }

    public void SpawnVirus(GameObject enemyToSpawn)
    {
        Instantiate(enemyToSpawn, nodeInternet.transform);
        //enemyToSpawn.transform.position = nodeInternet.transform.position;
        var gameNode = nodeInternet.GetComponent<GameNode>();
        gameNode.virus.Add(enemyToSpawn);
    }

    public void RemoveVirus(GameObject virus)
    {
        nodeInternet.GetComponent<GameNode>().virus.Remove(virus);
    }

    private void Update()
    {
        if (nodeServer.GetComponent<GameNode>().isInfected == true)
        {
            isServerInfected = true;
            GameManager.Instance.GameOver();
        }

        /*if (!gameFinished && )
        {
            if (nodeServer.GetComponent<GameNode>().isInfected == false)
            {
                gameFinished = true;
                GameOver();
            }
        }*/
    }

    public void GameOver() // <====={ SCENE LOSE }
    {
        GameManager.Instance.GameOver();
    }

    public void Win() // <====={ SCENE WIN }
    {
        GameManager.Instance.Win();
    }
}
