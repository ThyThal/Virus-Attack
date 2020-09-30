using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public BinaryTree nodes;
    public GameObject nodeInternet;
    public GameObject nodeServer;
    private const int NODE_POSITION_DIFERENCESS = 2;
    public Transform nodesParent;
    public Dictionary<string, string> dictionary;
    public List<GameObject> nodesPrefabs;
    static public LevelManager instance;

    void Awake()
    {
        nodes = new BinaryTree();
        int i = 0;
        var internet = Instantiate(nodesPrefabs[1], nodesParent);
        Vector2 position = new Vector2(-6, 0);
        ((GameObject)internet).transform.localPosition = position;
        nodeInternet = internet;
        nodes.Add(i, internet.GetComponent<GameNode>());
        for (; i < 1; i++)
        {
            position.x += NODE_POSITION_DIFERENCESS;
            position.y = Random.Range(-5, 5);
            var basicNode = Instantiate(nodesPrefabs[0], nodesParent);
            ((GameObject)basicNode).transform.localPosition = position;
            nodes.Add(i, basicNode.GetComponent<GameNode>());
        }
        i++;
        position.x += NODE_POSITION_DIFERENCESS;
        position.y = 0;
        var server = Instantiate(nodesPrefabs[2], nodesParent);
        ((GameObject)server).transform.localPosition = position;
        nodeServer = server;
        nodes.Add(i, server.GetComponent<GameNode>());

    }

    public void SpawnVirus(GameObject enemyToSpawn)
    {
        Instantiate(enemyToSpawn, nodeInternet.transform);
        var gameNode = (GameNode) nodeInternet.GetComponent<GameNode>();
        gameNode.virus.Add(enemyToSpawn.GetComponent<Virus>());
    }

    private void Update()
    {
        Debug.Log(nodeInternet.GetComponent<GameNode>().virus);
    }

    public void Win()
    {

    }
       
    public void GameOver()
    {

    }
}
