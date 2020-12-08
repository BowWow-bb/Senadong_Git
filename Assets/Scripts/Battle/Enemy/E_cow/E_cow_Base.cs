using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

//base
public abstract class E_cow_Attack_Node
{
    public abstract bool Invoke();
}

public class CompositeE_cow_Attack_Node : E_cow_Attack_Node
{
    public override bool Invoke()
    {
        throw new NotImplementedException();
    }

    public void AddChild(E_cow_Attack_Node E_cow_Attack_Node)
    {
        childrens.Push(E_cow_Attack_Node);
    }

    public Stack<E_cow_Attack_Node> GetChildrens()
    {
        return childrens;
    }
    private Stack<E_cow_Attack_Node> childrens = new Stack<E_cow_Attack_Node>();
}

//composite node
public class E_cow_Attack_Selector : CompositeE_cow_Attack_Node
{
    public override bool Invoke()
    {
        foreach (var E_cow_Attack_Node in GetChildrens())
        {
            if (E_cow_Attack_Node.Invoke())
            {

                return true;
            }
        }
        return false;
    }
}

public class E_cow_Attack_Sequence : CompositeE_cow_Attack_Node
{
    public override bool Invoke()
    {
        bool p = false;
        foreach (var E_cow_Attack_Node in GetChildrens())
        {
            if (E_cow_Attack_Node.Invoke() == false)
            {
                p = true;
            }
        }
        return !p;
    }
}

//행동 

public class E_cow_Basic_Attack : E_cow_Attack_Node
{
    public E_cow_Attack E_cow_attack
    {
        set { _E_cow_Attack = value; }
    }
    private E_cow_Attack _E_cow_Attack;
    public override bool Invoke()
    {
        return _E_cow_Attack.E_cow_Basic_Attack();
    }
}

public class E_cow_Find_Target : E_cow_Attack_Node
{
    public E_cow_Attack E_cow_attack
    {
        set { _E_cow_Attack = value; }
    }
    private E_cow_Attack _E_cow_Attack;
    public override bool Invoke()
    {
        return _E_cow_Attack.E_cow_Find_Target();
    }
}