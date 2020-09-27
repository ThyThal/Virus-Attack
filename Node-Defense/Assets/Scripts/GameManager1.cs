using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager1 : MonoBehaviour
{

    public BinaryTree nodes;
    private const int NODE_POSITION_DIFERENCESS = 100;

    public Dictionary<string, string> dictionary;

    public List<GameObject> nodesPrefabs;

    public Transform parent;

    static public GameManager1 instance;

    void Awake()
    {
        int i = 0;
        var internet = Instantiate(nodesPrefabs[1], parent);
        Vector2 position = new Vector2(300, 300);
        ((GameObject)internet).transform.localPosition = position;
        nodes.Add(i, internet.GetComponent<GameNode>());
        for (; i < 5; i++)
        {
            position.x += NODE_POSITION_DIFERENCESS;
            var basicNode = Instantiate(nodesPrefabs[0], parent);
            ((GameObject)basicNode).transform.localPosition = position;
            nodes.Add(i, internet.GetComponent<GameNode>());
        }
        i++;
        position.x += NODE_POSITION_DIFERENCESS;
        var server = Instantiate(nodesPrefabs[0], parent);
        ((GameObject)server).transform.localPosition = position;
        nodes.Add(i, internet.GetComponent<GameNode>());

    }
       
}
