﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiger_Attack : MonoBehaviour
{
    ItemManager item_manager;
    public int level;  //캐릭터 레벨

    Attack_Data attack_data;
    public int attack; //공격력

    public int hp;                 //hp
    int HPMax;              //최대 체력
    GameObject hp_bar;      //hp바
    float hpbar_sx;         //hp바 스케일 x값
    float hpbar_tx;         //hp바 위치 x값
    float hpbar_tmp;        //hp바 감소 정도

    //hp 파악 위함 
    E_ch_Attack E_chicken_hp;
    E_cow_Attack E_cow_hp;
    E_t_Attack E_tiger_hp;
    //

    public GameObject cow_enemy;
    public GameObject chicken_enemy;
    public GameObject tiger_enemy;

    float cow_distance;
    float chicken_distance;
    float tiger_distance;
    float min_distance = 10000;

    public float back_distance = 4;//뒤로 물러서는 거리
    public float back_distance_time = 40;//보다 크면 다시 공격 
    float back_time;//뒤로 물러서는 시간 -> 증가되는 값

    int battackTime = 0;//기본 공격 시간
    int skillTime = 0;
    public int C_battackTime = 100;//기본 공격 시간 조정
    public int C_skillTime=1000;
    public bool is_skill_time = false;
    public bool is_target_cow = false;
    public bool is_target_chicken = false;
    public bool is_target_tiger = false;

    public bool is_find_target = false;
    public bool is_basic_attack = false;

    public bool is_go_right = false;//왼쪽에 적이 존재함 
    public bool is_Attack = false;
    public GameObject fPazik;
    public Cow_Attack cow;
    public Chicken_Attack chicken;
    int tmp_tiger;
    int tmp_cow;
    int tmp_chicken;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
       // isDie = GameObject.Find("Tiger_p").transform.GetChild(0).GetComponent<Tiger_Move>().isDie;

        HPMax = 1000;
        hp = HPMax;
        hp_bar = GameObject.FindWithTag("TigerHp");
        hpbar_sx = hp_bar.transform.localScale.x;
        hpbar_tx = hp_bar.transform.localPosition.x;
        hpbar_tmp = hpbar_sx / HPMax;   //최대 체력에 따른 hp바 이동량 설정

        //hp 가져오기 
        E_chicken_hp = GameObject.FindWithTag("chicken_enemy").GetComponent<E_ch_Attack>();
        E_cow_hp = GameObject.FindWithTag("cow_enemy").GetComponent<E_cow_Attack>();
        E_tiger_hp = GameObject.FindWithTag("tiger_enemy").GetComponent<E_t_Attack>();
        //

        fPazik = transform.GetChild(1).gameObject;
        fPazik.SetActive(false);
        cow = GameObject.FindWithTag("cow").GetComponent<Cow_Attack>();
        chicken = GameObject.FindWithTag("chicken").GetComponent<Chicken_Attack>();

        //공격 레벨 가져오기
        item_manager = GameObject.Find("ItemManager").GetComponent<ItemManager>();
        level = item_manager.tiger_level;

        //공격력 가져오기
        attack_data = GameObject.Find("AttackData").GetComponent<Attack_Data>();
        attack = attack_data.getAttackValue(level);

        Debug.Log("호랑이 레벨: " + level + ", 공격력: " + attack);

        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (item_manager.tiger_die)
        {
            E_tiger_hp.is_find_target = false;
            Destroy(gameObject, 0.001f);
        }


        battackTime++;
        if (is_target_chicken)
        {
            if (E_chicken_hp.hp <= 0)
            {
                Debug.Log("적 치킨 죽음 ");
                is_Attack = true;
                is_target_chicken = false;
                is_find_target = false;
            }
        }
        if (is_target_cow)
        {
            if (E_cow_hp.hp <= 0)
            {
                Debug.Log("적 소 죽음 ");
                is_Attack = true;
                is_target_cow = false;
                is_find_target = false;
            }
        }
        if (is_target_tiger)
        {
            if (E_tiger_hp.hp <= 0)
            {
                is_Attack = true;
                Debug.Log("적 호랑이 죽음");
                is_target_tiger = false;
                is_find_target = false;
            }
        }
        if (gameObject.transform.position.x <= -13.8f)
        {
            gameObject.transform.position = new Vector3(-13.8f, transform.position.y, transform.position.z);
        }
        else if (gameObject.transform.position.x >= 13.8f)
        {
            gameObject.transform.position = new Vector3(13.8f, transform.position.y, transform.position.z);
        }
        //if (is_basic_attack)
        //{
        //    animator.SetBool("is_Attack", true);
        //}
        //else
        //{
        //    animator.SetBool("is_Attack", false);
        //}
    }
    //행동 
    public bool Tiger_Basic_Attack()
    {
        //기본: 거리 파악-> 가장 가까운 애한테 공격 (모두 체력 같을 때)->update로 할 경우 계속 바뀜 
        //체력이 다르다면 -> 체력 가장 낮은 애한테 공격... 추후에
        if(is_basic_attack)
        {
            if (!is_Attack)//닿지 않았다면 계속 가기,닿으면 그만 
            {
                if (min_distance == cow_distance && cow_enemy != null)
                {
                    is_target_cow = true;
                    transform.position = Vector3.MoveTowards(transform.position, cow_enemy.transform.position, 0.3f);
                }
                else if (min_distance == chicken_distance && chicken_enemy != null)
                {
                    is_target_chicken = true;
                    transform.position = Vector3.MoveTowards(transform.position, chicken_enemy.transform.position, 0.3f);
                }
                else if (min_distance == tiger_distance && tiger_enemy != null)
                {
                    is_target_tiger = true;
                    transform.position = Vector3.MoveTowards(transform.position, tiger_enemy.transform.position, 0.3f);
                }
            }
            else//닿았다면 뒤로 간다음 다시 가게 하면 
            {
                back_time++;
                if (is_target_cow && cow_enemy != null)
                {
                    if (cow_enemy.transform.position.x < transform.position.x)//소가 왼쪽이라면 -> 오른쪽으로 가기
                    {
                        Vector3 pos = new Vector3(cow_enemy.transform.position.x + back_distance, cow_enemy.transform.position.y, cow_enemy.transform.position.z);
                        transform.position = Vector3.MoveTowards(transform.position, pos, 0.1f);
                        transform.localScale = new Vector3(1, 1, 1);
                    }
                    else//왼쪽으로 가기 
                    {
                        Vector3 pos = new Vector3(cow_enemy.transform.position.x - back_distance, cow_enemy.transform.position.y, cow_enemy.transform.position.z);
                        transform.position = Vector3.MoveTowards(transform.position, pos, 0.1f);
                        transform.localScale = new Vector3(-1, 1, 1);
                    }
                }
                else if (is_target_chicken && chicken_enemy != null)
                {
                    if (chicken_enemy.transform.position.x < transform.position.x)//치킨이 왼쪽이라면 -> 오른쪽으로 가기
                    {
                        Vector3 pos = new Vector3(chicken_enemy.transform.position.x + back_distance, chicken_enemy.transform.position.y, chicken_enemy.transform.position.z);
                        transform.position = Vector3.MoveTowards(transform.position, pos, 0.1f);
                        transform.localScale = new Vector3(1, 1, 1);
                    }
                    else//왼쪽으로 가기 
                    {
                        Vector3 pos = new Vector3(chicken_enemy.transform.position.x - back_distance, chicken_enemy.transform.position.y, chicken_enemy.transform.position.z);
                        transform.position = Vector3.MoveTowards(transform.position, pos, 0.1f);
                        transform.localScale = new Vector3(-1, 1, 1);
                    }
                }
                else if (is_target_tiger && tiger_enemy != null)
                {
                    if (tiger_enemy.transform.position.x < transform.position.x)//호랑이가 왼쪽이라면 -> 오른쪽으로 가기
                    {
                        Vector3 pos = new Vector3(tiger_enemy.transform.position.x + back_distance, tiger_enemy.transform.position.y, tiger_enemy.transform.position.z);
                        transform.position = Vector3.MoveTowards(transform.position, pos, 0.1f);
                        transform.localScale = new Vector3(1, 1, 1);
                    }
                    else//왼쪽으로 가기 
                    {
                        Vector3 pos = new Vector3(tiger_enemy.transform.position.x - back_distance, tiger_enemy.transform.position.y, tiger_enemy.transform.position.z);
                        transform.position = Vector3.MoveTowards(transform.position, pos, 0.1f);
                        transform.localScale = new Vector3(-1, 1, 1);
                    }
                }
                if (back_time > back_distance_time)
                {
                    back_time = 0;
                    is_Attack = false;//근접 공격 false로 바꿔줘서 다시 다가갈 수 있게
                    is_basic_attack = false;//공격 한번 끝 
                }
            }
        }
        else
        {
            if (battackTime > C_battackTime)//기본 공격 시간 제어 
            {
                is_basic_attack = true;
                battackTime = 0;
            }
        }
        return true;
    }

    public bool Tiger_Find_Target()
    {
        if (!is_find_target)//target을 찾지 않았다면 
        {
            min_distance = 10000;
            //!is_find_target시점에 적과의 거리 파악 위함 -> 제일 가까운 적 찾기
            //체력 가장 낮은 애 공격 -> 나중에 할거임 
            if (cow_enemy != null)
            {
                cow_distance = Vector3.Distance(this.gameObject.transform.position, cow_enemy.transform.position);//거리 파악
                if ((min_distance > cow_distance) && cow_distance != 0)
                {
                    min_distance = cow_distance;
                }
            }
            if (chicken_enemy != null)
            {
                chicken_distance = Vector3.Distance(this.gameObject.transform.position, chicken_enemy.transform.position);//거리 파악
                if ((min_distance > chicken_distance) && chicken_distance != 0)
                {
                    min_distance = chicken_distance;
                }
            }
            if (tiger_enemy != null)
            {
                tiger_distance = Vector3.Distance(this.gameObject.transform.position, tiger_enemy.transform.position);//거리 파악
                if ((min_distance > tiger_distance) && tiger_distance != 0)
                {
                    min_distance = tiger_distance;
                }
            }
            is_find_target = true;//target 찾음 
        }
        return true;
    }
    public bool Tiger_Skill()
    {
        if (is_skill_time)
        {
            skillTime++;
            if (skillTime > 5 * battackTime)
            {
                cow.attack = cow.attack  -3 * cow.level;
                chicken.attack = chicken.attack - 3 * chicken.level;
                attack = attack - 3 * level;

                skillTime = 0;
                fPazik.SetActive(false);
                is_skill_time = false;
            }
        }
        else
        {
            skillTime++;
            if (skillTime > C_skillTime && !is_basic_attack)
            {
                fPazik.SetActive(true);
                skillTime = 0;
                is_skill_time = true;
                tmp_cow = cow.attack;
                //cow.attack = tmp_cow * 1.2f;
                cow.attack = cow.attack + 3*cow.level;
                //chicken.attack = tmp_chicken * 1.2f;
                chicken.attack = chicken.attack + 3*chicken.level;
                //attack = tmp_tiger * 1.2f;
                attack = attack + 3*level;

            }
        }
        return true;
    }
    public void hpMove(int hp_delta)    //hp바 동작 구현
    {
        if (hp - hp_delta <= 0)   //0이하로 내려가는 경우 죽은걸로 판단
            Destroy(gameObject);

        float move = ((HPMax - hp) + hp_delta) * hpbar_tmp; //hp바 이동할 크기
        hp -= hp_delta; //hp 재설정

        Vector3 Scale = hp_bar.transform.localScale;    //현재 스케일 값
        hp_bar.transform.localScale = new Vector3(hpbar_sx - move, Scale.y, Scale.z);

        Vector3 Pos = hp_bar.transform.localPosition;   //현재 포지션 값
        hp_bar.transform.localPosition = new Vector3(hpbar_tx - move / 2.0f, Pos.y, Pos.z);   //스케일 변화의 절반 만큼 이동시, 자연스러운 감소효과 나타낼 수 o
    }

}
