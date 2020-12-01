using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiger_Attack_AI : MonoBehaviour
{
    private Tiger_Attack_Sequence root = new Tiger_Attack_Sequence();
    private Tiger_Attack_Selector selector = new Tiger_Attack_Selector();

    private Tiger_Attack_Sequence seqBehavior = new Tiger_Attack_Sequence();

    //행동
    private Tiger_Basic_Attack basic_Attack = new Tiger_Basic_Attack();
    private Tiger_Find_Target find_Target = new Tiger_Find_Target();

    private Tiger_Attack a_Tiger;

    private IEnumerator behaviorProcess;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start Tree");

        a_Tiger = gameObject.GetComponent<Tiger_Attack>();

        //root에 더함 
        root.AddChild(selector);

        //selector에 더함 
        selector.AddChild(seqBehavior);//행동

        find_Target.Tiger_attack = a_Tiger;
        basic_Attack.Tiger_attack = a_Tiger;

        //행동들
        seqBehavior.AddChild(find_Target);
        seqBehavior.AddChild(basic_Attack);


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
