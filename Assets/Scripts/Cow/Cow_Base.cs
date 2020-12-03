using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

//base
public abstract class Cow_Node
{
    public abstract bool Invoke();
}

public class CompositeCow_Node : Cow_Node
{
    public override bool Invoke()
    {
        throw new NotImplementedException();
    }

    public void AddChild(Cow_Node Cow_Node)
    {
        childrens.Push(Cow_Node);
    }

    public Stack<Cow_Node> GetChildrens()
    {
        return childrens;
    }
    private Stack<Cow_Node> childrens = new Stack<Cow_Node>();
}

//composite node
public class Cow_Selector : CompositeCow_Node
{
    public override bool Invoke()
    {
        foreach (var Cow_Node in GetChildrens())
        {
            if (Cow_Node.Invoke())
            {

                return true;
            }
        }
        return false;
    }
}

public class Cow_Sequence : CompositeCow_Node
{
    public override bool Invoke()
    {
        bool p = false;
        foreach (var Cow_Node in GetChildrens())
        {
            if (Cow_Node.Invoke() == false)
            {
                p = true;
            }
        }
        return !p;
    }
}

//상태,행동 추가 
public class Cow_Hungry : Cow_Node
{
    public Cow_Move cow
    {
        set { _cow = value; }
    }
    private Cow_Move _cow;
    public override bool Invoke()
    {
        return _cow.Cow_Hungry();
    }
}
public class Cow_Poop : Cow_Node
{
    public Cow_Move cow
    {
        set { _cow = value; }
    }
    private Cow_Move _cow;
    public override bool Invoke()
    {
        return _cow.Cow_Poop();
    }
}

public class Cow_Play : Cow_Node
{
    public Cow_Move cow
    {
        set { _cow = value; }
    }
    private Cow_Move _cow;
    public override bool Invoke()
    {
        return _cow.Cow_Play();
    }
}

public class Cow_Milk : Cow_Node
{
    public Cow_Move cow
    {
        set { _cow = value; }
    }
    private Cow_Move _cow;
    public override bool Invoke()
    {
        return _cow.Cow_Milk();
    }
}

public class Cow_FollowMouse : Cow_Node
{
    public Cow_Move cow
    {
        set { _cow = value; }
    }
    private Cow_Move _cow;
    public override bool Invoke()
    {
        return _cow.Cow_FollowMouse();
    }
}

public class Cow_Eat : Cow_Node
{
    public Cow_Move cow
    {
        set { _cow = value; }
    }
    private Cow_Move _cow;
    public override bool Invoke()
    {
        return _cow.Cow_Eat();
    }
}
public class Cow_BasicMove : Cow_Node
{
    public Cow_Move cow
    {
        set { _cow = value; }
    }
    private Cow_Move _cow;
    public override bool Invoke()
    {
        return _cow.Cow_BasicMove();
    }
}
public class Cow_Follow_Food : Cow_Node
{
    public Cow_Move cow
    {
        set { _cow = value; }
    }
    private Cow_Move _cow;
    public override bool Invoke()
    {
        return _cow.Cow_Follow_Food();
    }
}
public class Cow_Follow_Milk : Cow_Node
{
    public Cow_Move cow
    {
        set { _cow = value; }
    }
    private Cow_Move _cow;
    public override bool Invoke()
    {
        return _cow.Cow_Follow_Milk();
    }
}

public class Cow_Follow_Egg : Cow_Node
{
    public Cow_Move cow
    {
        set { _cow = value; }
    }
    private Cow_Move _cow;
    public override bool Invoke()
    {
        return _cow.Cow_Follow_Egg();
    }
}