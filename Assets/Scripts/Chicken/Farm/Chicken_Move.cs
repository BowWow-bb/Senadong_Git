﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class Chicken_Move : MonoBehaviour
{
    Animator animator;

    GameObject Bap_prefab;

    bool is_drag;
    int MovedTime = 0;

    int EggTime = 0;//달걀 낳는 시간
    public int C_EggTime = 1000;//달걀 낳는 속도 조정 

    public float movePower = 1f;//움직이는 속도

    Vector3 Dir;
    float DirR = 180.0f;
    float Fspeed = 0.04f;

    int movementFlag = 0;//0:idle, 1:left, 2:right

    public bool is_follow_food = false;
    public bool isdrag;
    bool isEggTime;//달걀 낳는 거 
    bool ismoving = true;
    bool isRight = false;//보는 방향:왼쪽/오른쪽 

    public GameObject Egg_Prefab;//달걀 

    void Start()
    {
        animator = gameObject.GetComponentInChildren<Animator>();
        is_drag = GameObject.Find("Click_Move").GetComponent<Click_Move>().chicken_drag;
        movementFlag = Random.Range(0, 5);//0,1,2,3,4
    }

    void Update()
    {
        EggTime++;
        if(isdrag)
        {
            //animator.SetBool("is_drag", true);
        }
        else
        {
            //animator.SetBool("is_drag", false);
        }
    }

    //행동 
    public bool Chicken_FollowMouse()
    {
        return true;
    }

    public bool Chicken_Eat()
    {
        return true;
    }

    public bool Chicken_Follow_Food()
    {
        if(is_follow_food)
        {
            Dir = Bap_prefab.transform.position - gameObject.transform.position;
            Dir.Normalize();

            Quaternion Rot = Quaternion.LookRotation(Dir, new Vector3(0, 1, 0));
            DirR = Rot.eulerAngles.y;
            gameObject.transform.localRotation = Rot;
            gameObject.transform.position += Dir * Fspeed;

            return true;
        }
        else
        {
            return true;
        }
    }
    public bool Chicken_BasicMove()
    {
        if (ismoving && !is_follow_food)//움직이는 중인 상태-> 상태 안 바꾸게 
        {
            Vector3 moveVelocity = Vector3.zero;
            MovedTime++;
            if (MovedTime>200)//움직인 시간 일정 시간 넘으면 
            {
                ismoving = false;//상태 바꾸기 
            }

            //animator.SetBool("is_drop_egg", false);

            if (movementFlag == 1)//왼쪽 
            {
                moveVelocity = Vector3.left;//(-1,0,0)
                transform.localScale = new Vector3(0.8f, 0.8f, 1);
                isRight = false;
            }
            else if (movementFlag == 2)//오른쪽 
            {
                moveVelocity = Vector3.right;//(1,0,0)
                transform.localScale = new Vector3(-0.8f, 0.8f, 1);
                isRight = true;
            }
            else if(movementFlag ==3)//왼쪽 보는 중
            {
                moveVelocity = new Vector3(-1, 1, 0);
                transform.localScale = new Vector3(0.8f, 0.8f, 1);
                isRight = false;
            }
            else if (movementFlag ==4)//오른쪽 보는 중 
            {
                moveVelocity = new Vector3(1, -1, 0);
                transform.localScale = new Vector3(-0.8f, 0.8f, 1);
                isRight = true;
            }
            else//movementFlag=0 일 때 
            {
                //animator.SetBool("is_drop_egg", true);
                Chicken_Egg();
            }

            gameObject.transform.position += moveVelocity * movePower * Time.deltaTime;

            return true;
        }
        else//움직이는 방향 바꿀 시간 되면 
        {
            MovedTime = 0;
            movementFlag = Random.Range(0, 5);//0,1,2,3,4
            ismoving = true;

            return true;
        }
    }

    public void Chicken_Egg()
    {
        if (isEggTime)
        {
            Vector3 eggPos;
            //animator.SetBool("is_drop_egg", true);
            
            if (isRight)//오른쪽을 보고 있는 경우 
            {
                eggPos = new Vector3(transform.position.x - 1, transform.position.y - 0.8f, transform.position.z);

            }
            else//왼쪽을 보고 있는 경우 
            {
                eggPos = new Vector3(transform.position.x + 1, transform.position.y - 0.8f, transform.position.z);
            }
            
            GameObject egg = GameObject.Instantiate(Egg_Prefab);
            isEggTime = false;
            egg.transform.position = eggPos;
            egg.transform.parent = null;
        }
        else
        {
            if (EggTime > C_EggTime)
            {
                isEggTime = true;
                EggTime = 0;
            }
        }
    }
}
