using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

//base
public abstract class Cow_Attack_Node
{
    public abstract bool Invoke();
}

public class CompositeCow_Attack_Node : Cow_Attack_Node
{
    public override bool Invoke()
    {
        throw new NotImplementedException();
    }

    public void AddChild(Cow_Attack_Node Cow_Attack_Node)
    {
        childrens.Push(Cow_Attack_Node);
    }

    public Stack<Cow_Attack_Node> GetChildrens()
    {
        return childrens;
    }
    private Stack<Cow_Attack_Node> childrens = new Stack<Cow_Attack_Node>();
}

//composite node
public class Cow_Attack_Selector : CompositeCow_Attack_Node
{
    public override bool Invoke()
    {
        foreach (var Cow_Attack_Node in GetChildrens())
        {
            if (Cow_Attack_Node.Invoke())
            {

                return true;
            }
        }
        return false;
    }
}

public class Cow_Attack_Sequence : CompositeCow_Attack_Node
{
    public override bool Invoke()
    {
        bool p = false;
        foreach (var Cow_Attack_Node in GetChildrens())
        {
            if (Cow_Attack_Node.Invoke() == false)
            {
                p = true;
            }
        }
        return !p;
    }
}

//행동 

public class Cow_Basic_Attack : Cow_Attack_Node
{
    public Cow_Attack Cow_attack
    {
        set { _Cow_Attack = value; }
    }
    private Cow_Attack _Cow_Attack;
    public override bool Invoke()
    {
        return _Cow_Attack.Cow_Basic_Attack();
    }
}
public class Cow_Special_Attack : Cow_Attack_Node
{
    public Cow_Attack Cow_attack
    {
        set { _Cow_Attack = value; }
    }
    private Cow_Attack _Cow_Attack;
    public override bool Invoke()
    {
        return _Cow_Attack.Cow_Special_Attack();
    }
}
public class Cow_Find_Target : Cow_Attack_Node
{
    public Cow_Attack Cow_attack
    {
        set { _Cow_Attack = value; }
    }
    private Cow_Attack _Cow_Attack;
    public override bool Invoke()
    {
        return _Cow_Attack.Cow_Find_Target();
    }
}