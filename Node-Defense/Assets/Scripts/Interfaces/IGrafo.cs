using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGrafo
{
    void Initialization();
    void AddVertex(int v);
    void RemoveVertex(int v);
    void AddEdge(int v1, int v2, int weight);
    void RemoveEdge(int v1, int v2);
    bool HasEdge(int v1, int v2);
    int WeightEdge(int v1, int v2);
}
