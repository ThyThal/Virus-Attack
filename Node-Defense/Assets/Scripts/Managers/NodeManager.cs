using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NodeManager : MonoBehaviour
{
    static public NodeManager instance;

    [SerializeField] private List<int> vertexInit = new List<int>();
    [SerializeField] public int[] vertex;
    [SerializeField] public List<Vector3> edges;

    [Header("Nodes Info")]
    [SerializeField] private int minRandom;
    [SerializeField] private int maxRandom;
    [SerializeField] private int minEdges;
    [SerializeField] private int maxEdges;
    [SerializeField] private int edgeAmount;

    [Header("Nodes")]
    [SerializeField] private GameObject nodeInternet;
    [SerializeField] private GameObject nodeBasic;
    [SerializeField] private GameObject nodeServer;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        for (int i = 0; i < Random.Range(minRandom, maxRandom); i++)
        {
            vertexInit.Add(i + 1);
        }

        vertex = new int[vertexInit.Count];
        for (int i = 0; i < vertexInit.Count; i++)
        {
            vertex[i] = vertexInit[i];
        }

        int currentVertex = vertexInit[0];
        int lastVertex = vertexInit[vertexInit.Count - 1]; // Ultimo vertice que va a tener conexiones.
        vertexInit.Remove(currentVertex); // Remueve primer nodo.
        vertexInit.Remove(lastVertex); // Remueve ultimo nodo antes del servidor.

        while (vertexInit.Count > 0)
        {
            int index = Random.Range(0, vertexInit.Count);
            edgeAmount = Random.Range(minEdges, maxEdges);
            for (int i = 0; i < edgeAmount; i++)
            {
                print("index " + index);
                print(string.Join(", " ,vertexInit));
                edges.Add(new Vector3(currentVertex, vertexInit[index], 1)); // From, To, Weight.
                if (vertexInit.Count <= 1) break;
                int aux1 = index;
                while (index == aux1)
                    index = Random.Range(0, vertexInit.Count);

            }
            int aux = currentVertex;
            currentVertex = vertexInit[index];
            vertexInit.Remove(aux);
        }

        edges.Add(new Vector3(currentVertex, lastVertex, 1));
    }
}
