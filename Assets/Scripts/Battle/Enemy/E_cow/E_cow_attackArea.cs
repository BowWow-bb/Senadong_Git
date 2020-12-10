using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_cow_attackArea : MonoBehaviour
{
    E_cow_Attack E_cow;

    Chicken_Attack chicken;
    Cow_Attack cow;
    camera_shake Camera;

    int power = 100;//후에 공격력 

    bool camera_shake = false;

    // Start is called before the first frame update
    void Start()
    {
        E_cow = GameObject.FindWithTag("cow_enemy").GetComponent<E_cow_Attack>();
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
        //적이랑 닿으면 camera 움직임 
        if ((other.gameObject.tag == "cow") && E_cow.is_basic_attack)
        {
            cow = GameObject.FindWithTag("cow").GetComponent<Cow_Attack>();
            cow.hpMove(power);

            E_cow.is_Attack = true;
        }
        if (other.gameObject.tag == "tiger" && E_cow.is_basic_attack)
        {
            E_cow.is_Attack = true;
        }
        if (other.gameObject.tag == "chicken" && E_cow.is_basic_attack)
        {
            chicken = GameObject.FindWithTag("chicken").GetComponent<Chicken_Attack>();
            chicken.hpMove(power);

            E_cow.is_Attack = true;
        }
    }
    //겹침 방지 
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "cow")
        {
            //is_Attack: false
            if (other.gameObject.transform.position == E_cow.transform.position)
            {
                Debug.Log("겹침");
                E_cow.is_find_target = false;//겹치면 타겟 다시 찾기 
            }
        }
        if (other.gameObject.tag == "tiger")
        {
            if (other.gameObject.transform.position == E_cow.transform.position)
            {
                E_cow.is_find_target = false;
            }
        }
        if (other.gameObject.tag == "chicken")
        {
            if (other.gameObject.transform.position == E_cow.transform.position)
            {
                E_cow.is_find_target = false;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        //if (other.gameObject.tag == "cow")
        //{
        //    camera_shake = false;
        //}
        //if (other.gameObject.tag == "tiger")
        //{
        //    camera_shake = false;
        //}
        //if (other.gameObject.tag == "cow")
        //{
        //    camera_shake = false;
        //}
    }
}
