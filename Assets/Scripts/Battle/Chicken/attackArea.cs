using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attackArea : MonoBehaviour
{
    Chicken_Attack chicken;
    camera_shake Camera;

    bool camera_shake = false;

    // Start is called before the first frame update
    void Start()
    {
        chicken = GameObject.FindWithTag("chicken").GetComponent<Chicken_Attack>();
        Camera = GameObject.FindWithTag("MainCamera").GetComponent<camera_shake>();
    }

    // Update is called once per frame
    void Update()
    {
        if(camera_shake)
        {
            Camera.cameraOn = true;
            Camera.shake = 0.3f;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        //적이랑 닿으면 camera 움직임 
        if((other.gameObject.tag =="chicken_enemy")&&chicken.is_basic_attack)
        {
            chicken.is_Attack = true;
        }
        if (other.gameObject.tag == "tiger_enemy")
        {
            chicken.is_Attack = true;
        }
        if (other.gameObject.tag == "cow_enemy")
        {
            chicken.is_Attack = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "chicken_enemy")
        {
            camera_shake = false;
        }
        if (other.gameObject.tag == "tiger_enemy")
        {
            camera_shake = false;
        }
        if (other.gameObject.tag == "cow_enemy")
        {
            camera_shake = false;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "chicken_enemy")
        {
            if (other.gameObject.transform.position == chicken.transform.position)
            {
                if(chicken.is_go_right)//적이 왼쪽 
                {
                    chicken.transform.position = new Vector3(chicken.transform.position.x + 1, chicken.transform.position.y, chicken.transform.position.z);
                }
                else
                {
                    chicken.transform.position = new Vector3(chicken.transform.position.x - 1, chicken.transform.position.y, chicken.transform.position.z);
                }
            }
        }
    }
}
