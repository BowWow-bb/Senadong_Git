using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class Chicken_Move : MonoBehaviour
{
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

    Vector2 move_vec; // 움직일 방향벡터
    Vector3 Start_Point; // 움직일때의 시작점
    Vector3 trace; // 마우스와 오브젝트 사이의 벡터 
    Vector3 Mouse;
    Vector3 tmp_Point;
    //

    //속성값 관련
    public int Timer;
    int statTime = 263, statMax = 263;  //말풍선 지속 시간
    public int valueMax = 1000;
    public int hungry; public bool isHungry = false; int hungryTimer = 900;
    public int poop; bool isPoop = false; int poopTimer = 1400;
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
    public int exp;

    //밥 추적 위함 
    GameObject Bap;
    public float follow_distance = 15;//밥 추적 범위 
    float distance;
    public bool is_follow_food = false;//밥 추적 중인지
    //

    Animator animator;

    public bool isdrag = false;//drag 중인지 파악 -> 이동시 순간이동 방지  

    int EggTime = 0;//달걀 낳는 시간
    public int C_EggTime = 1000;//달걀 낳는 속도 조정 

    bool isEggTime;//달걀 낳는 거 
    bool isRight = false;//보는 방향:왼쪽/오른쪽 

    public GameObject Egg_Prefab;//달걀

    void Start()
    {
        Timer = 7;
        //속성값 초기 설정
        hungry = valueMax;
        poop = valueMax;
        play = valueMax;

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
    }

    void Update()
    {
        EggTime++;
        if (isdrag)
        {
            animator.SetBool("is_drag", true);
        }
        else
        {
            animator.SetBool("is_drag", false);
        }

        if (!moving && !isPoop && !is_follow_food)//따라가는 상태가 아니라면 
        {
            animator.SetBool("is_drop_egg", true);
        }
        else
        {
            animator.SetBool("is_drop_egg", false);
        }

        //밥 추적 
        Bap = GameObject.FindWithTag("hungry_follow_item");//밥 아이템 찾기 -> 문제: 여러 개 생성되었으면 제일 위에것만 따라감 
        if (Bap != null)//밥 생성 되었는지 
        {
            //Debug.Log("밥 생성");
            distance = Vector3.Distance(this.gameObject.transform.position, Bap.transform.position);//거리 파악
        }
        is_follow_food = (Bap != null && distance < follow_distance && isHungry == true);//밥이 생성 되었고 거리가 follow_distance 미만이라면 is_follow_food true
        //
    }
    private void FixedUpdate()
    {
        BasicTime++;
        Timer++;

        if (Timer % 20 == 0)
        {
            hungry -= 1;
            poop -= 1;
            play -= 1;
            poop -= countPoop * 5; //똥 개수에 비례하여 감소

            if (hungry > 0 && poop > 0 && play > 0)
                exp += 1;
            else
                exp -= 5;
        }
    }
    //행동 
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
            //Debug.Log("밥 생성, 거리 추적 범위");
            //Debug.Log("밥 위치: ", Bap.transform);
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
    public bool Chicken_BasicMove()
    {
        if (isPoop)    //화장실로 이동
        {
            //목표지점을 향하는 벡터 이용해 이동
            Vector3 toilet_vec = (toiletPos - transform.position).normalized * Time.deltaTime;    //현재위치에서 화장실 위치 향해...

            if (toilet_vec.x >= 0)
                gameObject.transform.localScale = new Vector3(-1, 1, 1); // 왼쪽으로 움직인다면 왼쪽을 봄
            else
                gameObject.transform.localScale = new Vector3(1, 1, 1); // 오른쪽이라면 오른쪽을 봄

            transform.position += toilet_vec;

            if ((int)transform.position.x == (int)toiletPos.x
                && (int)transform.position.y == (int)toiletPos.y) //목표 지점 도달한 경우
            {
                GameObject mini_poop = Instantiate(ChickenPoopPrefab);
                mini_poop.tag = "chicken_poop";
                mini_poop.transform.position = transform.position;  //현재 위치에 똥 싸기
                countPoop++;
                isPoop = false;
                statTime = statMax;
                fPoop.SetActive(false); //똥 싼 후 말풍선 비활성화   
            }
        }
        else   //랜덤 이동
        {
            if (!playing && !isdrag && !is_follow_food)
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
                    if (x >= 3 && y >= -6.5f && y <= -4.5f)
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
        if (isEggTime && !is_follow_food && !isPoop && !moving)//따라갈 때는 알 낳지 말아라 
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
            egg.transform.position = eggPos;
            egg.transform.parent = null;
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
            && (!isHungry && !isPoop && !isPlay))
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
                   && (!isHungry && !isPoop && !isPlay))
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
            && (!isHungry && !isPoop && !isPlay))
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