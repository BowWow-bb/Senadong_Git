using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Store : MonoBehaviour
{
    GameObject cow;
    GameObject tiger;
    GameObject chicken;

    Chicken_Move cc;
    Cow_Move c;

    Tiger_Move t;
    GameObject ItemSlot;
    Vector3 Pos;
    ItemManager item_manager;

    // Start is called before the first frame update
    void Start()
    {
        ItemSlot = GameObject.Find("ItemSlot");
        Pos = ItemSlot.transform.position;//상점 가기 전 위치 
        item_manager = GameObject.Find("ItemManager").GetComponent<ItemManager>();

        cow = GameObject.FindWithTag("cow");
        tiger = GameObject.FindWithTag("tiger");
        chicken = GameObject.FindWithTag("chicken");
    }

    // Update is called once per frame
    public void OnClick()
    {
        ItemSlot.transform.position = new Vector3(Pos.x + 10, Pos.y, Pos.z);
        if (!item_manager.chicken_die)
        {
            cc = chicken.GetComponent<Chicken_Move>();
            item_manager.cc_exp = cc.exp;
            item_manager.cc_hungry = cc.hungry;
            item_manager.cc_poop = cc.poop;
            item_manager.cc_play = cc.play;
            item_manager.cc_i++;
            Destroy(chicken);
        }
        if (!item_manager.cow_die)
        {
            c = cow.GetComponent<Cow_Move>();
            item_manager.c_exp = c.exp;
            item_manager.c_hungry = c.hungry;
            item_manager.c_poop = c.poop;
            item_manager.c_play = c.play;
            item_manager.c_i++;
            Destroy(cow);
        }
        if (!item_manager.tiger_die)
        {
            t = tiger.GetComponent<Tiger_Move>();
            item_manager.t_exp = t.exp;
            item_manager.t_hungry = t.hungry;
            item_manager.t_poop = t.poop;
            item_manager.t_play = t.play;
            item_manager.t_i++;
            Destroy(tiger);
        }
        SceneManager.LoadScene("Store_Scene");
    }
}
