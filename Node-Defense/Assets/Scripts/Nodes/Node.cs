using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public IGameNode gameNode { get; set; }
    public int value { get; set; }
    public Node leftNode { get; set; }
    public Node rightNode { get; set; }

    public Node (IGameNode gameNode, int value, Node left, Node right)
    {
        this.gameNode = gameNode;
        this.value = value;
        leftNode = left;
        rightNode = right;
    }

    public Node(IGameNode gameNode, int value)
    {
        this.gameNode = gameNode;
        this.value = value;
        leftNode = null;
        rightNode = null;
    }
}
