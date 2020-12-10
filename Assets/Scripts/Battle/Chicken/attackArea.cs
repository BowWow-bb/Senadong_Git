using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attackArea : MonoBehaviour
{
    Chicken_Attack chicken;

    E_ch_Attack E_chicken;
    E_cow_Attack E_cow;
    E_t_Attack E_tiger;

    camera_shake Camera;

    int power=100;//후에 공격력으로 수정해주삼요~

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
            E_chicken = GameObject.FindWithTag("chicken_enemy").GetComponent<E_ch_Attack>();
            E_chicken.hpMove(power);

            chicken.is_Attack = true;
        }
        if (other.gameObject.tag == "tiger_enemy" && chicken.is_basic_attack)
        {
            E_tiger = GameObject.FindWithTag("tiger_enemy").GetComponent<E_t_Attack>();
            E_tiger.hpMove(power);

            chicken.is_Attack = true;
        }
        if (other.gameObject.tag == "cow_enemy" && chicken.is_basic_attack)
        {
            E_cow = GameObject.FindWithTag("cow_enemy").GetComponent<E_cow_Attack>();
            E_cow.hpMove(power);

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
    //겹쳤을 경우-> 타겟 다시 찾기 
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "chicken_enemy")
        {
            if (other.gameObject.transform.position == chicken.transform.position)
            {
                chicken.is_find_target = false;
            }
        }
        if (other.gameObject.tag == "cow_enemy")
        {
            if (other.gameObject.transform.position == chicken.transform.position)
            {
                chicken.is_find_target = false;
            }
        }
        if (other.gameObject.tag == "tiger_enemy")
        {
            if (other.gameObject.transform.position == chicken.transform.position)
            {
                chicken.is_find_target = false;
            }
        }
    }
}
