﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public Grafo nodesGraph;
    private GameNode nodeInternet;
    private GameNode nodeServer;
    private WaveManager waveManager;
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
        int i = 0; // Lo creo afuera para saber cual es la posicion de ultimo vertice
        for (; i < NodeManager.instance.vertex.Length; i++)
        {
            basicNode.transform.localPosition = position;
            nodesGraph.AddVertex(NodeManager.instance.vertex[i]);
        }

        for (int k = 0; k < NodeManager.instance.edges.Count; k++)
        {
            nodesGraph.AddEdge(k, (int)NodeManager.instance.edges[k].x, (int)NodeManager.instance.edges[k].y, (int)NodeManager.instance.edges[k].z);// Id, From, To, Weight.
        }

        NodeManager.instance.nodesDictionary.TryGetValue(NodeManager.instance.vertex[0], out nodeInternet);
        NodeManager.instance.nodesDictionary.TryGetValue(NodeManager.instance.vertex[i-1], out nodeServer);
    }

    public void SpawnVirus(GameObject enemyToSpawn)
    {
        Instantiate(enemyToSpawn, nodeInternet.transform);
        enemyToSpawn.transform.position = nodeInternet.transform.position;
    }

    public void RemoveVirus(GameObject virus)
    {
        //nodeInternet.GetComponent<GameNode>().virus.Remove(virus);
    }

    private void Update()
    {
        if (nodeServer.isInfected == true)
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
