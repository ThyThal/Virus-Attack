using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Virus : MonoBehaviour
{
    [SerializeField] private ITree init;
    [SerializeField] public GameObject target;
    [SerializeField] private Vector2 direction;
    [SerializeField] private float distance;
    [SerializeField] private float speed;
    [SerializeField] private int damage;
    [SerializeField] private float timer;
    [SerializeField] int life;

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
        if (LevelManager.instance.isServerInfected == false)
        {
            // TODO algoritmo para buscar siguiente
            /*var targetNode = target.GetComponent<GameNode>().NextNode();
            if(targetNode != null)
                target = targetNode.gameNode; */
        }
        if (LevelManager.instance.isServerInfected == true)
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
            target = GameObject.FindGameObjectWithTag("Node");
            return false;
        }
    }

    public bool IsTargetInfected()
    {
        if (target.GetComponent<GameNode>().isInfected)
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
        GameManager.Instance.score += 100;
        LevelManager.instance.RemoveVirus(gameObject);
        Destroy(gameObject);
    }
}
