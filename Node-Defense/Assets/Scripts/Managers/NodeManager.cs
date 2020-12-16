using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class NodeManager : MonoBehaviour
{
    static public NodeManager Instance;
    private List<int> vertexInit = new List<int>();
    private List<int> visited = new List<int>();

    [Header("Public Node Info")]
    public List<Vector3> edges;
    public int[] vertex;

    [Header("Node Manager Info")]
    [SerializeField] private int _minNodes;
    [SerializeField] private int _maxNodes;
    [SerializeField] private int _minEdges;
    [SerializeField] private int _maxEdges;
    private int _edgeAmount;

    [Header("Line Renderer Info")]
    [SerializeField] private GameObject _foundOrigin;
    [SerializeField] private GameObject _foundDestiny;
    [SerializeField] private float _scale;
    [SerializeField] private GameObject lineEdgePrefab;

    [Header("Nodes")]
    [SerializeField] private GameObject nodeInternet;
    [SerializeField] private Transform nodeInternetPosition;
    [SerializeField] private GameObject nodeServer;
    [SerializeField] private Transform nodeServerPosition;
    [SerializeField] private GameObject nodeBasic;
    [SerializeField] private Transform nodeBasicPosition;

    [Header("Transforms")]
    [SerializeField] private List<Transform> transforms1;
    [SerializeField] private List<Transform> transforms2;
    [SerializeField] private List<Transform> transforms3;
    [SerializeField] private List<Transform> transforms4;
    [SerializeField] private List<Transform> transforms5;
    [SerializeField] private List<Transform> transforms6;
    private List<Transform> returnTransforms;


    

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
        var basicNodes = vertexInit.Count - 2;
        var spawnedNodes = 0;
        var nodeID = 1;

        for (int i = 1; i < vertexInit.Count-1; i++)
        {            
            GetGrid(i); // Get Current Grid Column.

            var maxNodesInColumn = Random.Range(1, 3);
            var j = 0;
            for (j = 0; j < maxNodesInColumn; j++) // Crear mas nodos en la misma columna.
            {
                vertex[nodeID] = vertexInit[nodeID];
                if (spawnedNodes == basicNodes)
                {

                }

                else
                {
                    var transformIndex = Random.Range(0, returnTransforms.Count);
                    CreateBasicNode(vertex[nodeID], returnTransforms[transformIndex]);
                    spawnedNodes++;
                    nodeID++;
                    returnTransforms.RemoveAt(transformIndex);
                }
            }
        }        
        
        CreateNodeServer(); // Instanciar Nodo Servidor.

        int lastVertex = vertexInit[vertexInit.Count - 1]; // Referencia al ultimo nodo.
        int currentVertex = vertexInit[0]; // Referencia al nodo actual (primero).
        vertexInit.Remove(currentVertex);
        vertexInit.Remove(lastVertex);

        while (vertexInit.Count > 0)
        {            
            int index = Random.Range(0, vertexInit.Count); // Seleccionar un nodo aleatorio para conectarlo.

            if (vertexInit.Count <= _maxEdges) 
            {
                _edgeAmount = Random.Range(_minEdges, vertexInit.Count);
            } // Generamos la cantidad de conexiones a generar en index.

            else
            {
                _edgeAmount = Random.Range(_minEdges, _maxEdges);
            } // Si el maximo de conexiones es mayor que los items, se genera lo mayor posible.

            // Agregar conexion de "currentVertex" con el nodo "index".
            for (int i = 0; i < _edgeAmount; i++)
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

    private List<Transform> GetGrid(int iteration)
    {
        switch (iteration)
        {
            case 1:
                returnTransforms = transforms1;
                return returnTransforms;

            case 2:
                returnTransforms = transforms2;
                return returnTransforms;

            case 3:
                returnTransforms = transforms3;
                return returnTransforms;

            case 4:
                returnTransforms = transforms4;
                return returnTransforms;

            case 5:
                returnTransforms = transforms5;
                return returnTransforms;

            case 6:
                returnTransforms = transforms6;
                return returnTransforms;

            default:
                return null;
        }
    }

    private void CreateInitialNodes()
    {
        // Creamos la cantidad de vertices a inicializar.
        for (int i = 0; i < Random.Range(_minNodes, _maxNodes); i++)
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

        nodeBasicPosition.GetComponent<Image>().enabled = true;
    }

    private void InstantiateEdge(int origin, int destiny)
    {
        GameNode currentNode;
        GameNode destinyNode;
        nodesDictionary.TryGetValue(origin, out currentNode);
        nodesDictionary.TryGetValue(destiny, out destinyNode);

        FindNodeObject(currentNode, destinyNode);
        var lineEdge = Instantiate(lineEdgePrefab, _foundOrigin.transform); // Prefab del LineRenderer.
        lineEdge.GetComponent<LineRenderer>().SetPosition(0, Vector3.zero); // Posicion de Origen.

        Vector3 vectorToTarget = (_foundDestiny.transform.position - _foundOrigin.transform.position) * _scale; // FALLA

        lineEdge.GetComponent<LineRenderer>().SetPosition(1, vectorToTarget);
        currentNode.edgesRenderers.Add(lineEdge);
    }

    private void FindNodeObject(GameNode originEdgeObject, GameNode destinyEdgeObject)
    {
        var originString = originEdgeObject.transform.parent.name;
        _foundOrigin = GameObject.Find(originString);

        var destinyString = destinyEdgeObject.transform.parent.name;
        _foundDestiny = GameObject.Find(destinyString);
    }
}
