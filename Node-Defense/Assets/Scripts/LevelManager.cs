using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    public BinaryTree nodes;
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
        nodes.Add(i, internet.GetComponent<GameNode>());
        for (; i < 5; i++)
        {
            position.x += NODE_POSITION_DIFERENCESS;
            position.y = Random.Range(-5, 5);
            var basicNode = Instantiate(nodesPrefabs[0], nodesParent);
            ((GameObject)basicNode).transform.localPosition = position;
            nodes.Add(i, internet.GetComponent<GameNode>());
        }
        i++;
        position.x += NODE_POSITION_DIFERENCESS;
        position.y = 0;
        var server = Instantiate(nodesPrefabs[2], nodesParent);
        ((GameObject)server).transform.localPosition = position;
        nodes.Add(i, internet.GetComponent<GameNode>());

    }
       
}
