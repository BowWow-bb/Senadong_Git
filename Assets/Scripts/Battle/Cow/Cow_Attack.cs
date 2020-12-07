using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cow_Attack : MonoBehaviour
{
    ItemManager item_manager;
    int level;  //캐릭터 레벨

    Attack_Data attack_data;
    int attack; //공격력

    int hp;                 //hp
    int HPMax;              //최대 체력
    GameObject hp_bar;      //hp바
    float hpbar_sx;         //hp바 스케일 x값
    float hpbar_tx;         //hp바 위치 x값
    float hpbar_tmp;        //hp바 감소 정도

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
    public int C_battackTime = 100;//기본 공격 시간 조정

    bool is_target_cow = false;
    bool is_target_chicken = false;
    bool is_target_tiger = false;

    public bool is_find_target = false;
    bool is_basic_attack = false;

    public bool is_go_right = false;//왼쪽에 적이 존재함 
    public bool is_Attack = false;


    //Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        HPMax = 1000;
        hp = HPMax;
        hp_bar = GameObject.FindWithTag("CowHp");
        hpbar_sx = hp_bar.transform.localScale.x;
        hpbar_tx = hp_bar.transform.localPosition.x;
        hpbar_tmp = hpbar_sx / HPMax;   //최대 체력에 따른 hp바 이동량 설정


        //공격 레벨 가져오기
        item_manager = GameObject.Find("ItemManager").GetComponent<ItemManager>();
        level = item_manager.cow_level;

        //공격력 가져오기
        attack_data = GameObject.Find("AttackData").GetComponent<Attack_Data>();
        attack = attack_data.getAttackValue(level);

        //animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        battackTime++;
        if (is_basic_attack)
        {
            //animator.SetBool("is_Attack", true);
        }
        else
        {
            //animator.SetBool("is_Attack", false);
        }
    }
    //행동 
    public bool Cow_Basic_Attack()
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
                if (is_target_cow)
                {
                    if (cow_enemy.transform.position.x < transform.position.x)//소가 왼쪽이라면 -> 오른쪽으로 가기
                    {
                        Vector3 pos = new Vector3(cow_enemy.transform.position.x + back_distance, cow_enemy.transform.position.y, cow_enemy.transform.position.z);
                        transform.position = Vector3.MoveTowards(transform.position, pos, 0.1f);
                        transform.localScale = new Vector3(-1, 1, 1);
                    }
                    else//왼쪽으로 가기 
                    {
                        Vector3 pos = new Vector3(cow_enemy.transform.position.x - back_distance, cow_enemy.transform.position.y, cow_enemy.transform.position.z);
                        transform.position = Vector3.MoveTowards(transform.position, pos, 0.1f);
                        transform.localScale = new Vector3(1, 1, 1);
                    }
                }
                else if (is_target_chicken)
                {
                    if (chicken_enemy.transform.position.x < transform.position.x)//치킨이 왼쪽이라면 -> 오른쪽으로 가기
                    {
                        Vector3 pos = new Vector3(chicken_enemy.transform.position.x + back_distance, chicken_enemy.transform.position.y, chicken_enemy.transform.position.z);
                        transform.position = Vector3.MoveTowards(transform.position, pos, 0.1f);
                        transform.localScale = new Vector3(-1, 1, 1);
                    }
                    else//왼쪽으로 가기 
                    {
                        Vector3 pos = new Vector3(chicken_enemy.transform.position.x - back_distance, chicken_enemy.transform.position.y, chicken_enemy.transform.position.z);
                        transform.position = Vector3.MoveTowards(transform.position, pos, 0.1f);
                        transform.localScale = new Vector3(1, 1, 1);
                    }
                }
                else if (is_target_tiger)
                {
                    if (tiger_enemy.transform.position.x < transform.position.x)//호랑이가 왼쪽이라면 -> 오른쪽으로 가기
                    {
                        Vector3 pos = new Vector3(tiger_enemy.transform.position.x + back_distance, tiger_enemy.transform.position.y, tiger_enemy.transform.position.z);
                        transform.position = Vector3.MoveTowards(transform.position, pos, 0.1f);
                        transform.localScale = new Vector3(-1, 1, 1);
                    }
                    else//왼쪽으로 가기 
                    {
                        Vector3 pos = new Vector3(tiger_enemy.transform.position.x - back_distance, tiger_enemy.transform.position.y, tiger_enemy.transform.position.z);
                        transform.position = Vector3.MoveTowards(transform.position, pos, 0.1f);
                        transform.localScale = new Vector3(1, 1, 1);
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

    public bool Cow_Find_Target()
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
