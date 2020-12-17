using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeEdge
{
    public int id;
    public int edgeWeight;
    public NodeGraph nodeOrigin;
    public NodeGraph nodeDestination;
    public NodeEdge nextEdge;
}
