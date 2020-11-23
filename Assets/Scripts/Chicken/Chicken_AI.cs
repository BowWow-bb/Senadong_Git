using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chicken_AI : MonoBehaviour
{
    private Chicken_Sequence root = new Chicken_Sequence();
    private Chicken_Selector selector = new Chicken_Selector();
    private Chicken_Sequence seqInTheFarm = new Chicken_Sequence();

    //상태 3가지 
    private Chicken_Hungry hungry = new Chicken_Hungry();
    private Chicken_Poop poop = new Chicken_Poop();
    private Chicken_Play play = new Chicken_Play();
    //행동
    private Chicken_Egg dropegg = new Chicken_Egg();
    private Chicken_FollowMouse followMouse = new Chicken_FollowMouse();
    private Chicken_Eat eat = new Chicken_Eat();
    private Chicken_BasicMove basicMove = new Chicken_BasicMove();

    private Chicken_Move m_chicken;
    private IEnumerator behaviorProcess;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start Tree");

        m_chicken = gameObject.GetComponent<Chicken_Move>();

        //root에 더함 
        root.AddChild(selector);

        //selector에 더함 
        selector.AddChild(seqInTheFarm);//농장에서 

        hungry.chicken = m_chicken;
        poop.chicken = m_chicken;
        play.chicken = m_chicken;

        followMouse.chicken = m_chicken;
        dropegg.chicken = m_chicken;
        eat.chicken = m_chicken;
        basicMove.chicken = m_chicken;


        //seqIntheFarm에 더함 
        //상태들
        seqInTheFarm.AddChild(play);
        seqInTheFarm.AddChild(hungry);
        seqInTheFarm.AddChild(poop);
        //행동들 
        seqInTheFarm.AddChild(dropegg);
        //seqInTheFarm.AddChild(followMouse);
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
