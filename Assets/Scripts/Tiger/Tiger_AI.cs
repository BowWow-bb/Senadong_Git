﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiger_AI : MonoBehaviour
{
    private Sequence root = new Sequence();
    private Selector selector = new Selector();
    private Sequence seqInTheFarm = new Sequence();

    private Hungry hungry = new Hungry();
    private Poop poop = new Poop();
    private Play play = new Play();
    private FollowMouse followMouse = new FollowMouse();

    private Tiger_Follow_Food follow_Food = new Tiger_Follow_Food();
    private Tiger_Follow_Milk follow_Milk = new Tiger_Follow_Milk();
    private Tiger_Follow_Egg follow_Egg = new Tiger_Follow_Egg();

    private Eat eat = new Eat();
    private BasicMove basicMove = new BasicMove();
    private Quarrel quarrel = new Quarrel();
    private Tiger_Move m_tiger;
    private IEnumerator behaviorProcess;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start Tree");
        m_tiger = gameObject.GetComponent<Tiger_Move>();
        root.AddChild(selector);
        selector.AddChild(seqInTheFarm);

        hungry.tiger = m_tiger;
        poop.tiger = m_tiger;
        play.tiger = m_tiger;
        followMouse.tiger = m_tiger;
        follow_Food.tiger = m_tiger;
        follow_Egg.tiger = m_tiger;
        follow_Milk.tiger = m_tiger;
        eat.tiger = m_tiger;
        basicMove.tiger = m_tiger;
        quarrel.tiger = m_tiger;

        seqInTheFarm.AddChild(hungry);
        seqInTheFarm.AddChild(poop);
        seqInTheFarm.AddChild(play);
        seqInTheFarm.AddChild(followMouse);

        seqInTheFarm.AddChild(follow_Food);
        seqInTheFarm.AddChild(follow_Milk);
        seqInTheFarm.AddChild(follow_Egg);

        seqInTheFarm.AddChild(eat);
        seqInTheFarm.AddChild(basicMove);
        seqInTheFarm.AddChild(quarrel);

        behaviorProcess = BehaviorProcess();
        StartCoroutine(behaviorProcess);

    }
    public IEnumerator BehaviorProcess()
    {
        while (root.Invoke())
        {
            yield return new WaitForEndOfFrame();
        }
        Destroy(gameObject, 0.0f);
        Debug.Log("behavior process exit");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
