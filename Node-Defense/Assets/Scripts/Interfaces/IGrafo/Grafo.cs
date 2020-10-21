using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grafo : IGrafo
{
    NodeGraph origin;

    // Inicializar el grafo.
    public void Initialization()
    {
        origin = null;
    }

    public void AddEdge(int v)
    {
        NodeGraph aux = new NodeGraph();
        aux.nodeValue = v;
        aux.edge = null;
        aux.nextNode = origin;
        origin = aux;
    }

    public void AddEdge(int v1, int v2, int weight)
    {
        NodeGraph n1 = VertexToNode(v1);
        NodeGraph n2 = VertexToNode(v2);

        NodeEdge aux = new NodeEdge();
        aux.edgeWeight = weight;
        aux.nodeDestination = n2;
        aux.nextEdge = n1.edge;
        n1.edge = aux;
    }

    private NodeGraph VertexToNode(int v)
    {
        NodeGraph aux = origin;
        if (aux != null && aux.nodeValue != v)
        {
            aux = aux.nextNode;
        }

        return aux;
    }
}
