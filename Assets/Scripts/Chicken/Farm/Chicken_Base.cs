﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

//base
public abstract class Chicken_Node
{
    public abstract bool Invoke();
}

public class CompositeChicken_Node : Chicken_Node
{
    public override bool Invoke()
    {
        throw new NotImplementedException();
    }

    public void AddChild(Chicken_Node Chicken_Node)
    {
        childrens.Push(Chicken_Node);
    }

    public Stack<Chicken_Node> GetChildrens()
    {
        return childrens;
    }
    private Stack<Chicken_Node> childrens = new Stack<Chicken_Node>();
}

//composite node
public class Chicken_Selector : CompositeChicken_Node
{
    public override bool Invoke()
    {
        foreach (var Chicken_Node in GetChildrens())
        {
            if (Chicken_Node.Invoke())
            {

                return true;
            }
        }
        return false;
    }
}

public class Chicken_Sequence : CompositeChicken_Node
{
    public override bool Invoke()
    {
        bool p = false;
        foreach (var Chicken_Node in GetChildrens())
        {
            if (Chicken_Node.Invoke() == false)
            {
                p = true;
            }
        }
        return !p;
    }
}
//상태
public class Chicken_Hungry : Chicken_Node
{
    public Chicken_Move chicken
    {
        set { _chicken = value; }
    }
    private Chicken_Move _chicken;
    public override bool Invoke()
    {
        return _chicken.Chicken_Hungry();
    }
}
public class Chicken_Poop : Chicken_Node
{
    public Chicken_Move chicken
    {
        set { _chicken = value; }
    }
    private Chicken_Move _chicken;
    public override bool Invoke()
    {
        return _chicken.Chicken_Poop();
    }
}

public class Chicken_Play : Chicken_Node
{
    public Chicken_Move chicken
    {
        set { _chicken = value; }
    }
    private Chicken_Move _chicken;
    public override bool Invoke()
    {
        return _chicken.Chicken_Play();
    }
}
//행동 
public class Chicken_FollowMouse : Chicken_Node
{
    public Chicken_Move chicken
    {
        set { _chicken = value; }
    }
    private Chicken_Move _chicken;
    public override bool Invoke()
    {
        return _chicken.Chicken_FollowMouse();
    }
}

public class Chicken_Eat : Chicken_Node
{
    public Chicken_Move chicken
    {
        set { _chicken = value; }
    }
    private Chicken_Move _chicken;
    public override bool Invoke()
    {
        return _chicken.Chicken_Eat();
    }
}

public class Chicken_BasicMove : Chicken_Node
{
    public Chicken_Move chicken
    {
        set { _chicken = value; }
    }
    private Chicken_Move _chicken;
    public override bool Invoke()
    {
        return _chicken.Chicken_BasicMove();
    }
}

public class Chicken_Follow_Food : Chicken_Node
{
    public Chicken_Move chicken
    {
        set { _chicken = value; }
    }
    private Chicken_Move _chicken;
    public override bool Invoke()
    {
        return _chicken.Chicken_Follow_Food();
    }
}

public class Chicken_Follow_Milk : Chicken_Node
{
    public Chicken_Move chicken
    {
        set { _chicken = value; }
    }
    private Chicken_Move _chicken;
    public override bool Invoke()
    {
        return _chicken.Chicken_Follow_Milk();
    }
}

public class Chicken_Follow_Egg : Chicken_Node
{
    public Chicken_Move chicken
    {
        set { _chicken = value; }
    }
    private Chicken_Move _chicken;
    public override bool Invoke()
    {
        return _chicken.Chicken_Follow_Egg();
    }
}

public class Chicken_Drop_Egg : Chicken_Node
{
    public Chicken_Move chicken
    {
        set { _chicken = value; }
    }
    private Chicken_Move _chicken;
    public override bool Invoke()
    {
        return _chicken.Chicken_Egg();
    }
}

public class Chicken_Quarrel : Chicken_Node
{
    public Chicken_Move chicken
    {
        set { _chicken = value; }
    }
    private Chicken_Move _chicken;
    public override bool Invoke()
    {
        return _chicken.Chicken_Quarrel();
    }
}