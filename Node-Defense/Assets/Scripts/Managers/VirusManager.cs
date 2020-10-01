﻿using System.Collections;
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
    public List<GameObject> prefabs;
    public Transform parent;

    void Awake()
    {
        instance = this;

        items = new Queue();
        //items.InitializeQueue();
        itemsCount = 0;

        for (int i = 0; i < QUEUE_LIMIT; i++)
        {
            queuePositions.Add(new Vector2(-400, queuePositions.Count == 0 ? 260 : queuePositions[i - 1].y - QUEUE_POSITION_DIFERENCESS));
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
            var newItem = Instantiate(prefabs[Random.Range(0, prefabs.Count)], parent);
            EnqueueItem(newItem);
        }
    }

    public void DepleteQueue()
    {
        items.Clear();
    }

}
