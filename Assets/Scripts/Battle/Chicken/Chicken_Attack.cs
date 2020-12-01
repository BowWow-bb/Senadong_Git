﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chicken_Attack : MonoBehaviour
{
    public GameObject cow_enemy;
    public GameObject chicken_enemy;
    public GameObject tiger_enemy;

    float cow_distance;
    float chicken_distance;
    float tiger_distance;
    float min_distance = 10000;

    public float back_distance=4;//뒤로 물러서는 거리
    public float back_distance_time = 40;//보다 크면 다시 공격 
    float back_time;//뒤로 물러서는 시간 -> 증가되는 값 

    bool is_target_cow = false;
    bool is_target_chicken = false;
    bool is_target_tiger = false;

    bool is_find_target = false;
    bool is_basic_attack = false;

    public bool is_Attack = false;

    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(is_basic_attack)
        {
            animator.SetBool("is_Attack", true);
        }
        else
        {
            animator.SetBool("is_Attack", false);
        }
    }
    //행동 
    public bool Chicken_Basic_Attack()
    {
        //기본: 거리 파악-> 가장 가까운 애한테 공격 (모두 체력 같을 때)->update로 할 경우 계속 바뀜 
        //체력이 다르다면 -> 체력 가장 낮은 애한테 공격... 추후에 

        is_basic_attack = true;//기본 공격 중임 

        if (!is_Attack)//닿지 않았다면 계속 가기,닿으면 그만 
        {
            if (min_distance == cow_distance)
            {
                is_target_cow = true;
                transform.position = Vector3.MoveTowards(transform.position, cow_enemy.transform.position, 0.3f);
            }
            else if (min_distance == chicken_distance)
            {
                is_target_chicken = true;
                transform.position = Vector3.MoveTowards(transform.position, chicken_enemy.transform.position, 0.3f);
            }
            else if (min_distance == tiger_distance)
            {
                is_target_tiger = true;
                transform.position = Vector3.MoveTowards(transform.position, tiger_enemy.transform.position, 0.3f);
            }
        }
        else//닿았다면 뒤로 간다음 다시 가게 하면 
        {
            back_time++;
            if(is_target_cow)
            {
                if(cow_enemy.transform.position.x <transform.position.x)//소가 왼쪽이라면 -> 오른쪽으로 가기 (-붙으면 오른쪽 봄)
                {
                    Vector3 pos = new Vector3(cow_enemy.transform.position.x + back_distance, cow_enemy.transform.position.y, cow_enemy.transform.position.z);
                    transform.position = Vector3.MoveTowards(transform.position, pos , 0.1f);
                    transform.localScale = new Vector3(0.8f, 0.8f, 1);
                }
                else//왼쪽으로 가기 
                {
                    Vector3 pos = new Vector3(cow_enemy.transform.position.x - back_distance, cow_enemy.transform.position.y, cow_enemy.transform.position.z);
                    transform.position = Vector3.MoveTowards(transform.position, pos, 0.1f);
                    transform.localScale = new Vector3(-0.8f, 0.8f, 1);
                }
            }
            else if(is_target_chicken)
            {
                if (chicken_enemy.transform.position.x < transform.position.x)//치킨이 왼쪽이라면 -> 오른쪽으로 가기
                {
                    Vector3 pos = new Vector3(chicken_enemy.transform.position.x + back_distance, chicken_enemy.transform.position.y, chicken_enemy.transform.position.z);
                    transform.position = Vector3.MoveTowards(transform.position, pos, 0.1f);
                    transform.localScale = new Vector3(0.8f, 0.8f, 1);
                }
                else//왼쪽으로 가기 
                {
                    Vector3 pos = new Vector3(chicken_enemy.transform.position.x - back_distance, chicken_enemy.transform.position.y, chicken_enemy.transform.position.z);
                    transform.position = Vector3.MoveTowards(transform.position, pos, 0.1f);
                    transform.localScale = new Vector3(-0.8f, 0.8f, 1);
                }
            }
            else if(is_target_tiger)
            {
                if (tiger_enemy.transform.position.x < transform.position.x)//호랑이가 왼쪽이라면 -> 오른쪽으로 가기
                {
                    Vector3 pos = new Vector3(tiger_enemy.transform.position.x + back_distance, tiger_enemy.transform.position.y, tiger_enemy.transform.position.z);
                    transform.position = Vector3.MoveTowards(transform.position, pos, 0.1f);
                    transform.localScale = new Vector3(0.8f, 0.8f, 1);
                }
                else//왼쪽으로 가기 
                {
                    Vector3 pos = new Vector3(tiger_enemy.transform.position.x - back_distance, tiger_enemy.transform.position.y, tiger_enemy.transform.position.z);
                    transform.position = Vector3.MoveTowards(transform.position, pos, 0.1f);
                    transform.localScale = new Vector3(-0.8f, 0.8f, 1);
                }
            }
            if(back_time>back_distance_time)
            {
                back_time = 0;
                is_Attack = false;//근접 공격 false로 바꿔줘서 다시 다가갈 수 있게 
            }
        }
        return true;
    }

    public bool Chicken_Find_Target()
    {
        if(!is_find_target)//target을 찾지 않았다면 
        {
            //!is_find_target시점에 적과의 거리 파악 위함 -> 제일 가까운 적 찾기
            //체력 가장 낮은 애 공격 -> 나중에 할거임 
            if (cow_enemy != null)
            {
                cow_distance = Vector3.Distance(this.gameObject.transform.position, cow_enemy.transform.position);//거리 파악
                if (min_distance > cow_distance)
                {
                    min_distance = cow_distance; 
                }
            }
            if (chicken_enemy != null)
            {
                chicken_distance = Vector3.Distance(this.gameObject.transform.position, chicken_enemy.transform.position);//거리 파악
                if (min_distance > chicken_distance)
                {
                    min_distance = chicken_distance;
                }
            }
            if (tiger_enemy != null)
            {
                tiger_distance = Vector3.Distance(this.gameObject.transform.position, tiger_enemy.transform.position);//거리 파악
                if (min_distance > tiger_distance)
                {
                    min_distance = tiger_distance;
                }
            }
            is_find_target = true;//target 찾음 
        } 
        return true;
    }
    
}