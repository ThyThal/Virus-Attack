using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager1 : MonoBehaviour
{

    public BinaryTree nodes;

    public Dictionary<string, string> dictionary;

    public List<GameObject> nodesPrefabs;

    public Transform parent;

    static public GameManager1 instance;

    void Awake()
    {
        int i = 0;
        GameObject basicNode = Instantiate(nodesPrefabs[0], parent);

    }
       
}
