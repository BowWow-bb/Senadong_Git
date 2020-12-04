using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cow_AI : MonoBehaviour
{
    private Cow_Sequence root = new Cow_Sequence();
    private Cow_Selector selector = new Cow_Selector();
    private Cow_Sequence seqInTheFarm = new Cow_Sequence();

    //상태
    private Cow_Hungry hungry = new Cow_Hungry();
    private Cow_Poop poop = new Cow_Poop();
    private Cow_Play play = new Cow_Play();

    //행동
    private Cow_FollowMouse followMouse = new Cow_FollowMouse();
    private Cow_Milk dropmilk = new Cow_Milk();
    private Cow_Follow_Food followFood = new Cow_Follow_Food();
    private Cow_Follow_Milk follow_Milk = new Cow_Follow_Milk();
    private Cow_Follow_Egg follow_Egg = new Cow_Follow_Egg();
    private Cow_Eat eat = new Cow_Eat();
    private Cow_BasicMove basicMove = new Cow_BasicMove();
    private Cow_Quarrel quarrel = new Cow_Quarrel();
    private Cow_Move m_cow;
    private IEnumerator behaviorProcess;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start Tree");

        m_cow = gameObject.GetComponent<Cow_Move>();

        //root에 더함 
        root.AddChild(selector);

        //selector에 더함 
        selector.AddChild(seqInTheFarm);//농장에서 

        hungry.cow = m_cow;
        poop.cow = m_cow;
        play.cow = m_cow;

        followMouse.cow = m_cow;
        dropmilk.cow = m_cow;

        quarrel.cow = m_cow;
        followFood.cow = m_cow;
        follow_Egg.cow = m_cow;
        follow_Milk.cow = m_cow;
        eat.cow = m_cow;
        basicMove.cow = m_cow;


        //seqIntheFarm에 더함 
        //상태들
        seqInTheFarm.AddChild(play);
        seqInTheFarm.AddChild(hungry);
        seqInTheFarm.AddChild(poop);
        //행동들 
        seqInTheFarm.AddChild(dropmilk);
        seqInTheFarm.AddChild(followMouse);
        seqInTheFarm.AddChild(followFood);
        seqInTheFarm.AddChild(follow_Milk);
        seqInTheFarm.AddChild(follow_Egg);
        seqInTheFarm.AddChild(quarrel);
        //seqInTheFarm.AddChild(eat);
        seqInTheFarm.AddChild(basicMove);

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
