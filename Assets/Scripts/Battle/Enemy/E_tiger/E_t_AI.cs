using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_t_AI : MonoBehaviour
{
    private E_t_Attack_Sequence root = new E_t_Attack_Sequence();
    private E_t_Attack_Selector selector = new E_t_Attack_Selector();

    private E_t_Attack_Sequence seqBehavior = new E_t_Attack_Sequence();

    //행동
    private E_t_Basic_Attack basic_Attack = new E_t_Basic_Attack();
    private E_t_Find_Target find_Target = new E_t_Find_Target();

    private E_t_Attack a_E_t;

    private IEnumerator behaviorProcess;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start Tree");

        a_E_t = gameObject.GetComponent<E_t_Attack>();

        //root에 더함 
        root.AddChild(selector);

        //selector에 더함 
        selector.AddChild(seqBehavior);//행동

        find_Target.E_t_attack = a_E_t;
        basic_Attack.E_t_attack = a_E_t;

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
