using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Virus : MonoBehaviour
{
    [SerializeField] private ITree init;
    [SerializeField] public GameObject target;
    [SerializeField] private Vector2 direction;
    [SerializeField] private float speed;

    // Start is called before the first frame update
    void Start()
    {
        ActionNode findNext = new ActionNode(FindNext);
        ActionNode sendAttack = new ActionNode(Attack);

        QuestionNode isInfected = new QuestionNode(IsTargetInfected, findNext, sendAttack);
        QuestionNode hasTarget = new QuestionNode(HasTarget, isInfected , findNext);
        //QuestionNode isInfected = new QuestionNode(IsInfected, findNear, sendAttack);

        init = hasTarget;
    }

    // Update is called once per frame
    void Update()
    {
        init.Execute();
    }

    public void Exe()
    {
        target = GameObject.FindGameObjectWithTag("Node");
    }

    private void FindNext()
    {
        //target = levelManager.nodes.;
    }

    private void Attack()
    {
        direction = target.transform.position - transform.position;
        var distance = direction.magnitude;
        var move = transform.right * speed * Time.deltaTime;
        transform.position += Vector3.ClampMagnitude(move, distance);
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
        if (target.GetComponent<GameNode>().isInfected)
        {
            return true;
        }

        else
        {
            return false;
        }
    }
}
