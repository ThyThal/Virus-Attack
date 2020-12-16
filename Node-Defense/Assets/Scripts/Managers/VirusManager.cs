using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusManager : MonoBehaviour
{
    private int QUEUE_LIMIT;

    static public VirusManager instance;

    public Queue items;
    //public QueueTF items;
    public int itemsCount;
    public List<Vector2> queuePositions;
    public List<GameObject> prefabs;
    public Transform parent;
    public List<Transform> positions;
	
	public ABB virusTypes;

    void Awake()
    {
        instance = this;

        items = new Queue();
        //items.InitializeQueue();
        itemsCount = 0;
        QUEUE_LIMIT = positions.Count;

        for (int i = 0; i < QUEUE_LIMIT; i++)
        {
            queuePositions.Add(positions[i].position);
        }
        virusTypes = new ABB();
        virusTypes.InicializarArbol();

        foreach(GameObject virusGo in prefabs)
        {
            virusTypes.AgregarElem(ref virusTypes.raiz, virusGo.GetComponent<Virus>());
        }
    }

    /* Almacena el power up en la cola */
    public void EnqueueItem(GameObject item)
    {
        if (itemsCount < QUEUE_LIMIT)
        {
            items.Enqueue(item); /* AGREGA OBJETO A LA COLA */
            itemsCount++;

            //item.GetComponent<Virus>().InQueue = true;
            item.transform.localPosition = GetQueuePosition();
        }
        else
        {
            Destroy(item);
        }
    }

    /* Setea el último power up en la cola a activo para aplicar */
    public GameObject GetItem()
    {
        GameObject item = (GameObject)items.Peek(); /* OBTIENE OBJETO Y REMUEVE DE LA COLA */
        items.Dequeue();
        itemsCount--;

        /*activeItem = tempItem.GetComponent<Virus>();
        activeItem.transform.localPosition = itemActivePosition;*/

        //UpdatePositions();
        for (int i = 0; i < items.Count; i++)
        {
            ((GameObject)(items.ToArray())[i]).transform.localPosition = queuePositions[i];
        }

        return item;
    }

    /* Posiciona el power up en pantalla según la dimensión de la pila */
    private Vector2 GetQueuePosition()
    {
        return queuePositions[itemsCount - 1];
    }

    public void InstanstiateQueue(int totalItems)
    {
        for(int i = 0; i< totalItems ; i++)
        {
            int typeVirus = Random.Range(0, 9);
            var newItem = Instantiate(GetVirus(typeVirus).gameObject, parent);
            EnqueueItem(newItem);
        }
    }

    public void DepleteQueue()
    {
        items.Clear();
    }

    private Virus GetVirus(int hierachy)
    {
        return GetVirusNodeABB(virusTypes.raiz, hierachy);
    }

    private Virus GetVirusNodeABB(NodoABB node, int hierachy)
    {
        if (node != null) { 
            Virus virus;
            if (node.info.hierarchy == hierachy)
                return node.info;
            else if(node.info.hierarchy < hierachy)
            {
                virus = GetVirusNodeABB(node.hijoDer, hierachy);
                if (virus != null)
                    return virus;
                else
                    return null;
            }
            else
            {
                virus = GetVirusNodeABB(node.hijoIzq, hierachy);
                if (virus != null)
                    return virus;
                else
                    return node.info;
            }
        }
        return null;
    }
}
