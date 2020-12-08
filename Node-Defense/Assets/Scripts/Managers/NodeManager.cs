using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NodeManager : MonoBehaviour
{
    static public NodeManager Instance;

    [SerializeField] private List<int> vertexInit = new List<int>();
    [SerializeField] public int[] vertex;
    [SerializeField] private List<int> visited;
    [SerializeField] public List<Vector3> edges;

    [Header("Nodes Info")]
    [SerializeField] private List<Transform> transforms;
    [SerializeField] private int minRandom;
    [SerializeField] private int maxRandom;
    [SerializeField] private int minEdges;
    [SerializeField] private int maxEdges;
    [SerializeField] private int edgeAmount;

    [Header("Nodes")]
    [SerializeField] private GameObject nodeInternet;
    [SerializeField] private Transform nodeInternetPosition;
    [SerializeField] private GameObject nodeServer;
    [SerializeField] private Transform nodeServerPosition;
    [SerializeField] private GameObject nodeBasic;
    [SerializeField] private Transform nodeBasicPosition;

    [Header("Nodes Position")]
    [SerializeField] private float distanceY;
    [SerializeField] private List<Vector2> currentXnodes;
    [SerializeField] private Vector2 position;
    [SerializeField] private float spacing;
    [SerializeField] private float minY;
    [SerializeField] private float maxY;
    [SerializeField] private int nodesY;
    [SerializeField] private float scale;


    public GameObject lineEdgePrefab;

    public Dictionary<int, GameNode> nodesDictionary;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        } // Instance.
        nodesDictionary = new Dictionary<int, GameNode>();
                
        CreateInitialNodes(); // Creamos la cantidad de vertices a inicializar.
        
        CreateNodeInternet(); // Instanciar Nodo Internet.
        for (int i = 1; i < vertexInit.Count-1; i++)
        {
            vertex[i] = vertexInit[i];
            var transformIndex = Random.Range(1, transforms.Count - 1);
            CreateBasicNode(vertex[i], transforms[transformIndex]);
            transforms.RemoveAt(transformIndex);
        }        
        
        CreateNodeServer(); // Instanciar Nodo Servidor.

        int lastVertex = vertexInit[vertexInit.Count - 1]; // Referencia al ultimo nodo.
        int currentVertex = vertexInit[0]; // Referencia al nodo actual (primero).
        vertexInit.Remove(currentVertex);
        vertexInit.Remove(lastVertex);

        while (vertexInit.Count > 0)
        {            
            int index = Random.Range(0, vertexInit.Count); // Seleccionar un nodo aleatorio para conectarlo.

            if (vertexInit.Count <= maxEdges) 
            {
                edgeAmount = Random.Range(minEdges, vertexInit.Count);
            } // Generamos la cantidad de conexiones a generar en index.

            else
            {
                edgeAmount = Random.Range(minEdges, maxEdges);
            } // Si el maximo de conexiones es mayor que los items, se genera lo mayor posible.

            // Agregar conexion de "currentVertex" con el nodo "index".
            for (int i = 0; i < edgeAmount; i++)
            {  

                // Recorremos todos los nodos visitados para no volver atras.
                for (int k = 0; k < visited.Count; k++)
                {
                    // Si el "index" ya fue visitado, generamos uno nuevo.
                    if (index == visited[k])
                    {
                        index = Random.Range(0, vertexInit.Count);
                        //k = 0;
                    }
               }

                edges.Add(new Vector3(currentVertex, vertexInit[index], 1)); // From, To, Weight.
                InstantiateEdge(currentVertex, vertexInit[index]); // Añadimos la visualizacion de
                visited.Add(index);
            }

            int aux = currentVertex;
            currentVertex = vertexInit[index];
            vertexInit.Remove(aux);
        } //Generar Las Conexiones de los Nodos menos el ultimo.

        edges.Add(new Vector3(currentVertex, lastVertex, 1));
        InstantiateEdge(currentVertex, lastVertex);
    }

    private void CreateInitialNodes()
    {
        // Creamos la cantidad de vertices a inicializar.
        for (int i = 0; i < Random.Range(minRandom, maxRandom); i++)
        {
            vertexInit.Add(i + 1);
        }

        vertex = new int[vertexInit.Count]; // Inicializamos los vertices.
    }
    private void CreateNodeInternet()
    {
        vertex[0] = vertexInit[0];
        var node = Instantiate(nodeInternet, nodeInternetPosition);
        var gameNode = node.GetComponent<GameNode>();
        gameNode.Vertex = vertex.First();
        nodesDictionary.Add(vertex.First(), gameNode);
    }
    private void CreateNodeServer()
    {
        vertex[vertex.Count() - 1] = vertexInit.Last();
        var node = Instantiate(nodeServer, nodeServerPosition);
        var gameNode = node.GetComponent<GameNode>();
        gameNode.Vertex = vertex.Last();
        nodesDictionary.Add(vertex.Last(), gameNode);
    }
    private void CreateBasicNode(int id, Transform nodeBasicPosition)
    {
        var node = Instantiate(nodeBasic, nodeBasicPosition);
        var gameNode = node.GetComponent<GameNode>();
        gameNode.Vertex = id;
        nodesDictionary.Add(id, gameNode);
    }
    private void InstantiateEdge(int origin, int destiny)
    {
        GameNode currentNode;
        GameNode destinyNode;
        nodesDictionary.TryGetValue(origin, out currentNode);
        nodesDictionary.TryGetValue(destiny, out destinyNode);

        var lineEdge = Instantiate(lineEdgePrefab, currentNode.gameObject.transform); // Prefab del LineRenderer.
        lineEdge.GetComponent<LineRenderer>().SetPosition(0, Vector3.zero); // Posicion de Origen.



        Vector3 vectorToTarget = (destinyNode.transform.localPosition - currentNode.transform.localPosition) * scale; // FALLA

        if (vectorToTarget == Vector3.zero) { 
            vectorToTarget = (Vector3.one) * scale; }
        



        lineEdge.GetComponent<LineRenderer>().SetPosition(1, vectorToTarget);
        currentNode.edgesRenderers.Add(lineEdge);
    }
}
