using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Grafo : IGrafo
{
    NodeGraph origin;

    // Inicializar el grafo.
    public void Initialization()
    {
        origin = null;
    }

    /*
     */
    public void AddVertex(int v)
    {
        NodeGraph aux = new NodeGraph();
        aux.nodeValue = v;
        aux.edge = null;
        aux.nextNode = origin;
        origin = aux;
    }

    /*
     */
    public void RemoveVertex(int v)
    {
        if (origin.nodeValue == v)
        {
            origin = origin.nextNode;
        }

        NodeGraph aux = origin;
        if (aux != null)
        {
            RemoveEdgeNode(aux, v);

            if (aux.nextNode != null && aux.nextNode.nodeValue == v)
            {
                aux.nextNode = aux.nextNode.nextNode;
            }
            aux = aux.nextNode;
        }
    }

    /*
     */
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

    /*
     */
    public void RemoveEdge(int v1, int v2)
    {
        NodeGraph n1 = VertexToNode(v1);
        RemoveEdgeNode(n1, v2);
    }

    /*
     */
    private NodeGraph VertexToNode(int v)
    {
        NodeGraph aux = origin;
        if (aux != null && aux.nodeValue != v)
        {
            aux = aux.nextNode;
        }

        return aux;
    }

    /*
     */
    private void RemoveEdgeNode(NodeGraph node, int v)
    {
        NodeEdge aux = node.edge;
        if (aux != null)
        {
            if (aux.nodeDestination.nodeValue == v)
            {
                node.edge = aux.nextEdge;
            }

            else
            {
                if (aux.nextEdge != null && aux.nextEdge.nodeDestination.nodeValue != v)
                {
                    aux = aux.nextEdge;
                }

                if (aux.nextEdge != null)
                {
                    aux.nextEdge = aux.nextEdge.nextEdge;
                }

            }
        }
    }

    /*
     */
    public int WeightEdge(int v1, int v2)
    {
        NodeGraph n1 = VertexToNode(v1);
        NodeEdge aux = n1.edge;

        if (aux.nodeDestination.nodeValue != v2)
        {
            aux = aux.nextEdge;
        }

        return aux.edgeWeight;
    }

    /*
     */
    public bool HasEdge(int v1, int v2)
    {
        NodeGraph n1 = VertexToNode(v1);
        NodeEdge aux = n1.edge;

        if (aux != null && aux.nodeDestination.nodeValue != v2)
        {
            aux = aux.nextEdge;
        }

        return aux != null;
    }
}
