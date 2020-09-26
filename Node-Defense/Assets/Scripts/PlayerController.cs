
﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    private float movSpeed;

    private void Start()
    {
        movSpeed = 0.5f;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            this.transform.Translate(Vector3.right * movSpeed);
        }
        
        if (Input.GetKey(KeyCode.A))
        {
            this.transform.Translate(Vector3.left * movSpeed);
        }
    }
}
