using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class Chicken_Move : MonoBehaviour
{
    public GameObject ttiger;
    ItemManager item_manager;
    int exp_check;

    //서창 이동
    //int hungryTime = 0; // 배고픔 재는 시간
    int BasicTime = 0; // 기본 움직임 재는 시간
    public int playTime = 0; // 심심한 시간 재는 시간

    public bool playing = false; // 노는중 인지
    bool hunger; // 배고픈 상태인지 
    public bool moving; // 움직이고 있는 상태 인지
    float move_length = 0; // 얼마나 움직였는지의 벡터
    float trace_length = 0;
    int check = 0;
    public bool trace_mouse; // 마우스를 향해 달리는 중인지
    public bool quarrel = false;
    public bool hurt = false;
    Vector2 move_vec; // 움직일 방향벡터
    Vector3 Start_Point; // 움직일때의 시작점
    Vector3 trace; // 마우스와 오브젝트 사이의 벡터 
    Vector3 Mouse;
    Vector3 tmp_Point;
    Vector3 hurt_vec;
    int hurt_length=0;
    //

    //속성값 관련
    public int Timer;
    int statTime = 450, statMax = 450;  //말풍선 지속 시간
    public int valueMax = 1000;
    public int hungry; public bool isHungry = false; int hungryTimer = 900;
    public int poop; public bool isPoop = false; int poopTimer = 1400;
    public int play; bool isPlay = false; int playTimer = 1150;

    //청결 관련
    public int countPoop = 0;           //똥 개수
    public GameObject ChickenPoopPrefab;    //똥 오브젝트
    Vector3 toiletPos;                  //화장실 내 목표 위치
    float tx, ty;                       //화장실 내 목표 위치 x,y (랜덤)

    //속성관련 오브젝트 -자식
    public GameObject fHungry;                 //체력 오브젝트
    public GameObject fPoop;                   //청결 오브젝트
    public GameObject fPlay;                   //흥미 오브젝트 
    public int exp = 0;

    //밥 추적 위함 
    GameObject Bap;
    GameObject Milk;
    GameObject Egg;
    public float follow_distance = 10;//밥 추적 범위 
    float distance;
    float distance_milk;
    float distance_egg;
    public bool is_follow_food = false;//밥 추적 중인지
    public bool is_follow_milk = false;//우유 추적 중인지
    public bool is_follow_egg = false;//계란 추적 중인지
    //

    Animator animator;

    public bool isdrag = false;//drag 중인지 파악 -> 이동시 순간이동 방지  

    int EggTime = 0;//달걀 낳는 시간
    public int C_EggTime = 2000;//달걀 낳는 속도 조정 

    public bool isEggTime;//달걀 낳는 거 
    bool isRight = false;//보는 방향:왼쪽/오른쪽 

    public GameObject Egg_Prefab;//달걀

    void Start()
    {
        ttiger = GameObject.FindWithTag("tiger").gameObject;
        item_manager = GameObject.Find("ItemManager").GetComponent<ItemManager>();
        exp_check = 100;

        //말풍선 비활성화
        fHungry = transform.GetChild(0).gameObject;
        fHungry.SetActive(false);
        fPoop = transform.GetChild(1).gameObject;
        fPoop.SetActive(false);
        fPlay = transform.GetChild(2).gameObject;
        fPlay.SetActive(false);

        hunger = false; // 변수 초기화
        moving = false;
        playing = false;

        animator = GetComponent<Animator>();

        if (!item_manager.chicken_die && item_manager.cc_i != 0)
        {
            exp = item_manager.cc_exp;
            hungry = item_manager.cc_hungry;
            poop = item_manager.cc_poop;
            play = item_manager.cc_play;
        }
        else
        {
            hungry = valueMax;
            poop = valueMax;
            play = valueMax;
        }
    }

    void Update()
    {
        EggTime++;
        if (isdrag || hurt)
        {
            animator.SetBool("is_drag", true);
        }
        else
        {
            animator.SetBool("is_drag", false);
        }

        if (!moving && !isPoop && !is_follow_food &&!is_follow_egg && !is_follow_milk &&!quarrel)//따라가는 상태가 아니라면 
        {
            animator.SetBool("is_drop_egg", true);
        }
        else
        {
            animator.SetBool("is_drop_egg", false);
        }

        //추적 우선 순위: 밥> 우유 > 계란
        //밥 추적 
        Bap = GameObject.FindWithTag("hungry_follow_item");//밥 아이템 찾기 -> 문제: 여러 개 생성되었으면 제일 위에것만 따라감 
        if (Bap != null)//밥 생성 되었는지 
        {
            //Debug.Log("밥 생성");
            distance = Vector3.Distance(this.gameObject.transform.position, Bap.transform.position);//거리 파악
        }
        is_follow_food = (Bap != null && distance < follow_distance && isHungry == true);//밥이 생성 되었고 거리가 follow_distance 미만이라면 is_follow_food true

        //우유 아이템 추적 
        Milk = GameObject.FindWithTag("milk_item_follow");//밥 아이템 찾기 -> 문제: 여러 개 생성되었으면 제일 위에것만 따라감 
        if (Milk != null)//우선 순위
        {
            distance_milk = Vector3.Distance(this.gameObject.transform.position, Milk.transform.position);//거리 파악
        }
        is_follow_milk = (Milk != null && distance_milk < follow_distance && isHungry == true);

        //계란 아이템 추적 
        Egg = GameObject.FindWithTag("egg_item_follow");//밥 아이템 찾기 -> 문제: 여러 개 생성되었으면 제일 위에것만 따라감 
        if (Egg != null)//밥 생성 되었는지 
        {
            //Debug.Log("계란 생성");
            distance_egg = Vector3.Distance(this.gameObject.transform.position, Egg.transform.position);//거리 파악
        }
        is_follow_egg = (Egg != null && distance_egg < follow_distance && isHungry == true);
        //
    }

    private void FixedUpdate()
    {
        BasicTime++;
        Timer++;

        if (Timer > 1000 && exp == 0)    // 사망
        {
            transform.Find("die_msg").gameObject.SetActive(true);
            item_manager.chicken_die = true;
            item_manager.chicken_level = 0;
            Destroy(transform.gameObject, 2.0f);
        }
        if (exp == valueMax)  //성장 완료
        {
            GameObject.Find("Canvas").transform.GetChild(0).gameObject.SetActive(true);
            GetCoin cc = GameObject.FindWithTag("expmax_panel").transform.GetChild(2).GetComponent<GetCoin>();
            cc.tagname = transform.tag;
            gameObject.SetActive(false);
            //Destroy(transform.gameObject);
        }

        if (Timer % 40 == 0)
        {
            //시간에 따라 계속 속성 값 감소
            if (hungry - 1 < 0) hungry = 0;
            else hungry--;

            if (poop - 1 < 0) poop = 0;
            else poop--;

            if (play - 1 < 0) play = 0;
            else play--;

            //똥 안치우면 poop속성값 더 많이 감소
            if (poop - countPoop * 5 < 0) poop = 0;
            else poop -= countPoop * 5;

            //속성값 0인 항목이 있는 경우 경험치 감소
            if (hungry > 0 && poop > 0 && play > 0)
                if (exp + 1 > valueMax) exp = valueMax;
                else exp += 1;
            else
                if (exp - 30 < 0) exp = 0;
            else exp -= 30;
        }

        //공격 레벨 설정
        if (exp > exp_check && exp_check < valueMax)
        {
            item_manager.chicken_level++;   //공격 레벨 증가
            exp_check += 100;           //임계점 상향  
        }
    }
    //행동 
    public bool Chicken_Quarrel()
    {
        if (!item_manager.tiger_die)
        {
            GameObject tiger = GameObject.FindWithTag("tiger").gameObject;
            Tiger_Move T_m = tiger.GetComponent<Tiger_Move>();
            if (quarrel)//쫓기고 있을 때 
            {
                if (hurt)//피해를 입었다면
                {
                    trace_length += 0.3f;
                    float x = Start_Point.x + (hurt_vec.x * trace_length);  //호랑이가 밀어버린 벡터의 방향으로 밀림
                    float y = Start_Point.y + (hurt_vec.y * trace_length);
                    tmp_Point = gameObject.transform.position;

                    if (x >= 13f) // 울타리를 넘어가지 않기 위해 
                        x = 13f;
                    if (x <= -13f)
                        x = -13f;
                    if (y >= 5.7f)
                        y = 5.7f;
                    if (y <= -6.5f)
                        y = -6.5f;
                    if (x >= 3 && y >= -6.5f && y <= -4f)
                    {
                        x = tmp_Point.x; y = tmp_Point.y;
                    }
                    gameObject.transform.position = new Vector3(x, y, Start_Point.z); // 이동

                    if (trace_length > 3f)//일정 거리 밀렸다면 변수 비활성 
                    {
                        trace_length = 0;
                        hurt = false;
                        quarrel = false;
                    }

                }
                else
                {
                    if ((Vector3.Distance(Start_Point, tiger.transform.position)) < 1f)
                    {
                        hurt = true;//호랑이에게 잡혔다면 hurt활성화
                    }
                    else
                    {
                        Start_Point = gameObject.transform.position;
                        hurt_vec = T_m.trace;// 호랑이가 쫓아오는 벡터
                        trace_length = 0;
                    }
                }
            }
        }
        
        return true;
    }
    public bool Chicken_FollowMouse()
    {
        if (playing) // 놀고 있는 상태
        {
            if (trace_mouse == true) // 추적 중일때
            {
                float x = Input.mousePosition.x / 1368.0f; // 화면 비율에 맞춘 마우스 좌표 0 ~ 1
                float y = Input.mousePosition.y / 768.0f;
                Mouse = new Vector3(x, y, -8);
                x = gameObject.transform.position.x / 26f + 0.5f; // 화면 비율에 맞춘 호랑이좌표 0~1 
                y = gameObject.transform.position.y / 13f + 0.5f;
                Start_Point = new Vector3(x, y, -8);

                trace = (Mouse - Start_Point).normalized; // 호랑이와 마우스 사이의 벡터
                if (trace.x >= 0)
                    gameObject.transform.localScale = new Vector3(-1, 1, 1); // 왼쪽으로 움직인다면 왼쪽을 봄
                else
                    gameObject.transform.localScale = new Vector3(1, 1, 1); // 오른쪽이라면 오른쪽을 봄


                x = (Start_Point.x + (trace.x * trace_length) - 0.5f) * 26f; // (시작점 + 방향벡터 * 거리)를 화면이 아닌 유니티의 좌표로 바꿔줌
                y = (Start_Point.y + (trace.y * trace_length) - 0.5f) * 13f;

                gameObject.transform.position = new Vector3(x, y, Start_Point.z); // 이동

                trace_length += 0.0001f; // 빨라지는 추적속도
                if (Vector3.Distance(Start_Point, Mouse) < 0.02f) // 마우스를 잡았다면
                {
                    if (check == 5) //총 5번 잡았다면
                    {
                        playing = false; // 놀이끝 
                        check = 0; //초기화
                        gameObject.transform.position = new Vector3(transform.position.x, transform.position.y, Start_Point.z); // 이동
                        isPlay = false;
                        statTime = statMax;

                        if (play + 100 > valueMax) play = valueMax;
                        else play += 100;
                        if (exp + 100 > valueMax) exp = valueMax;
                        else exp += 100;
                    }
                    else
                    {
                        check++; //잡은 횟수 +
                        trace_mouse = false; //잠시 추적종료
                        playTime = 0;
                    }
                }
            }
            else // 추적중이지 않을떄 = 마우스를 잡고 잠시 기다림
            {
                playTime++;
                if (playTime > 50) // 기다리는 시간
                {
                    trace_mouse = true; // 다시 추적
                }
            }
        }
        return true;
    }

    public bool Chicken_Eat()
    {
        return true;
    }

    public bool Chicken_Follow_Food()
    {
        if (is_follow_food)//밥 생성 되었는지 
        {
            if (Bap.transform.position.x < transform.position.x)//밥이 왼쪽 이라면 
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            else//밥이 오른쪽이라면 
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            transform.position = Vector3.MoveTowards(transform.position, Bap.transform.position, 0.06f);
            return true;
        }
        else
        {
            return true;
        }
    }

    public bool Chicken_Follow_Milk()
    {
        if (is_follow_milk)//밥 생성 되었는지 
        {
            if (Milk.transform.position.x < transform.position.x)//밥이 왼쪽 이라면 
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            else//밥이 오른쪽이라면 
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            transform.position = Vector3.MoveTowards(transform.position, Milk.transform.position, 0.06f);
            return true;
        }
        else
        {
            return true;
        }
    }

    public bool Chicken_Follow_Egg()
    {
        if (is_follow_egg)//밥 생성 되었는지 
        {
            if (Egg.transform.position.x < transform.position.x)//밥이 왼쪽 이라면 
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            else//밥이 오른쪽이라면 
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            transform.position = Vector3.MoveTowards(transform.position, Egg.transform.position, 0.06f);
            return true;
        }
        else
        {
            return true;
        }
    }


    public bool Chicken_BasicMove()
    {
        if (isPoop)    //화장실로 이동
        {
            //목표지점을 향하는 벡터 이용해 이동
            Vector3 toilet_vec = 5 * (toiletPos - transform.position).normalized * Time.deltaTime;    //현재위치에서 화장실 위치 향해...

            if (toilet_vec.x >= 0)
                gameObject.transform.localScale = new Vector3(-1, 1, 1); // 왼쪽으로 움직인다면 왼쪽을 봄
            else
                gameObject.transform.localScale = new Vector3(1, 1, 1); // 오른쪽이라면 오른쪽을 봄

            transform.position += toilet_vec;

            if ((int)transform.position.x == (int)toiletPos.x
                && (int)transform.position.y == (int)toiletPos.y) //목표 지점 도달한 경우
            {
                GameObject mini_poop = Instantiate(ChickenPoopPrefab);
                mini_poop.transform.parent = transform;
                mini_poop.transform.position = transform.position;  //현재 위치에 똥 싸기
                countPoop++;
                isPoop = false;
                statTime = statMax;
                fPoop.SetActive(false); //똥 싼 후 말풍선 비활성화   
            }
        }
        else   //랜덤 이동
        {
            if (!playing && !isdrag && !is_follow_food && !is_follow_egg && !is_follow_milk && !quarrel)
            {
                if (moving) // 노는중 아닐 때 , 움직이는 중,계란 낳는 중 아닐때 ->움직여라 
                {
                    float x = Start_Point.x + move_vec.x * move_length; // 시작점 + 방향벡터 * 거리
                    float y = Start_Point.y + move_vec.y * move_length;
                    tmp_Point = gameObject.transform.position;
                    if (x >= 13f) // 울타리를 넘어가지 않기 위해 
                        x = 13f;
                    if (x <= -13f)
                        x = -13f;
                    if (y >= 6.5f)
                        y = 6.5f;
                    if (y <= -6.5f)
                        y = -6.5f;
                    if (x >= 3 && (y >= -6.5f && y <= -1.5f))
                    {
                        x = tmp_Point.x;
                        y = tmp_Point.y;
                    }
                    if (x >= 5.5f && (y <= 6.5f && y >= 5f))
                    {
                        x = tmp_Point.x;
                        y = tmp_Point.y;
                    }
                    gameObject.transform.position = new Vector3(x, y, Start_Point.z); // 이동

                    move_length += 0.05f; // 거리를 차근차근 움직임
                    if (move_length > 10f) // 다 움직였다면 다시 움직임 타이머를 잼
                    {
                        moving = false;
                        BasicTime = 0;
                    }
                    return true;
                }

                else
                {
                    if (BasicTime % 55 == 0 && BasicTime > 0) // 일정시간마다 혼자 돌아다님
                    {
                        BasicTime = 0;
                        moving = true;
                        Start_Point = gameObject.transform.position; // 시작점 저장
                        move_vec = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)); // 상하좌우,대각 랜덤으로 정함
                        if (move_vec.x >= 0)
                        {
                            gameObject.transform.localScale = new Vector3(-1, 1, 1); // 왼쪽으로 움직인다면 왼쪽을 봄
                            isRight = false;
                        }
                        else
                        {
                            gameObject.transform.localScale = new Vector3(1, 1, 1); // 오른쪽이라면 오른쪽을 봄
                            isRight = true;
                        }
                        move_length = 0;
                    }
                    return true;
                }
            }
            else
            {
                BasicTime = 0;
                moving = false;
            }
        }

        return true;
    }

    public bool Chicken_Egg()
    {
        if (isEggTime && !is_follow_food && !is_follow_milk && !is_follow_egg && !isPoop && !moving && !quarrel)//따라갈 때는 알 낳지 말아라 
        {
            moving = false;

            Vector3 eggPos;
            if (!isRight)//오른쪽을 보고 있는 경우 -> 반대로 해야함 
            {
                eggPos = new Vector3(transform.position.x - 1, transform.position.y - 0.8f, transform.position.z);

            }
            else//왼쪽을 보고 있는 경우 
            {
                eggPos = new Vector3(transform.position.x + 1, transform.position.y - 0.8f, transform.position.z);
            }

            GameObject egg = GameObject.Instantiate(Egg_Prefab);
            isEggTime = false;
            egg.transform.parent = GameObject.Find("ItemManager").GetComponent<ItemManager>().transform;
            egg.transform.position = eggPos;
        }
        else
        {
            if (EggTime > C_EggTime)
            {
                moving = false;
                isEggTime = true;
                EggTime = 0;
            }
        }

        return true;
    }

    public bool Chicken_Hungry()
    {
        if ((Timer != 0 && Timer % hungryTimer == 0)
            && (!isHungry && !isPoop && !isPlay && !quarrel))
        {
            isHungry = true;
            fHungry.SetActive(true);
        }
        if (isHungry)    // 상태 유지
        {
            statTime--;
            if (statTime == 0)
            {
                isHungry = false;
                fHungry.SetActive(false);
                statTime = statMax;
            }
        }
        return true;
    }

    public bool Chicken_Poop()
    {
        if ((Timer != 0 && Timer % poopTimer == 0)
                   && (!isHungry && !isPoop && !isPlay && !quarrel))
        {
            isPoop = true;
            fPoop.SetActive(true);
            moving = false;
            BasicTime = 0;
            //화장실 내 랜덤한 위치 설정
            tx = Random.Range(-12.6f, -7.0f);
            ty = Random.Range(4.19f, 6.76f);
            toiletPos = new Vector3(tx, ty, transform.position.z);
        }
        return true;
    }

    public bool Chicken_Play()
    {
        if ((Timer != 0 && Timer % playTimer == 0)
            && (!isHungry && !isPoop && !isPlay && !quarrel))
        {
            isPlay = true;
            fPlay.SetActive(true);
        }
        if (isPlay == true && playing == false)    // 상태 유지
        {
            statTime--;
            if (statTime == 0)
            {
                isPlay = false;
                fPlay.SetActive(false);
                statTime = statMax;
            }
        }
        return true;
    }
}