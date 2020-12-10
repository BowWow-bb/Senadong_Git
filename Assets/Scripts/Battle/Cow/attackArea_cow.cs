using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attackArea_cow : MonoBehaviour
{
    Cow_Attack cow;

    E_ch_Attack E_chicken;
    E_cow_Attack E_cow;

    camera_shake Camera;

    int power = 100;//후에 공격력 

    bool camera_shake = false;

    // Start is called before the first frame update
    void Start()
    {
        cow = GameObject.FindWithTag("cow").GetComponent<Cow_Attack>();
        Camera = GameObject.FindWithTag("MainCamera").GetComponent<camera_shake>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (camera_shake)
        //{
        //    Camera.cameraOn = true;
        //    Camera.shake = 0.3f;
        //}
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "chicken_enemy" && cow.is_basic_attack)
        {
            E_chicken = GameObject.FindWithTag("chicken_enemy").GetComponent<E_ch_Attack>();
            E_chicken.hpMove(power);

            cow.is_Attack = true;
            camera_shake = true;
        }
        if (other.gameObject.tag == "tiger_enemy" && cow.is_basic_attack)
        {
            cow.is_Attack = true;
            camera_shake = true;
        }
        if (other.gameObject.tag == "cow_enemy" && cow.is_basic_attack)
        {
            E_cow = GameObject.FindWithTag("cow_enemy").GetComponent<E_cow_Attack>();
            E_cow.hpMove(power);

            cow.is_Attack = true;
            camera_shake = true;
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
    //겹쳤을 경우-> 타겟 다시 찾기 
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "chicken_enemy")
        {
            if (other.gameObject.transform.position == cow.transform.position)
            {
                cow.is_find_target = false;
            }
        }
        if (other.gameObject.tag == "cow_enemy")
        {
            if (other.gameObject.transform.position == cow.transform.position)
            {
                cow.is_find_target = false;
            }
        }
        if (other.gameObject.tag == "tiger_enemy")
        {
            if (other.gameObject.transform.position == cow.transform.position)
            {
                cow.is_find_target = false;
            }
        }
    }
}
