using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_Data : MonoBehaviour
{
    int level, attack_value;

    GameObject c, co, t;
    GameObject Ec, Eco, Et;

    // Start is called before the first frame update
    void Start()
    {
        //캐릭터 오브젝트 
        c = GameObject.FindWithTag("chicken").gameObject;
        co = GameObject.FindWithTag("cow").gameObject;
        t = GameObject.FindWithTag("tiger").gameObject;

        //적 오브젝트
        Ec = GameObject.FindWithTag("chicken_enemy").gameObject;
        Eco = GameObject.FindWithTag("cow_enemy").gameObject;
        Et = GameObject.FindWithTag("tiger_enemy").gameObject;

    }
    private void FixedUpdate()
    {
        if(c == null && co == null && t == null)    //캐릭터가 모두 사망한 경우
        {
            GameObject.Find("Canvas").transform.Find("Lose").gameObject.SetActive(true);
        }

        if (Ec == null && Eco == null && Et == null)    //적이 모두 사망한 경우
        {
            GameObject.Find("Canvas").transform.Find("Win").gameObject.SetActive(true);
        }

    }
    // Update is called once per frame
    public int getAttackValue(int level)
    {
        int attack_value;
        attack_value = level * 10;
        return attack_value;
    }
}
