using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

//base
public abstract class Tiger_Attack_Node
{
    public abstract bool Invoke();
}

public class CompositeTiger_Attack_Node : Tiger_Attack_Node
{
    public override bool Invoke()
    {
        throw new NotImplementedException();
    }

    public void AddChild(Tiger_Attack_Node Tiger_Attack_Node)
    {
        childrens.Push(Tiger_Attack_Node);
    }

    public Stack<Tiger_Attack_Node> GetChildrens()
    {
        return childrens;
    }
    private Stack<Tiger_Attack_Node> childrens = new Stack<Tiger_Attack_Node>();
}

//composite node
public class Tiger_Attack_Selector : CompositeTiger_Attack_Node
{
    public override bool Invoke()
    {
        foreach (var Tiger_Attack_Node in GetChildrens())
        {
            if (Tiger_Attack_Node.Invoke())
            {

                return true;
            }
        }
        return false;
    }
}

public class Tiger_Attack_Sequence : CompositeTiger_Attack_Node
{
    public override bool Invoke()
    {
        bool p = false;
        foreach (var Tiger_Attack_Node in GetChildrens())
        {
            if (Tiger_Attack_Node.Invoke() == false)
            {
                p = true;
            }
        }
        return !p;
    }
}

//행동 

public class Tiger_Basic_Attack : Tiger_Attack_Node
{
    public Tiger_Attack Tiger_attack
    {
        set { _Tiger_Attack = value; }
    }
    private Tiger_Attack _Tiger_Attack;
    public override bool Invoke()
    {
        return _Tiger_Attack.Tiger_Basic_Attack();
    }
}
public class Tiger_Find_Target : Tiger_Attack_Node
{
    public Tiger_Attack Tiger_attack
    {
        set { _Tiger_Attack = value; }
    }
    private Tiger_Attack _Tiger_Attack;
    public override bool Invoke()
    {
        return _Tiger_Attack.Tiger_Find_Target();
    }
}