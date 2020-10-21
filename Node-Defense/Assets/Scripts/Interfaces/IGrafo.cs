using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGrafo
{
    private void Initialization();
    private void AddVertex(int v);
    private void RemoveVertex(int v);
    private void AddEdge(int v1, int v2, int weight);
    private void RemoveEdge(int v1, int v2);
    private bool HasEdge(int v1, int v2);
    private int WeightEdge(int v1, int v2);
}
