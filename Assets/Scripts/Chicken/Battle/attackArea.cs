using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attackArea : MonoBehaviour
{
    Chicken_Attack chicken;
    // Start is called before the first frame update
    void Start()
    {
        chicken = GameObject.FindWithTag("chicken").GetComponent<Chicken_Attack>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag =="chicken_enemy")
        {
            chicken.is_Attack = true;
            Debug.Log("닿음");
        }
        if (other.gameObject.tag == "tiger_enemy")
        {
            chicken.is_Attack = true;
            Debug.Log("닿음");
        }
        if (other.gameObject.tag == "cow_enemy")
        {
            chicken.is_Attack = true;
            Debug.Log("닿음");
        }
    }
}
