using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

//base
public abstract class E_t_Attack_Node
{
    public abstract bool Invoke();
}

public class CompositeE_t_Attack_Node : E_t_Attack_Node
{
    public override bool Invoke()
    {
        throw new NotImplementedException();
    }

    public void AddChild(E_t_Attack_Node E_t_Attack_Node)
    {
        childrens.Push(E_t_Attack_Node);
    }

    public Stack<E_t_Attack_Node> GetChildrens()
    {
        return childrens;
    }
    private Stack<E_t_Attack_Node> childrens = new Stack<E_t_Attack_Node>();
}

//composite node
public class E_t_Attack_Selector : CompositeE_t_Attack_Node
{
    public override bool Invoke()
    {
        foreach (var E_t_Attack_Node in GetChildrens())
        {
            if (E_t_Attack_Node.Invoke())
            {

                return true;
            }
        }
        return false;
    }
}

public class E_t_Attack_Sequence : CompositeE_t_Attack_Node
{
    public override bool Invoke()
    {
        bool p = false;
        foreach (var E_t_Attack_Node in GetChildrens())
        {
            if (E_t_Attack_Node.Invoke() == false)
            {
                p = true;
            }
        }
        return !p;
    }
}

//행동 

public class E_t_Basic_Attack : E_t_Attack_Node
{
    public E_t_Attack E_t_attack
    {
        set { _E_t_Attack = value; }
    }
    private E_t_Attack _E_t_Attack;
    public override bool Invoke()
    {
        return _E_t_Attack.E_t_Basic_Attack();
    }
}

public class E_t_Find_Target : E_t_Attack_Node
{
    public E_t_Attack E_t_attack
    {
        set { _E_t_Attack = value; }
    }
    private E_t_Attack _E_t_Attack;
    public override bool Invoke()
    {
        return _E_t_Attack.E_t_Find_Target();
    }
}