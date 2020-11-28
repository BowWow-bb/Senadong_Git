using System.Collections;
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
    private Eat eat = new Eat();
    private BasicMove basicMove = new BasicMove();

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
        eat.tiger = m_tiger;
        basicMove.tiger = m_tiger;

        seqInTheFarm.AddChild(hungry);
        seqInTheFarm.AddChild(poop);
        seqInTheFarm.AddChild(play);
        seqInTheFarm.AddChild(followMouse);
        seqInTheFarm.AddChild(eat);
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
