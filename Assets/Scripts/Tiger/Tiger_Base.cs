using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public abstract class Node
{
    public abstract bool Invoke();
}
public class CompositeNode : Node
{
    public override bool Invoke()
    {
        throw new NotImplementedException();
    }

    public void AddChild(Node node)
    {
        childrens.Push(node);
    }

    public Stack<Node> GetChildrens()
    {
        return childrens;
    }
    private Stack<Node> childrens = new Stack<Node>();
}

public class Selector : CompositeNode
{
    public override bool Invoke()
    {
        foreach (var node in GetChildrens())
        {
            if (node.Invoke())
            {

                return true;
            }
        }
        return false;
    }
}

public class Sequence : CompositeNode
{
    public override bool Invoke()
    {
        bool p = false;
        foreach (var node in GetChildrens())
        {
            if (node.Invoke() == false)
            {
                p = true;
            }
        }
        return !p;
    }
}
public class Hungry : Node
{
    public Tiger_Move tiger
    {
        set { _tiger = value;  }
    }
    private Tiger_Move _tiger;
    public override bool Invoke()
    {
        return _tiger.Hungry();
    }
}
public class Poop : Node
{
    public Tiger_Move tiger
    {
        set { _tiger = value; }
    }
    private Tiger_Move _tiger;
    public override bool Invoke()
    {
        return _tiger.Poop();
    }
}

public class FollowMouse : Node
{
    public Tiger_Move tiger
    {
        set { _tiger = value; }
    }
    private Tiger_Move _tiger;
    public override bool Invoke()
    {
        return _tiger.FollowMouse();
    }
}

public class Eat : Node
{
    public Tiger_Move tiger
    {
        set { _tiger = value; }
    }
    private Tiger_Move _tiger;
    public override bool Invoke()
    {
        return _tiger.Eat();
    }
}
public class BasicMove : Node
{
    public Tiger_Move tiger
    {
        set { _tiger = value; }
    }
    private Tiger_Move _tiger;
    public override bool Invoke()
    {
        return _tiger.BasicMove();
    }
}