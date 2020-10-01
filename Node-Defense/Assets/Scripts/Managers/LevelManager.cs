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
        instance = this;
        nodes = new BinaryTree();
        int i = 0;
        var internet = Instantiate(nodesPrefabs[1], nodesParent);
        Vector2 position = new Vector2(-6, 0);
        ((GameObject)internet).transform.localPosition = position;
        internet.GetComponent<GameNode>().TreePosition = i;
        nodeInternet = internet;
        nodes.Add(i, (GameObject)internet);
        i++;
        for (; i < 5; i++)
        {
            position.x += NODE_POSITION_DIFERENCESS;
            position.y = Random.Range(-5, 5);
            var basicNode = Instantiate(nodesPrefabs[0], nodesParent);
            ((GameObject)basicNode).transform.localPosition = position;
            basicNode.GetComponent<GameNode>().TreePosition = i;
            nodes.Add(i, (GameObject)basicNode);
        }
        i++;
        position.x += NODE_POSITION_DIFERENCESS;
        position.y = 0;
        var server = Instantiate(nodesPrefabs[2], nodesParent);
        ((GameObject)server).transform.localPosition = position;
        server.GetComponent<GameNode>().TreePosition = i;
        nodeServer = server;
        nodes.Add(i, (GameObject)server);
    }

    public void SpawnVirus(GameObject enemyToSpawn)
    {
        Instantiate(enemyToSpawn, nodeInternet.transform);
        var gameNode = (GameNode) nodeInternet.GetComponent<GameNode>();
        gameNode.virus.Add(enemyToSpawn);
    }

    public void RemoveVirus(GameObject virus)
    {
        nodeInternet.GetComponent<GameNode>().virus.Remove(virus);
    }

    private void Update()
    {

    }

    public void Win()
    {

    }
       
    public void GameOver()
    {

    }
}
