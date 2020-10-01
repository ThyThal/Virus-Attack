using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusManager : MonoBehaviour
{
    private const int QUEUE_LIMIT = 5;
    private const int QUEUE_POSITION_DIFERENCESS = 85;

    static public VirusManager instance;

    public Queue items;
    //public QueueTF items;
    public int itemsCount;
    public List<Vector2> queuePositions;

    void Awake()
    {
        instance = this;

        items = new Queue();
        //items.InitializeQueue();
        itemsCount = 0;

        for (int i = 0; i < QUEUE_LIMIT; i++)
        {
            queuePositions.Add(new Vector3(-400, queuePositions.Count == 0 ? 260 : queuePositions[i - 1].y - QUEUE_POSITION_DIFERENCESS, -1));
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
    public void GetItem()
    {
        var tempItem = items.Peek(); /* OBTIENE OBJETO Y REMUEVE DE LA COLA */
        items.Dequeue();
        itemsCount--;

        activeItem = tempItem.GetComponent<Virus>();
        activeItem.transform.localPosition = itemActivePosition;

        UpdatePositions();
    }

    /* Posiciona el power up en pantalla según la dimensión de la pila */
    private Vector2 GetQueuePosition()
    {
        return queuePositions[itemsCount - 1];
    }

    /* Recorre la cola actualizando las posiciones de los objetos restantes tras sacar el primer objeto */
    private void UpdatePositions()
    {
        var count = 0;
        var tempNode = items.PeekNode();

        while (tempNode != null)
        {
            tempNode.data.transform.localPosition = queuePositions[count];
            count++;
            tempNode = tempNode.next;
        }
    }

}
