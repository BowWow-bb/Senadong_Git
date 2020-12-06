using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_ch_attackArea : MonoBehaviour
{
    E_ch_Attack E_chicken;
    Chicken_Attack chicken;
    camera_shake Camera;

    bool camera_shake = false;

    // Start is called before the first frame update
    void Start()
    {
        E_chicken = GameObject.FindWithTag("chicken_enemy").GetComponent<E_ch_Attack>();
        chicken = GameObject.FindWithTag("chicken").GetComponent<Chicken_Attack>();
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
        if ((other.gameObject.tag == "chicken") && E_chicken.is_basic_attack)
        { 
            E_chicken.is_Attack = true;
        }
        if(other.gameObject.tag =="chicken_wind")
        {
            Debug.Log("chickenwind");
            //E_chicken.is_basic_attack = false;
            if(E_chicken.is_go_right)//왼쪽에 적이 존재 
            {
                E_chicken.transform.position = new Vector3(E_chicken.transform.position.x + 1.5f, E_chicken.transform.position.y, E_chicken.transform.position.z);
            }
            else//적이 오른쪽 
            {
                E_chicken.transform.position = new Vector3(E_chicken.transform.position.x - 1.5f, E_chicken.transform.position.y, E_chicken.transform.position.z);
            }
            E_chicken.is_basic_attack = false;
        }
        if (other.gameObject.tag == "tiger")
        {
            E_chicken.is_Attack = true;
        }
        if (other.gameObject.tag == "cow")
        {
            E_chicken.is_Attack = true;
        }
    }
    //겹침 방지 
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "chicken")
        {
            //is_Attack: false
            if (other.gameObject.transform.position == E_chicken.transform.position)
            {
                Debug.Log("겹침");
                E_chicken.is_find_target = false;//겹치면 타겟 다시 찾기 
            }
        }
        if (other.gameObject.tag == "tiger")
        {
            if (other.gameObject.transform.position == E_chicken.transform.position)
            {
                E_chicken.is_find_target = false;
            }
        }
        if (other.gameObject.tag == "cow")
        {
            if (other.gameObject.transform.position == E_chicken.transform.position)
            {
                E_chicken.is_find_target = false;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        //if (other.gameObject.tag == "chicken")
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
