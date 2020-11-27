using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class Chicken_Move : MonoBehaviour
{
    Animator animator;

    //밥 추적 위함 
    GameObject Bap;
    public float follow_distance=15;//밥 추적 범위 
    float distance;
    bool is_follow_food = false;//밥 추적 중인지
    //
    public bool trace_mouse;
    public bool playing = false;
    public bool isdrag = false;//drag 중인지 파악 
    int MovedTime = 0;

    int EggTime = 0;//달걀 낳는 시간
    public int C_EggTime = 1000;//달걀 낳는 속도 조정 

    public float movePower = 1f;//움직이는 속도

    int movementFlag = 0;//0:idle, 1:left, 2:right

    bool isStop = false;//movementFlag=0
    bool isEggTime;//달걀 낳는 거 
    bool ismoving = true;
    bool isRight = false;//보는 방향:왼쪽/오른쪽 

    public GameObject Egg_Prefab;//달걀 

    //속성값 관련
    int floating;                       //말풍선 랜덤 1:hungry, 2:poop, 3:play, 4~100:none
    int statTime = 500, statMax = 200;  //말풍선 지속 시간
    int timer = 0;                      //타이머
    int valueMax = 1000;
    public int hungry = 0; bool isHungry = false;
    public int poop = 0; bool isPoop = false;
    public int play = 0; bool isPlay = false;
    Vector2 move_vec; // 움직일 방향벡터
    Vector3 Start_Point; // 움직일때의 시작점
    Vector3 trace; // 마우스와 오브젝트 사이의 벡터 
    Vector3 Mouse;
    bool moving; // 움직이고 있는 상태 인지
    float move_length = 0; // 얼마나 움직였는지의 벡터
    float trace_length = 0;
    public int playTime = 0; // 심심한 시간 재는 시간
    int check = 0;
    //속성관련 오브젝트 -자식
    GameObject fHungry;                 //체력 오브젝트
    GameObject fPoop;                   //청결 오브젝트
    GameObject fPlay;                   //흥미 오브젝트

    void Start()
    {
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

        animator = GetComponent<Animator>();
        isdrag = GameObject.Find("Click_Move").GetComponent<Click_Move>().chicken_drag;
        movementFlag = Random.Range(0, 5);//0,1,2,3,4
    }

    void Update()
    {
        floating = Random.Range(1, 101);
        timer++;

        if (timer % 100 == 0)
        {
            hungry--;
            poop--;
            play--;
            //poop -= countPoop * 5; //똥 개수에 비례하여 감소
        }

        EggTime++;
        if(isdrag)
        {
            animator.SetBool("is_drag", true);
        }
        else
        {
           animator.SetBool("is_drag", false);
        }

        if(isStop && !is_follow_food)//따라가는 상태가 아니라면 
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
        is_follow_food = (Bap != null && distance < follow_distance);//밥이 생성 되었고 거리가 follow_distance 미만이라면 is_follow_food true
        //
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

            return true;
        }
        else
        {
            if ((play != valueMax && floating == 3)
            && (!isHungry && !isPoop && !isPlay))
            {
                isPlay = true;
                fPlay.SetActive(true);
                Debug.Log("치킨 놀");
            }

            if (isPlay)    // 상태 유지
            {
                statTime--;
                if (statTime == 0)
                {
                    floating = 4;
                    isPlay = false;
                    fPlay.SetActive(false);
                    statTime = statMax;
                }
            }
            return true;
        }

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
            if(Bap.transform.position.x<transform.position.x)//밥이 왼쪽 이라면 
            {
                transform.localScale = new Vector3(0.8f, 0.8f, 1);
            }
            else//밥이 오른쪽이라면 
            {
                transform.localScale = new Vector3(-0.8f, 0.8f, 1);
            }
            transform.position = Vector3.Lerp(transform.position, Bap.transform.position, 0.008f);
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

            isStop = false;

            if (MovedTime>200)//움직인 시간 일정 시간 넘으면 
            {
                ismoving = false;//상태 바꾸기 
            }

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
            else if(movementFlag == 0)
            {
                isStop = true;
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

    public bool Chicken_Hungry()
    {
        if ((hungry != valueMax && floating == 1)
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
                floating = 4;
                isHungry = false;
                fHungry.SetActive(false);
                statTime = statMax;
            }
        }

        return true;
    }
    public bool Chicken_Poop()
    {
 
        return true;
    }
    public bool Chicken_Play()
    {
        if ((play != valueMax && floating == 3)
            && (!isHungry && !isPoop && !isPlay))
        {
            isPlay = true;
            fPlay.SetActive(true);
        }

        if (isPlay)    // 상태 유지
        {
            statTime--;
            if (statTime == 0)
            {
                floating = 4;
                isPlay = false;
                fPlay.SetActive(false);
                statTime = statMax;
            }
        }
        return true;
    }
}
