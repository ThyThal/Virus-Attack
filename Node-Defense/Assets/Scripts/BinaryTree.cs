using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BinaryTree
{
    public Node Root { get; set; }

    public bool Add(int value, GameObject gameNode)
    {
        Node before = null, after = this.Root;

        while (after != null)
        {
            before = after;
            if (value < after.value) //Is new node in left tree? 
                after = after.leftNode;
            else if (value > after.value) //Is new node in right tree?
                after = after.rightNode;
            else
            {
                //Exist same value
                return false;
            }
        }

        Node newNode = new Node(gameNode, value);

        if (this.Root == null)//Tree ise empty
            this.Root = newNode;
        else
        {
            if (value < before.value)
                before.leftNode = newNode;
            else
                before.rightNode = newNode;
        }

        return true;
    }

    public Node Find(int value)
    {
        return this.Find(value, this.Root);
    }

    public void Remove(int value)
    {
        this.Root = Remove(this.Root, value);
    }

    private Node Remove(Node parent, int key)
    {
        if (parent == null) return parent;

        if (key < parent.value) parent.leftNode = Remove(parent.leftNode, key);
        else if (key > parent.value)
            parent.rightNode = Remove(parent.rightNode, key);

        // if value is same as parent's value, then this is the node to be deleted  
        else
        {
            // node with only one child or no child  
            if (parent.leftNode == null)
                return parent.rightNode;
            else if (parent.rightNode == null)
                return parent.leftNode;

            // node with two children: Get the inorder successor (smallest in the right subtree)  
            parent.value = Minvalue(parent.rightNode);

            // Delete the inorder successor  
            parent.rightNode = Remove(parent.rightNode, parent.value);
        }

        return parent;
    }

    private int Minvalue(Node node)
    {
        int minv = node.value;

        while (node.leftNode != null)
        {
            minv = node.leftNode.value;
            node = node.leftNode;
        }

        return minv;
    }

    private Node Find(int value, Node parent)
    {
        if (parent != null)
        {
            if (value == parent.value) return parent;
            if (value < parent.value)
                return Find(value, parent.leftNode);
            else
                return Find(value, parent.rightNode);
        }

        return null;
    }

    public int GetTreeDepth()
    {
        return this.GetTreeDepth(this.Root);
    }

    private int GetTreeDepth(Node parent)
    {
        return parent == null ? 0 : Math.Max(GetTreeDepth(parent.leftNode), GetTreeDepth(parent.rightNode)) + 1;
    }

    public void TraversePreOrder(Node parent)
    {
        if (parent != null)
        {
            TraversePreOrder(parent.leftNode);
            TraversePreOrder(parent.rightNode);
        }
    }

    public void TraverseInOrder(Node parent)
    {
        if (parent != null)
        {
            TraverseInOrder(parent.leftNode);
            TraverseInOrder(parent.rightNode);
        }
    }

    public void TraversePostOrder(Node parent)
    {
        if (parent != null)
        {
            TraversePostOrder(parent.leftNode);
            TraversePostOrder(parent.rightNode);
        }
    }
}
