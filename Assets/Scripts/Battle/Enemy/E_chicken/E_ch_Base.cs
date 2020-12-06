using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

//base
public abstract class E_chicken_Attack_Node
{
    public abstract bool Invoke();
}

public class CompositeE_chicken_Attack_Node : E_chicken_Attack_Node
{
    public override bool Invoke()
    {
        throw new NotImplementedException();
    }

    public void AddChild(E_chicken_Attack_Node E_chicken_Attack_Node)
    {
        childrens.Push(E_chicken_Attack_Node);
    }

    public Stack<E_chicken_Attack_Node> GetChildrens()
    {
        return childrens;
    }
    private Stack<E_chicken_Attack_Node> childrens = new Stack<E_chicken_Attack_Node>();
}

//composite node
public class E_chicken_Attack_Selector : CompositeE_chicken_Attack_Node
{
    public override bool Invoke()
    {
        foreach (var E_chicken_Attack_Node in GetChildrens())
        {
            if (E_chicken_Attack_Node.Invoke())
            {

                return true;
            }
        }
        return false;
    }
}

public class E_chicken_Attack_Sequence : CompositeE_chicken_Attack_Node
{
    public override bool Invoke()
    {
        bool p = false;
        foreach (var E_chicken_Attack_Node in GetChildrens())
        {
            if (E_chicken_Attack_Node.Invoke() == false)
            {
                p = true;
            }
        }
        return !p;
    }
}

//행동 

public class E_chicken_Basic_Attack : E_chicken_Attack_Node
{
    public E_ch_Attack E_chicken_attack
    {
        set { _E_chicken_Attack = value; }
    }
    private E_ch_Attack _E_chicken_Attack;
    public override bool Invoke()
    {
        return _E_chicken_Attack.E_ch_Basic_Attack();
    }
}

public class E_chicken_Find_Target : E_chicken_Attack_Node
{
    public E_ch_Attack E_chicken_attack
    {
        set { _E_chicken_Attack = value; }
    }
    private E_ch_Attack _E_chicken_Attack;
    public override bool Invoke()
    {
        return _E_chicken_Attack.E_ch_Find_Target();
    }
}