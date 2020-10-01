﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameNode : MonoBehaviour, IGameNode
{
    [SerializeField] private int treePosition;
    [SerializeField] private PowerUp powerUp;
    [SerializeField] int Type;
    [SerializeField] int life;
    [SerializeField] public bool isInfected;
    [SerializeField] public List<GameObject> virus;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private float timer;
    [SerializeField] private int damage;

    public int TreePosition
    {
        get
        {
            return treePosition;
        }

        set
        {
            treePosition = value;
        }
    }

    public PowerUp PowerUp
    {
        get
        {
            return powerUp;
        }

        set
        {
            powerUp = value;
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        GameObject targetVirus = GameObject.FindGameObjectWithTag("Virus");
        if (targetVirus != null)
        {
            if (timer <= 0)
            {
                Attack(targetVirus.GetComponent<Virus>());
                timer = 2;
            }
        }
        /*foreach (GameObject v in virus)
        {
            targetVirus = v;
            Virus scriptVirus = v.GetComponent<Virus>();

            if (targetVirus.target == null)
            {
                var nextNode = NextNode().gameNode;
                targetVirus.target = nextNode;
            }

            if (targetVirus != null)
            {
                if (timer <= 0)
                {
                    //Attack(targetVirus);
                    timer = 2;
                }
            }


        }*/
    }

    private void Attack(Virus v)
    {
        Debug.Log("attack");
        v.GetDamage(damage);
    }

    public Node NextNode()
    {
        /*Node actualNode = LevelManager.instance.nodes.Find(treePosition);
        bool Boolean = (Random.value > 0.5f);*/
        Node nextNode = LevelManager.instance.nodes.Find(treePosition++); 
        /*if (Boolean)
        {
            nextNode = actualNode.leftNode != null ? actualNode.leftNode : actualNode.rightNode;
        }
        else
        {
            nextNode = actualNode.rightNode != null ? actualNode.rightNode : actualNode.leftNode;
        }*/
        return nextNode;
    }

    public void GetDamage(int damage)
    {
        if (life > 0)
            life -= damage;
        else if(life <= 0)
            HasDied();
    }

    public void HasDied()
    {
        isInfected = true;
        spriteRenderer.color = new Color(1f, 0.47f, 0.47f);
    }
}
