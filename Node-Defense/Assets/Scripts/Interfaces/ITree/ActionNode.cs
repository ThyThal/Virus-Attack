using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionNode : ITree
{
    public delegate void myDelegate();
    myDelegate action;

    public ActionNode(myDelegate a)
    {
        action = a;
    }

    public void SubAction(myDelegate newAction)
    {
        action += newAction;
    }

    public void Execute()
    {
        action();
    }
}
