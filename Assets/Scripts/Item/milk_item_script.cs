using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class milk_item_script : MonoBehaviour
{
    ItemManager item_manager;
    Chicken_Move c_m;
    Tiger_Move t_m;
    Cow_Move co_m;

    public int full = 100;//음식먹고 얻는 수치 (hungry)

    // Start is called before the first frame update
    void Start()
    {
        item_manager = GameObject.Find("Main Camera").GetComponent<ItemManager>();
        c_m = GameObject.FindWithTag("chicken").GetComponent<Chicken_Move>();
        t_m = GameObject.FindWithTag("tiger").GetComponent<Tiger_Move>();
        co_m = GameObject.FindWithTag("cow").GetComponent<Cow_Move>();
        item_manager.milk_item--;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "chicken" && c_m.is_follow_milk)
        {
            Debug.Log("치킨이 먹음");
            c_m.isHungry = false;
            c_m.fHungry.SetActive(false);
            if (c_m.hungry + full >= c_m.valueMax)
            {
                c_m.hungry = c_m.valueMax;
            }
            else
            {
                c_m.hungry += full;
            }
            Destroy(gameObject);
        }
        if (other.gameObject.tag == "cow" && co_m.is_follow_milk)
        {
            Debug.Log("소가먹음");
            co_m.isHungry = false;
            co_m.fHungry.SetActive(false);
            if (co_m.hungry + full >= co_m.valueMax)
            {
                co_m.hungry = co_m.valueMax;
            }
            else
            {
                co_m.hungry += full;
            }
            Destroy(gameObject);
        }
        if (other.gameObject.tag == "tiger" && t_m.is_follow_milk)
        {
            Debug.Log("호랑이가 먹음");
            t_m.isHungry = false;
            t_m.fHungry.SetActive(false);
            if (t_m.hungry + full >= t_m.valueMax)
            {
                t_m.hungry = t_m.valueMax;
            }
            else
            {
                t_m.hungry += full;
            }
            Destroy(gameObject);
        }
    }
}
