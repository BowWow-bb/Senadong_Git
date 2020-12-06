using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_ch_AI : MonoBehaviour
{
    private E_chicken_Attack_Sequence root = new E_chicken_Attack_Sequence();
    private E_chicken_Attack_Selector selector = new E_chicken_Attack_Selector();

    private E_chicken_Attack_Sequence seqBehavior = new E_chicken_Attack_Sequence();

    //행동
    private E_chicken_Basic_Attack basic_Attack = new E_chicken_Basic_Attack();
    private E_chicken_Find_Target find_Target = new E_chicken_Find_Target();

    private E_ch_Attack a_E_chicken;

    private IEnumerator behaviorProcess;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start Tree");

        a_E_chicken = gameObject.GetComponent<E_ch_Attack>();

        //root에 더함 
        root.AddChild(selector);

        //selector에 더함 
        selector.AddChild(seqBehavior);//행동

        find_Target.E_chicken_attack = a_E_chicken;
        basic_Attack.E_chicken_attack = a_E_chicken;

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
