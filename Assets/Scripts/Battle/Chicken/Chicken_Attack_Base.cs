using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

//base
public abstract class Chicken_Attack_Node
{
    public abstract bool Invoke();
}

public class CompositeChicken_Attack_Node : Chicken_Attack_Node
{
    public override bool Invoke()
    {
        throw new NotImplementedException();
    }

    public void AddChild(Chicken_Attack_Node Chicken_Attack_Node)
    {
        childrens.Push(Chicken_Attack_Node);
    }

    public Stack<Chicken_Attack_Node> GetChildrens()
    {
        return childrens;
    }
    private Stack<Chicken_Attack_Node> childrens = new Stack<Chicken_Attack_Node>();
}

//composite node
public class Chicken_Attack_Selector : CompositeChicken_Attack_Node
{
    public override bool Invoke()
    {
        foreach (var Chicken_Attack_Node in GetChildrens())
        {
            if (Chicken_Attack_Node.Invoke())
            {

                return true;
            }
        }
        return false;
    }
}

public class Chicken_Attack_Sequence : CompositeChicken_Attack_Node
{
    public override bool Invoke()
    {
        bool p = false;
        foreach (var Chicken_Attack_Node in GetChildrens())
        {
            if (Chicken_Attack_Node.Invoke() == false)
            {
                p = true;
            }
        }
        return !p;
    }
}

//행동 

public class Chicken_Basic_Attack : Chicken_Attack_Node
{
    public Chicken_Attack chicken_attack
    {
        set { _chicken_Attack = value; }
    }
    private Chicken_Attack _chicken_Attack;
    public override bool Invoke()
    {
        return _chicken_Attack.Chicken_Basic_Attack();
    }
}
public class Chicken_Special_Attack : Chicken_Attack_Node
{
    public Chicken_Attack chicken_attack
    {
        set { _chicken_Attack = value; }
    }
    private Chicken_Attack _chicken_Attack;
    public override bool Invoke()
    {
        return _chicken_Attack.Chicken_Special_Attack();
    }
}
public class Chicken_Find_Target : Chicken_Attack_Node
{
    public Chicken_Attack chicken_attack
    {
        set { _chicken_Attack = value; }
    }
    private Chicken_Attack _chicken_Attack;
    public override bool Invoke()
    {
        return _chicken_Attack.Chicken_Find_Target();
    }
}
