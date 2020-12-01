using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hungry_item_script : MonoBehaviour
{
    Chicken_Move c_m;
    Tiger_Move t_m;
    Cow_Move co_m;

    public int full=100;//음식먹고 얻는 수치 (hungry)

    // Start is called before the first frame update
    void Start()
    {
        c_m = GameObject.FindWithTag("chicken").GetComponent<Chicken_Move>();
        t_m = GameObject.FindWithTag("tiger").GetComponent<Tiger_Move>();
        co_m = GameObject.FindWithTag("cow").GetComponent<Cow_Move>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "chicken" && c_m.is_follow_food)
        {
            Debug.Log("치킨이 먹음");
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
        if(other.gameObject.tag == "cow" && co_m.is_follow_food)
        {
            Debug.Log("소가먹음");
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
        if (other.gameObject.tag == "tiger" && t_m.is_follow_food)
        {
            Debug.Log("호랑이가 먹음");
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
    private void OnTriggerStay(Collider other)
    {
        //if (other.gameObject.tag == "chicken")
        //{
        //    Debug.Log("치킨이 먹음");
        //    Destroy(gameObject);
        //}
        //if (other.gameObject.tag == "cow")
        //{
        //    Debug.Log("소가먹음");
        //    Destroy(gameObject);
        //}
        //if (other.gameObject.tag == "tiger")
        //{
        //    Debug.Log("호랑이가 먹음");
        //    Destroy(gameObject);
        //}
    }
}
