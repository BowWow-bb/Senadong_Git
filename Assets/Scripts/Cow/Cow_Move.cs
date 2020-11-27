using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class Cow_Move : MonoBehaviour
{
    int milkTimer = 0;                  //우유 생성 타이머

    //속성값 관련
    int floating;                       //말풍선 랜덤 1:hungry, 2:poop, 3:play, 4~100:none
    int statTime = 500, statMax = 200;  //말풍선 지속 시간
    int timer = 0;                      //타이머
    int valueMax = 1000;
    public int hungry = 0;  bool isHungry = false;
    public int poop = 0;    bool isPoop = false;
    public int play = 0;    bool isPlay = false;

    //청결 관련
    public int countPoop = 0;           //똥 개수
    public GameObject CowPoopPrefab;    //똥 오브젝트
    Vector3 toiletPos;                  //화장실 내 목표 위치
    float tx, ty;                       //화장실 내 목표 위치 x,y (랜덤)

    //밥 추적 위함 
    GameObject Bap;
    public float follow_distance = 15;//밥 추적 범위 
    float distance;
    bool is_follow_food = false;//밥 추적 중인지
    //

    //속성관련 오브젝트 -자식
    GameObject fHungry;                 //체력 오브젝트
    GameObject fPoop;                   //청결 오브젝트
    GameObject fPlay;                   //흥미 오브젝트 

    public GameObject MilkPrefab;       //우유 오브젝트

    //이동 관련
    int MovedTime = 0;

    public float movePower = 1f;        //움직이는 속도 
    int movementFlag = 0;               //0:idle, 1:left, 2:right

    bool ismoving = true;
    bool isRight = false;               //보는 방향:왼쪽/오른쪽 

    Vector3 movement;                   //z:-8

    // Start is called before the first frame update
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


        movementFlag = Random.Range(0, 5);  //0,1,2,3,4
    }

    // Update is called once per frame
    void Update()
    {
        floating = Random.Range(1,101); 
        timer++;
        milkTimer++;

        if (timer % 100 == 0)
        {
            hungry--;
            poop--;
            play--;
            poop -= countPoop * 5; //똥 개수에 비례하여 감소
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

    public bool Cow_Hungry()
    {
        if ((hungry != valueMax && floating == 1)
            && (!isHungry && !isPoop && !isPlay))
        {
            isHungry = true;
            fHungry.SetActive(true);
        }

        if(isHungry)    // 상태 유지
        {
            statTime--;
            if(statTime == 0)
            {
                floating = 4;
                isHungry = false;
                fHungry.SetActive(false);
                statTime = statMax;
            }
        }

        return true;
    }

    public bool Cow_Poop()
    {
        //if ((poop != valueMax && floating == 2)
        //    && (!isHungry && !isPoop && !isPlay))
        //{
        //    isPoop = true;
        //    fPoop.SetActive(true);

        //    //화장실 내 랜덤한 위치 설정
        //    tx = Random.Range(-12.6f, -7.0f);
        //    ty = Random.Range(4.19f, 6.76f);
        //    toiletPos = new Vector3(tx, ty, transform.position.z);
        //    Debug.Log("toiletPos: " + toiletPos);
        //}
        return true;
    }

    public bool Cow_Play()
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

    public bool Cow_Milk()
    {
        //if(milkTimer != 0 && milkTimer % 1000 == 0)
        //{
        //    //우유 생산
        //    GameObject milk = Instantiate(MilkPrefab);
        //    milk.transform.position = transform.position;
        //}
        return true;
    }

    public bool Cow_FollowMouse()
    {
        return true;
    }

    public bool Cow_Eat()
    {
        return true;
    }

    public bool Cow_Follow_Food()//알아서 바꿔서 쓰셈 
    {
        if (is_follow_food)//밥 생성 되었는지 
        {
            Debug.Log("밥 생성, 거리 추적 범위");
            //Debug.Log("밥 위치: ", Bap.transform);
            if (Bap.transform.position.x < transform.position.x)//밥이 왼쪽 이라면 
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

    public bool Cow_BasicMove()
    {
        if(isPoop)    //화장실로 이동
        {
            //목표지점을 향하는 벡터 이용해 이동
            transform.position += (toiletPos - transform.position).normalized * Time.deltaTime;

            if(transform.position == toiletPos) //목표 지점 도달한 경우
            {
                GameObject mini_poop = Instantiate(CowPoopPrefab);
                mini_poop.tag = "cow_poop";
                mini_poop.transform.position = transform.position;  //현재 위치에 똥 싸기

                floating = 4;
                countPoop++;
                isPoop = false;
                fPoop.SetActive(false); //똥 싼 후 말풍선 비활성화         
            }

        }
        else   //랜덤 이동
        {
            if (ismoving)//움직이는 중인 상태-> 상태 안 바꾸게 
            {
                Vector3 moveVelocity = Vector3.zero;
                MovedTime++;
                if (MovedTime > 150)//움직인 시간 일정 시간 넘으면 
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
                else if (movementFlag == 3)//왼쪽 보는 중
                {
                    moveVelocity = new Vector3(-1, 1, 0);
                    transform.localScale = new Vector3(0.8f, 0.8f, 1);
                    isRight = false;
                }
                else if (movementFlag == 4)//오른쪽 보는 중 
                {
                    moveVelocity = new Vector3(1, -1, 0);
                    transform.localScale = new Vector3(-0.8f, 0.8f, 1);
                    isRight = true;
                }

                gameObject.transform.position += moveVelocity * movePower * Time.deltaTime;

                return true;
            }
            else//상태 바꿀 시간 되면 
            {
                MovedTime = 0;
                movementFlag = Random.Range(0, 5);//0,1,2,3,4
                ismoving = true;
            }
        }
        return true;

    }
}
