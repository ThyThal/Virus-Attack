using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrafoMA : IGrafo
{
    static int n = 100;
    public int[,] MAdy;
    public int[] Etiqs;
    public int cantNodos;

    public void Initialization()
    {
        MAdy = new int[n, n];
        Etiqs = new int[n];
        cantNodos = 0;
    }

    public void AddVertex(int v)
    {
        Etiqs[cantNodos] = v;
        for (int i = 0; i <= cantNodos; i++)
        {
            MAdy[cantNodos, i] = 0;
            MAdy[i, cantNodos] = 0;
        }
        cantNodos++;
    }

    public void RemoveVertex(int v)
    {
        int ind = VertexToNode(v);

        for (int k = 0; k < cantNodos; k++)
        {
            MAdy[k, ind] = MAdy[k, cantNodos - 1];
        }

        for (int k = 0; k < cantNodos; k++)
        {
            MAdy[ind, k] = MAdy[cantNodos - 1, k];
        }

        Etiqs[ind] = Etiqs[cantNodos - 1];
        cantNodos--;
    }

    public int VertexToNode(int v)
    {
        int i = cantNodos - 1;
        while (i >= 0 && Etiqs[i] != v)
        {
            i--;
        }

        return i;
    }

    public IConjunto Vertices()
    {
        IConjunto Vert = new ConjuntoLD();
        Vert.InicializarConjunto();
        for (int i = 0; i < cantNodos; i++)
        {
            Vert.Agregar(Etiqs[i]);
        }
        return Vert;
    }

    public void AddEdge(int id, int v1, int v2, int peso)
    {
        int o = VertexToNode(v1);
        int d = VertexToNode(v2);
        MAdy[o, d] = peso;
    }

    public void RemoveEdge(int v1, int v2)
    {
        int o = VertexToNode(v1);
        int d = VertexToNode(v2);
        MAdy[o, d] = 0;
    }

    public bool HasEdge(int v1, int v2)
    {
        int o = VertexToNode(v1);
        int d = VertexToNode(v2);
        return MAdy[o, d] != 0;
    }

    public int WeightEdge(int v1, int v2)
    {
        int o = VertexToNode(v1);
        int d = VertexToNode(v2);
        return MAdy[o, d];
    }
}
