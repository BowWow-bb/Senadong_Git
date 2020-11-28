using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chicken_AI : MonoBehaviour
{
    private Chicken_Sequence root = new Chicken_Sequence();
    private Chicken_Selector selector = new Chicken_Selector();

    private Chicken_Sequence seqBehavior = new Chicken_Sequence();

    //상태
    private Chicken_Hungry hungry = new Chicken_Hungry();
    private Chicken_Poop poop = new Chicken_Poop();
    private Chicken_Play play = new Chicken_Play();

    //행동
    private Chicken_Follow_Food followFood = new Chicken_Follow_Food();
    private Chicken_FollowMouse followMouse = new Chicken_FollowMouse();
    private Chicken_Drop_Egg drop_Egg = new Chicken_Drop_Egg();
    private Chicken_Eat eat = new Chicken_Eat();
    private Chicken_BasicMove basicMove = new Chicken_BasicMove();

    private Chicken_Move m_chicken;
    //private Chicken_Status s_chicken;

    private IEnumerator behaviorProcess;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start Tree");

        m_chicken = gameObject.GetComponent<Chicken_Move>();

        //root에 더함 
        root.AddChild(selector);

        //selector에 더함 
        selector.AddChild(seqBehavior);//행동


        hungry.chicken = m_chicken;
        poop.chicken = m_chicken;
        play.chicken = m_chicken;

        followMouse.chicken = m_chicken;
        followFood.chicken = m_chicken;
        drop_Egg.chicken = m_chicken;
        eat.chicken = m_chicken;
        basicMove.chicken = m_chicken;

        //상태들
        seqBehavior.AddChild(play);
        seqBehavior.AddChild(hungry);
        seqBehavior.AddChild(poop);
        //행동들 
        seqBehavior.AddChild(drop_Egg);
        seqBehavior.AddChild(followMouse);
        //seqBehavior.AddChild(eat);
        seqBehavior.AddChild(basicMove);
        seqBehavior.AddChild(followFood);

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
