using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_t_AttackArea : MonoBehaviour
{
    E_t_Attack E_t;

    Chicken_Attack chicken;
    Cow_Attack cow;
    Tiger_Attack tiger;

    //int power = 100;//후에 공격력 

    camera_shake Camera;

    bool camera_shake = false;

    // Start is called before the first frame update
    void Start()
    {
        E_t = GameObject.FindWithTag("tiger_enemy").GetComponent<E_t_Attack>();
        Camera = GameObject.FindWithTag("MainCamera").GetComponent<camera_shake>();
        cow = GameObject.FindWithTag("cow").GetComponent<Cow_Attack>();
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
        //공격 함 
        //적이랑 닿으면 camera 움직임 
        if ((other.gameObject.tag == "cow") && E_t.is_basic_attack && !cow.is_special_attack_time)//소가 고유공격 안할때 데미지 줌
        {
            //cow = GameObject.FindWithTag("cow").GetComponent<Cow_Attack>();
            cow.hpMove(E_t.attack);

            E_t.is_Attack = true;
        }
        if ((other.gameObject.tag == "tiger") && E_t.is_basic_attack)
        {
            tiger = GameObject.FindWithTag("tiger").GetComponent<Tiger_Attack>();
            tiger.hpMove(E_t.attack);

            E_t.is_Attack = true;
        }
        if ((other.gameObject.tag == "chicken")&& E_t.is_basic_attack)
        {
            chicken = GameObject.FindWithTag("chicken").GetComponent<Chicken_Attack>();
            chicken.hpMove(E_t.attack);

            E_t.is_Attack = true;
        }
        //공격 받음 
        if (other.gameObject.tag == "chicken_wind")
        {
            if (E_t.is_go_right)//왼쪽에 적이 존재 
            {
                E_t.transform.position = new Vector3(E_t.transform.position.x + 1.5f, E_t.transform.position.y, E_t.transform.position.z);
            }
            else//적이 오른쪽 
            {
                E_t.transform.position = new Vector3(E_t.transform.position.x - 1.5f, E_t.transform.position.y, E_t.transform.position.z);
            }
            E_t.is_basic_attack = false;
        }
    }
    //겹침 방지 
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "cow")
        {
            //is_Attack: false
            if (other.gameObject.transform.position == E_t.transform.position)
            {
                Debug.Log("겹침");
                E_t.is_find_target = false;//겹치면 타겟 다시 찾기 
            }
        }
        if (other.gameObject.tag == "tiger")
        {
            if (other.gameObject.transform.position == E_t.transform.position)
            {
                E_t.is_find_target = false;
            }
        }
        if (other.gameObject.tag == "chicken")
        {
            if (other.gameObject.transform.position == E_t.transform.position)
            {
                E_t.is_find_target = false;
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
