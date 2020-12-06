using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_ch_Attack : MonoBehaviour
{
    public GameObject cow;
    public GameObject chicken;
    public GameObject tiger;

    float cow_distance;
    float chicken_distance;
    float tiger_distance;
    float min_distance = 10000;

    public float back_distance = 3;//뒤로 물러서는 거리
    public float back_distance_time = 40;//보다 크면 다시 공격
    float back_time;//뒤로 물러서는 시간 -> 증가되는 값 

    int battackTime = 0;//기본 공격 시간
    public int C_battackTime = 100;//장풍 쏘는 시간 조정

    bool is_target_cow = false;
    bool is_target_chicken = false;
    bool is_target_tiger = false;

    public bool is_find_target = false;
    public bool is_basic_attack = true;

    public bool is_go_right = false;//왼쪽에 적이 존재함 

    public bool is_Attack = false;
    public bool wind_Attacked = false;

    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        battackTime++;
    }
    //행동 
    public bool E_ch_Basic_Attack()
    {
        //기본: 거리 파악-> 가장 가까운 애한테 공격 (모두 체력 같을 때)->update로 할 경우 계속 바뀜 
        //체력이 다르다면 -> 체력 가장 낮은 애한테 공격... 추후에 
        if (is_basic_attack)
        {
            if (!is_Attack)//닿지 않았다면 계속 가기,닿으면 그만 
            {
                if (min_distance == cow_distance)
                {
                    is_target_cow = true;
                    transform.position = Vector3.MoveTowards(transform.position, cow.transform.position, 0.3f);
                }
                else if (min_distance == chicken_distance)
                {
                    is_target_chicken = true;
                    transform.position = Vector3.MoveTowards(transform.position, chicken.transform.position, 0.3f);
                }
                else if (min_distance == tiger_distance)
                {
                    is_target_tiger = true;
                    transform.position = Vector3.MoveTowards(transform.position, tiger.transform.position, 0.3f);
                }
            }
            else//닿았다면 뒤로 간다음 다시 가게 하면 => is_Attack = true
            {
                back_time++;
                if (is_target_cow)
                {
                    //왼쪽보는 중 
                    if (cow.transform.position.x < transform.position.x)//소가 왼쪽이라면 -> 오른쪽으로 가기 (-붙으면 오른쪽 봄)
                    {
                        Vector3 pos = new Vector3(cow.transform.position.x + back_distance, cow.transform.position.y, cow.transform.position.z);
                        transform.position = Vector3.MoveTowards(transform.position, pos, 0.1f);
                        transform.localScale = new Vector3(0.8f, 0.8f, 1);
                        is_go_right = true;
                    }
                    //오른쪽 보는 중 
                    else//왼쪽으로 가기 
                    {
                        Vector3 pos = new Vector3(cow.transform.position.x - back_distance, cow.transform.position.y, cow.transform.position.z);
                        transform.position = Vector3.MoveTowards(transform.position, pos, 0.1f);
                        transform.localScale = new Vector3(-0.8f, 0.8f, 1);
                        is_go_right = false;
                    }
                }
                else if (is_target_chicken)
                {
                    if (chicken.transform.position.x < transform.position.x)//치킨이 왼쪽이라면 -> 오른쪽으로 가기
                    {
                        Vector3 pos = new Vector3(chicken.transform.position.x + back_distance, chicken.transform.position.y, chicken.transform.position.z);
                        transform.position = Vector3.MoveTowards(transform.position, pos, 0.1f);
                        transform.localScale = new Vector3(0.8f, 0.8f, 1);
                        is_go_right = true;
                    }
                    else//왼쪽으로 가기 
                    {
                        Vector3 pos = new Vector3(chicken.transform.position.x - back_distance, chicken.transform.position.y, chicken.transform.position.z);
                        transform.position = Vector3.MoveTowards(transform.position, pos, 0.1f);
                        transform.localScale = new Vector3(-0.8f, 0.8f, 1);
                        is_go_right = false;
                    }
                }
                else if (is_target_tiger)
                {
                    if (tiger.transform.position.x < transform.position.x)//호랑이가 왼쪽이라면 -> 오른쪽으로 가기
                    {
                        Vector3 pos = new Vector3(tiger.transform.position.x + back_distance, tiger.transform.position.y, tiger.transform.position.z);
                        transform.position = Vector3.MoveTowards(transform.position, pos, 0.1f);
                        transform.localScale = new Vector3(0.8f, 0.8f, 1);
                        is_go_right = true;
                    }
                    else//왼쪽으로 가기 
                    {
                        Vector3 pos = new Vector3(tiger.transform.position.x - back_distance, tiger.transform.position.y, tiger.transform.position.z);
                        transform.position = Vector3.MoveTowards(transform.position, pos, 0.1f);
                        transform.localScale = new Vector3(-0.8f, 0.8f, 1);
                        is_go_right = false;
                    }
                }
                if (back_time > back_distance_time)
                {
                    //Debug.Log("뒤로 가는 중 끝남");
                    back_time = 0;
                    is_Attack = false;//근접 공격 false로 바꿔줘서 다시 다가갈 수 있게
                    is_basic_attack = false;//공격 한 번 끝 
                }
            }
        }
        else
        {
            if ((battackTime > C_battackTime))//기본 공격 시간 제어 
            {
                is_basic_attack = true;
                battackTime = 0;
            }
        }

        return true;
    }

    public bool E_ch_Find_Target()
    {
        if (!is_find_target)//target을 찾지 않았다면 
        {
            min_distance = 10000;
            //!is_find_target시점에 적과의 거리 파악 위함 -> 제일 가까운 적 찾기
            //체력 가장 낮은 애 공격 -> 나중에 할거임 
            if (cow != null)
            {
                cow_distance = Vector3.Distance(this.gameObject.transform.position, cow.transform.position);//거리 파악
                if ((min_distance > cow_distance) && cow_distance != 0)
                {
                    min_distance = cow_distance;
                }
            }
            if (chicken != null)
            {
                chicken_distance = Vector3.Distance(this.gameObject.transform.position, chicken.transform.position);//거리 파악
                if ((min_distance > chicken_distance) && chicken_distance != 0)
                {
                    min_distance = chicken_distance;
                }
            }
            if (tiger != null)
            {
                tiger_distance = Vector3.Distance(this.gameObject.transform.position, tiger.transform.position);//거리 파악
                if ((min_distance > tiger_distance) && tiger_distance != 0)
                {
                    min_distance = tiger_distance;
                }
            }
            is_find_target = true;//target 찾음 
        }
        return true;
    }
}
