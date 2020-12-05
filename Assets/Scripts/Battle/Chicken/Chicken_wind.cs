using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chicken_wind : MonoBehaviour
{
    Chicken_Attack chicken;
    camera_shake Camera;

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
        //적이랑 닿으면 camera 움직임 
        if (other.gameObject.tag == "chicken_enemy")
        {
            chicken.is_Attack = true;
            camera_shake = true;
            if(!attacked)
            {
                if (chicken.is_go_right)//적이 왼쪽 이면 
                {
                    other.transform.position = new Vector3(other.transform.position.x - 1, other.transform.position.y, other.transform.position.z);
                }
                else//적이 오른쪽이면 
                {
                    other.transform.position = new Vector3(other.transform.position.x + 1, other.transform.position.y, other.transform.position.z);
                }
                attacked = true;
            }
        }
        if (other.gameObject.tag == "tiger_enemy")
        {
            chicken.is_Attack = true;
            camera_shake = true;
            //Debug.Log("닿음");
        }
        if (other.gameObject.tag == "cow_enemy")
        {
            chicken.is_Attack = true;
            camera_shake = true;
            //Debug.Log("닿음");
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
