﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public GrafoMA nodesGraph;
    public GameNode nodeInternet;
    public GameNode nodeServer;
    public bool isServerInfected;
    public Transform nodesParent;
    public List<GameObject> nodesPrefabs;
    static public LevelManager instance;

    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        GameManager.Instance.score = 0;
        nodesGraph = new GrafoMA();
        nodesGraph.Initialization();
        int i = 0; // Lo creo afuera para saber cual es la posicion de ultimo vertice
        for (; i < NodeManager.instance.vertex.Length; i++)
        {
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
        enemyToSpawn.transform.position = nodeInternet.transform.position;
        enemyToSpawn.GetComponent<Virus>().target = nodeInternet;
    }

    public void RemoveVirus(GameObject virus)
    {

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
