using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hungry_item : MonoBehaviour
{
    Cow_Move cow;
    Chicken_Move chicken;
    //Tiger_Move tiger;

    public float distance = 10;//인식할 범위 
    float distance_cow = 0;
    float distance_chicken = 0;
    float distance_tiger = 0;

    // Start is called before the first frame update
    void Start()
    {
        //cow = GameObject.Find("Cow").GetComponent<Cow_Move>();
        //chicken = GameObject.Find("Chicken").GetComponent<Chicken_Move>();
        //tiger = GameObject.Find("tiger").GetComponent<Tiger_Move>();
    }

    //    // Update is called once per frame
    //    void Update()
    //    {
    //        //거리 체
    //        distance_cow = Vector3.Distance(cow.transform.position, this.gameObject.transform.position);
    //        distance_chicken = Vector3.Distance(chicken.transform.position, this.gameObject.transform.position);
    //        //distance_tiger = Vector3.Distance(tiger.transform.position, this.gameObject.transform.position);

    //        if(distance_cow<distance)
    //        {
    //            //cow_follow_food = true;
    //        }
    //        else
    //        {
    //            //cow_follow_food = false;
    //        }

    //        if (distance_chicken< distance)
    //        {
    //            chicken.is_follow_food = true;
    //        }
    //        else
    //        {
    //            chicken.is_follow_food = false;
    //        }

    //        if (distance_tiger < distance)
    //        {
    //            //tiger_follow_food = true;
    //        }
    //        else
    //        {
    //            //tiger_follow_food = false;
    //        }

    //    }
}
