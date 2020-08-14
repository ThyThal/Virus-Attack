using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public List<GameObject> bullets;
    public Dictionary<string, string> diccionry;
    public GameObject prefab;
    public Transform playerTransform;

    private void Start()
    {
        bullets = new List<GameObject>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            bullets.Add(Instantiate(prefab));
            bullets[bullets.Count - 1].name = "Enemy" + bullets.Count;
            bullets[bullets.Count - 1].gameObject.transform.position = playerTransform.position;
        }

        for (int i = 0; i < bullets.Count; i++)
        {
            if (bullets[i].GetComponent<Bullet>().hit)
            {
                Destroy(bullets[i]);
                bullets.RemoveAt(i);
            }
        }
    }
}
