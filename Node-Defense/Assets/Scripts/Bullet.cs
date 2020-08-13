using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float movSpeed { get; set; }
    public  Vector3 startPosition { get; set; }
    public bool hit { get; set; }

    private void Start()
    {
        hit = false;
    }

    private void Update()
    {
        this.transform.Translate(Vector3.up);

        if (this.transform.position.y > 100)
        {
            hit = true;
            Debug.Log("Hit");
        }
    }
}
