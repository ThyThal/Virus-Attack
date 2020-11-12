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
    [SerializeField] private float distanceY;
    [SerializeField] private List<Vector2> currentXnodes;
    [SerializeField] private Transform nodesParent;
    [SerializeField] private Vector2 position;
    [SerializeField] private float spacing;
    [SerializeField] private float minY;
    [SerializeField] private float maxY;
    [SerializeField] private int nodesY;


    public GameObject lineEdgePrefab;

    public Dictionary<int, GameNode> nodesDictionary;

    private void Awake()
    {
        instance = this;
        nodesDictionary = new Dictionary<int, GameNode>();
        // Creamos la cantidad de vertices a inicializar.
        for (int i = 0; i < Random.Range(minRandom, maxRandom); i++)
        {
            vertexInit.Add(i + 1);
        }

        // Inicializamos los vertices.
        vertex = new int[vertexInit.Count];

        // Instanciar Nodo Internet.
        vertex[0] = vertexInit[0];
        CreateInternet(position);

        position.x = position.x + spacing; // Agregar Espacio Entre Nodos.

        int j = 0;
        // Instanciar Nodos Basicos.
        for (int i = 1; i < vertexInit.Count-1; i++)
        {
            j++;
            if (j >= nodesY) // Pasa al siguiente
            {
                position.x += spacing;// Agregar Espacio Entre Nodos.
                currentXnodes.Clear();
                j = 0;
            }

            vertex[i] = vertexInit[i];

            // CHECK DISTANCE
            if (currentXnodes.Count == 0)
            {
                CreateNode(position, vertex[i]);
                position = RandomizePosition(position);
                currentXnodes.Add(position);
            }
            else
            {
                for (int k = 0; k < currentXnodes.Count; k++)
                {
                    //var distance = Mathf.Abs(currentXnodes[k] - Mathf.Abs(position.y));
                    var distance = (currentXnodes[k] - position).magnitude;
                    while (distance <= distanceY)
                    {
                        position = RandomizePosition(position);
                        distance = (currentXnodes[k] - position).magnitude;
                    }
                }
                CreateNode(position, vertex[i]);
            }
        }

        position.x += spacing;// Agregar Espacio Entre Nodos.
        // Instanciar Nodo Servidor.
        vertex[vertex.Count()-1] = vertexInit.Last();
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
                InstantiateEdge(currentVertex, vertexInit[index]);
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
        InstantiateEdge(currentVertex, lastVertex);
    }

    private void InstantiateEdge(int origin, int destiny)
    {
        GameNode currentNode;
        GameNode destinyNode;
        nodesDictionary.TryGetValue(origin, out currentNode);
        nodesDictionary.TryGetValue(destiny, out destinyNode);
        var lineEdge = Instantiate(lineEdgePrefab, currentNode.gameObject.transform);
        lineEdge.GetComponent<LineRenderer>().SetPosition(0, currentNode.transform.position);
        Vector3 vectorToTarget = (destinyNode.transform.position - currentNode.transform.position) * 50;
        lineEdge.GetComponent<LineRenderer>().SetPosition(1, vectorToTarget);
        currentNode.edgesRenderers.Add(lineEdge);
    }

    private void CreateInternet(Vector2 p)
    {
        var node = Instantiate(nodeInternet, nodesParent);
        node.GetComponent<GameNode>().Vertex = vertex.First();
        var nodeTransform = node.transform;
        nodeTransform.position = p;
        nodesDictionary.Add(vertex.First(), node.GetComponent<GameNode>());
    }

    private void CreateNode(Vector2 p, int id)
    {
        var node = Instantiate(nodeBasic, nodesParent);
        node.GetComponent<GameNode>().Vertex = id;
        var nodeTransform = node.transform;
        nodeTransform.position = p;
        nodesDictionary.Add(id, node.GetComponent<GameNode>());
    }

    private void CreateServer(Vector2 p)
    {
        var node = Instantiate(nodeServer, nodesParent);
        node.GetComponent<GameNode>().Vertex = vertex.Last();
        var nodeTransform = node.transform;
        nodeTransform.position = p;
        nodesDictionary.Add(vertex.Last(), node.GetComponent<GameNode>());
    }

    private Vector2 RandomizePosition(Vector2 p)
    {
        Vector2 random;
        random.x = Random.Range(-0.5f, 0.5f);
        random.y = Random.Range(-0.5f, 0.5f);

        p = new Vector2(p.x, Random.Range(minY, maxY)) + random;
        return p;
    }
}
