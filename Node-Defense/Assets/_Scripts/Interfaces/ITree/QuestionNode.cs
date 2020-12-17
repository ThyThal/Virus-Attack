using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionNode : ITree
{
    public delegate bool myDelegate();
    myDelegate question;
    ITree trueNode;
    ITree falseNode;

    public QuestionNode(myDelegate q, ITree tN, ITree fN)
    {
        question = q;
        trueNode = tN;
        falseNode = fN;
    }

    public void Execute()
    {
        if (question())
        {
            trueNode.Execute();
        }

        else
        {
            falseNode.Execute();
        }
    }
}
