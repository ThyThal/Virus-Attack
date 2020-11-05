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

    [Header("Nodes Position")]
    [SerializeField] private Transform nodesParent;
    [SerializeField] private Vector2 position;
    [SerializeField] private float spacing;
    [SerializeField] private float minY;
    [SerializeField] private float maxY;

    private void Awake()
    {
        instance = this;

        // Creamos la cantidad de vertices a inicializar.
        for (int i = 0; i < Random.Range(minRandom, maxRandom); i++)
        {
            vertexInit.Add(i + 1);
        }

        // Inicializamos los vertices.
        vertex = new int[vertexInit.Count];
        
        // Instanciar Nodo Internet.
        CreateInternet(position);
        position.x = position.x + spacing; // Agregar Espacio Entre Nodos.

        // Instanciar Nodos Basicos.
        for (int i = 1; i < vertexInit.Count-1; i++)
        {
            vertex[i] = vertexInit[i];
            CreateNode(position);
            position.x = position.x + spacing; // Agregar Espacio Entre Nodos.
        }

        // Instanciar Nodo Servidor.
        CreateServer(position);

        int lastVertex = vertexInit[vertexInit.Count - 1]; // Ultimo vertice que va a tener conexiones.
        int currentVertex = vertexInit[0];
        vertexInit.Remove(currentVertex); // Remueve primer nodo.
        vertexInit.Remove(lastVertex); // Remueve ultimo nodo antes del servidor.

        while (vertexInit.Count > 0)
        {
            int index = Random.Range(0, vertexInit.Count);
            edgeAmount = Random.Range(minEdges, maxEdges);
            for (int i = 0; i < edgeAmount; i++)
            {
                //Debug.Log("index " + index);
                //Debug.Log(string.Join(", " ,vertexInit));
                edges.Add(new Vector3(currentVertex, vertexInit[index], 1)); // From, To, Weight.
                if (vertexInit.Count <= 1) break;
                int aux1 = index;

                while (index == aux1)
                {
                    index = Random.Range(0, vertexInit.Count);
                }

            }
            int aux = currentVertex;
            currentVertex = vertexInit[index];
            vertexInit.Remove(aux);
        }

        edges.Add(new Vector3(currentVertex, lastVertex, 1));
    }

    private void CreateInternet(Vector2 p)
    {
        var node = Instantiate(nodeInternet, nodesParent);
        var nodeTransform = node.transform;
        RandomizePosition(nodeTransform, position);
    }

    private void CreateNode(Vector2 p)
    {
        var node = Instantiate(nodeBasic, nodesParent);
        var nodeTransform = node.transform;
        RandomizePosition(nodeTransform, position);
    }

    private void CreateServer(Vector2 p)
    {
        var node = Instantiate(nodeServer, nodesParent);
        var nodeTransform = node.transform;
        RandomizePosition(nodeTransform, position);

    }

    private Vector2 RandomizePosition(Transform t, Vector2 p)
    {
        t.position = new Vector2(p.x, Random.Range(minY, maxY));
        p.x = p.x + spacing;
        return p;
    }
}
