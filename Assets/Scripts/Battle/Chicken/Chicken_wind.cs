using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chicken_wind : MonoBehaviour
{
    Chicken_Attack chicken;
    E_ch_Attack E_chicken;
    E_cow_Attack E_cow;
    E_t_Attack E_tiger;

    camera_shake Camera;

    //int power = 200;//후에 공격력으로 수정해주삼요~

    bool camera_shake = false;
    bool attacked = false;
    Vector3 pos;

    public float speed =3;
    // Start is called before the first frame update
    void Start()
    {
        chicken = GameObject.FindWithTag("chicken").GetComponent<Chicken_Attack>();
        Camera = GameObject.FindWithTag("MainCamera").GetComponent<camera_shake>();

        if (chicken.is_go_right)//적이 왼쪽이라면 
        {
            pos = Vector3.left;
        }
        else
        {
            pos = Vector3.right;
        }
        Destroy(gameObject, 5);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += pos * Time.deltaTime * speed;
        if (camera_shake)
        {
            Camera.cameraOn = true;
            Camera.shake = 0.15f;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        Vector3 pos;
        //적이랑 닿으면 camera 움직임 
        if (other.gameObject.tag == "chicken_enemy")
        {
            E_chicken = GameObject.FindWithTag("chicken_enemy").GetComponent<E_ch_Attack>();
            chicken.is_Attack = true;
            camera_shake = true;
            if(!attacked)//중복 방지 
            {
                if (chicken.is_go_right)//적이 왼쪽 이면 
                {
                    pos = new Vector3(other.transform.position.x - 1, other.transform.position.y, other.transform.position.z);
                }
                else//적이 오른쪽이면 
                {
                    pos = new Vector3(other.transform.position.x + 1, other.transform.position.y, other.transform.position.z);
                }
                transform.position = Vector3.MoveTowards(other.transform.position, pos, 0.1f);
                E_chicken.hpMove(chicken.attack);
                if (E_chicken.hp <= 0)
                {
                    camera_shake = false;
                }
                attacked = true;
            }
        }
        if (other.gameObject.tag == "tiger_enemy")
        {
            E_tiger = GameObject.FindWithTag("tiger_enemy").GetComponent<E_t_Attack>();
            chicken.is_Attack = true;
            camera_shake = true;
            if (!attacked)//중복 방지 
            {
                if (chicken.is_go_right)//적이 왼쪽 이면 
                {
                    pos = new Vector3(other.transform.position.x - 1, other.transform.position.y, other.transform.position.z);
                }
                else//적이 오른쪽이면 
                {
                    pos = new Vector3(other.transform.position.x + 1, other.transform.position.y, other.transform.position.z);
                }
                transform.position = Vector3.MoveTowards(other.transform.position, pos, 0.1f);
                E_tiger.hpMove(chicken.attack);
                if (E_tiger.hp <= 0)
                {
                    camera_shake = false;
                }
                attacked = true;
            }

        }
        if (other.gameObject.tag == "cow_enemy")
        {
            E_cow = GameObject.FindWithTag("cow_enemy").GetComponent<E_cow_Attack>();
            chicken.is_Attack = true;
            camera_shake = true;
            if (!attacked)//중복 방지 
            {
                if (chicken.is_go_right)//적이 왼쪽 이면 
                {
                    pos = new Vector3(other.transform.position.x - 1, other.transform.position.y, other.transform.position.z);
                }
                else//적이 오른쪽이면 
                {
                    pos = new Vector3(other.transform.position.x + 1, other.transform.position.y, other.transform.position.z);
                }
                transform.position = Vector3.MoveTowards(other.transform.position, pos, 0.1f);
                E_cow.hpMove(chicken.attack);
                if(E_cow.hp<=0)
                {
                    camera_shake = false;
                }
                attacked = true;
            }

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
}
