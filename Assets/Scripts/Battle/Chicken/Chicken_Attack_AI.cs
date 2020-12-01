using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chicken_Attack_AI : MonoBehaviour
{
    private Chicken_Attack_Sequence root = new Chicken_Attack_Sequence();
    private Chicken_Attack_Selector selector = new Chicken_Attack_Selector();

    private Chicken_Attack_Sequence seqBehavior = new Chicken_Attack_Sequence();

    //행동
    private Chicken_Basic_Attack basic_Attack = new Chicken_Basic_Attack();
    private Chicken_Find_Target find_Target = new Chicken_Find_Target();

    private Chicken_Attack a_chicken;

    private IEnumerator behaviorProcess;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start Tree");

        a_chicken = gameObject.GetComponent<Chicken_Attack>();

        //root에 더함 
        root.AddChild(selector);

        //selector에 더함 
        selector.AddChild(seqBehavior);//행동

        find_Target.chicken_attack = a_chicken;
        basic_Attack.chicken_attack = a_chicken;

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
