using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeManager : MonoBehaviour
{
    [SerializeField] private int[] vertex;

    Dictionary<int, NodeGraph> dicNodes = new Dictionary<int, NodeGraph>();
    Dictionary<int, NodeEdge> dicEdge = new Dictionary<int, NodeEdge>();

    private void Start()
    {
        // Creamos un array de ID de NodeGraph.
        for (int i = 0; i < vertex.Length; i++)
        {
            // Asignamos un ID al Vertex.
            vertex[i] = i;


            // Creamos un NodeGraph y agregamos al diccionario.
            NodeGraph n1 = new NodeGraph();
            n1.id = vertex[i];
            dicNodes.Add(n1.id, n1);
        }
    }
}
