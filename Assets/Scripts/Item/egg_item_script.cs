using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class egg_item_script : MonoBehaviour
{
    ItemManager item_manager;
    Chicken_Move c_m;
    Tiger_Move t_m;
    Cow_Move co_m;

    public int full = 100;//음식먹고 얻는 수치 (hungry)

    // Start is called before the first frame update
    void Start()
    {
        item_manager = GameObject.Find("ItemManager").GetComponent<ItemManager>();
        if (!item_manager.chicken_die)
        {
            c_m = GameObject.FindWithTag("chicken").GetComponent<Chicken_Move>();
        }
        if (!item_manager.cow_die)
        {
            co_m = GameObject.FindWithTag("cow").GetComponent<Cow_Move>();
        }
        if (!item_manager.tiger_die)
        {
            t_m = GameObject.FindWithTag("tiger").GetComponent<Tiger_Move>();
        }
        item_manager.egg_item--;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "chicken" && c_m.is_follow_egg)
        {
            Debug.Log("치킨이 먹음");
            c_m.isHungry = false;
            c_m.fHungry.SetActive(false);
            if (c_m.hungry + 100 > c_m.valueMax) c_m.hungry = c_m.valueMax;
            else c_m.hungry += 100;
            if (c_m.exp + 100 > c_m.valueMax) c_m.exp = c_m.valueMax;
            else c_m.exp += 100;
            Destroy(gameObject);
        }
        if (other.gameObject.tag == "cow" && co_m.is_follow_egg)
        {
            Debug.Log("소가먹음");
            co_m.isHungry = false;
            co_m.fHungry.SetActive(false);
            if (co_m.hungry + 100 > co_m.valueMax) co_m.hungry = co_m.valueMax;
            else co_m.hungry += 100;
            if (co_m.exp + 100 > co_m.valueMax) co_m.exp = co_m.valueMax;
            else co_m.exp += 100;
            Destroy(gameObject);
        }
        if (other.gameObject.tag == "tiger" && t_m.is_follow_egg)
        {
            Debug.Log("호랑이가 먹음");
            t_m.isHungry = false;
            t_m.fHungry.SetActive(false);
            if (t_m.hungry + 100 > t_m.valueMax) t_m.hungry = t_m.valueMax;
            else t_m.hungry += 100;
            if (t_m.exp + 100 > t_m.valueMax) t_m.exp = t_m.valueMax;
            else t_m.exp += 100;
            Destroy(gameObject);
        }
    }
}
