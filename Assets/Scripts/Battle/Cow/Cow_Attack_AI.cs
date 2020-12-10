using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cow_Attack_AI : MonoBehaviour
{
    private Cow_Attack_Sequence root = new Cow_Attack_Sequence();
    private Cow_Attack_Selector selector = new Cow_Attack_Selector();

    private Cow_Attack_Sequence seqBehavior = new Cow_Attack_Sequence();

    //행동
    private Cow_Basic_Attack basic_Attack = new Cow_Basic_Attack();
    private Cow_Special_Attack special_Attack = new Cow_Special_Attack();
    private Cow_Find_Target find_Target = new Cow_Find_Target();

    private Cow_Attack a_Cow;

    private IEnumerator behaviorProcess;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start Tree");

        a_Cow = gameObject.GetComponent<Cow_Attack>();

        //root에 더함 
        root.AddChild(selector);

        //selector에 더함 
        selector.AddChild(seqBehavior);//행동

        find_Target.Cow_attack = a_Cow;
        basic_Attack.Cow_attack = a_Cow;
        special_Attack.Cow_attack = a_Cow;

        //행동들
        seqBehavior.AddChild(find_Target);
        seqBehavior.AddChild(basic_Attack);
        seqBehavior.AddChild(special_Attack);


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
