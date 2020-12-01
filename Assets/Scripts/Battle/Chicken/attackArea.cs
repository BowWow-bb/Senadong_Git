﻿using System.Collections;
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
        if(other.gameObject.tag =="chicken_enemy")
        {
            chicken.is_Attack = true;
            camera_shake = true;
            Debug.Log("닿음");
        }
        if (other.gameObject.tag == "tiger_enemy")
        {
            chicken.is_Attack = true;
            camera_shake = true;
            Debug.Log("닿음");
        }
        if (other.gameObject.tag == "cow_enemy")
        {
            chicken.is_Attack = true;
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