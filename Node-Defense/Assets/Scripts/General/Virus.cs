﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Virus : MonoBehaviour
{
    [SerializeField] private int score;
    [SerializeField] private ITree init;
    [SerializeField] public GameNode target;
    [SerializeField] private Vector2 direction;
    [SerializeField] private float distance;
    [SerializeField] private float speed;
    [SerializeField] private int damage;
    [SerializeField] private float timer;
    [SerializeField] int life;
    int[] way;
    int actualPosWay;

    // Start is called before the first frame update
    void Start()
    {
        ActionNode findNext = new ActionNode(FindNext);
        ActionNode move = new ActionNode(Move);
        ActionNode attack = new ActionNode(Attack);

        QuestionNode isColliding = new QuestionNode(IsColliding, attack, move);
        QuestionNode isInfected = new QuestionNode(IsTargetInfected, findNext, isColliding);
        QuestionNode hasTarget = new QuestionNode(HasTarget, isInfected , findNext);

        init = hasTarget;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (target != null)
            distance = (target.transform.position - transform.position).magnitude;
        init.Execute();
    }

    private void FindNext()
    {
        // Si no tiene target es porque todavia no se invoco a internet
        if (target != null && LevelManager.instance.isServerInfected == false)
        {
            if(way != null && way.Length > 0)
            {
                actualPosWay++;
                if(actualPosWay >= way.Length)
                {
                    way = new int[0];
                    FindNext();
                    return;
                }
            }
            else { 
                AlgDijkstra.Dijkstra(LevelManager.instance.nodesGraph, LevelManager.instance.nodeInternet.Vertex);
                string[] caminos = AlgDijkstra.nodos;
                List<string> caminosDispo = new List<string>();

                // Me guardo los caminos disponibles
                for (int i = 0; i < caminos.Length; i++)
                {
                    if(caminos[i] != null)
                    {
                        caminosDispo.Add(caminos[i]);
                    }                
                }
                // Agarro un camino random
                string[] camino = caminosDispo[Random.Range(0, caminosDispo.Count)].Split(',');
                way = new int[camino.Length];
                for(int i = 0; i < camino.Length; i++)
                {
                    way[i] = int.Parse(camino[i]);
                }
                actualPosWay = 0;
            }
            NodeManager.instance.nodesDictionary.TryGetValue(way[actualPosWay], out target);
        }
        else if (LevelManager.instance.isServerInfected == true)
        {
            HasDied();
        }
    }

    private void Move()
    {
        direction = target.transform.position - transform.position;
        var distance = direction.magnitude;
        var move = direction * speed * Time.deltaTime;
        transform.position += Vector3.ClampMagnitude(move, distance);

    }

    private void Attack()
    {
        if (timer <= 0)
        {
            target.GetComponent<GameNode>().GetDamage(damage);
            timer = 2;
        }
    }

    public bool IsColliding()
    {

        if (distance <= 0.5)
        {
            return true;
        }
        else
        {
            return false;
        }
        
    }

    public bool HasTarget()
    {
        if (target != null)
        {
            return true;
        }

        else
        {
            return false;
        }
    }

    public bool IsTargetInfected()
    {
        if (target.isInfected)
        {
            return true;
        }

        else
        {
            return false;
        }
    }

    public void GetDamage(int damage)
    {
        if (life > 0)
            life -= damage;

        if (life <= 0)
            HasDied();
    }

    public void HasDied()
    {
        GameManager.Instance.ScoreUpdate(score);
        LevelManager.instance.RemoveVirus(gameObject);
        Destroy(gameObject);
    }
}