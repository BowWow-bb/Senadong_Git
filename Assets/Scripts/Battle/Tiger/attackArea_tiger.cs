using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attackArea_tiger : MonoBehaviour
{
    Tiger_Attack tiger;
    camera_shake Camera;

    bool camera_shake = false;

    // Start is called before the first frame update
    void Start()
    {
        tiger = GameObject.FindWithTag("tiger").GetComponent<Tiger_Attack>();
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
        if (other.gameObject.tag == "chicken_enemy")
        {
            tiger.is_Attack = true;
            camera_shake = true;
            Debug.Log("닿음");
        }
        if (other.gameObject.tag == "tiger_enemy")
        {
            tiger.is_Attack = true;
            camera_shake = true;
            Debug.Log("닿음");
        }
        if (other.gameObject.tag == "cow_enemy")
        {
            tiger.is_Attack = true;
            camera_shake = true;
            Debug.Log("닿음");
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
