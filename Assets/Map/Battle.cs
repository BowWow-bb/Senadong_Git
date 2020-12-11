using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Battle : MonoBehaviour
{
    GameObject cow;
    GameObject tiger;
    GameObject chicken;

    Chicken_Move cc;
    Cow_Move c;
    Tiger_Move t;

    ItemManager item_manager;

    E_AttackData e_attack_data;
    // Start is called before the first frame update
    void Start()
    {
        cow = GameObject.FindWithTag("cow");
        tiger = GameObject.FindWithTag("tiger");
        chicken = GameObject.FindWithTag("chicken");

        cc= chicken.GetComponent<Chicken_Move>();
        c= cow.GetComponent<Cow_Move>();
        t= tiger.GetComponent<Tiger_Move>();

        item_manager = GameObject.Find("ItemManager").GetComponent<ItemManager>();

        e_attack_data = GameObject.Find("E_AttackData").GetComponent<E_AttackData>();
    }

    // Update is called once per frame
    public void OnClick()
    {
        //적군 공격력 증가 중지(전투 시작을 알림)
        e_attack_data.isBattle = true;

        if (item_manager.coin >= 2000)
        {
            if(!item_manager.chicken_die)
            {
                item_manager.cc_exp = cc.exp;
                item_manager.cc_hungry = cc.hungry;
                item_manager.cc_poop = cc.poop;
                item_manager.cc_play = cc.play;
                item_manager.cc_i++;
                Destroy(chicken);
            }
            if (!item_manager.cow_die)
            {
                item_manager.c_exp = c.exp;
                item_manager.c_hungry = c.hungry;
                item_manager.c_poop = c.poop;
                item_manager.c_play = c.play;
                item_manager.c_i++;
                Destroy(cow);
            }
            if (!item_manager.tiger_die)
            {
                item_manager.t_exp = t.exp;
                item_manager.t_hungry = t.hungry;
                item_manager.t_poop = t.poop;
                item_manager.t_play = t.play;
                item_manager.t_i++;
                Destroy(tiger);
            }
            SceneManager.LoadScene("Battle_Scene");
        }

        //error msg floating(2, 000 코인 이상 전장진출 가능)
        else
        {
            GameObject.Find("Canvas").transform.GetChild(3).gameObject.SetActive(true);
            StartCoroutine(delay(2.0f));
        }

    }

    IEnumerator delay(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        GameObject.Find("Canvas").transform.GetChild(3).gameObject.SetActive(false);
    }
}
