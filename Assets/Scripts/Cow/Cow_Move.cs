using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class Cow_Move : MonoBehaviour
{
    int statTime = 0;

    int timer = 0;                   //타이머
    int milkTimer = 0;               //우유 생성 타이머

    //속성값 관련
    public int hungry = 0; int hungryMax = 100;
    public int poop = 0; int poopMax = 100;
    public int play = 0; int playMax = 100;

    //속성관련 오브젝트 -자식
    GameObject fHungry;             //체력 오브젝트
    GameObject fPoop;               //청결 오브젝트
    GameObject fPlay;               //흥미 오브젝트 

    public GameObject MilkPrefab;   //우유 오브젝트

    //이동 관련
    int MovedTime = 0;

    public float movePower = 1f;    //움직이는 속도 
    int movementFlag = 0;           //0:idle, 1:left, 2:right

    bool ismoving = true;
    bool isRight = false;           //보는 방향:왼쪽/오른쪽 

    Vector3 movement;               //z:-8

    // Start is called before the first frame update
    //상태 
    public bool Cow_Hungry()
    {
        if (hungry != 100 && hungry % 20 == 0)
        {
            fHungry.SetActive(true);
        }

        return true;
    }

    public bool Cow_Poop()
    {
        if (poop != 100 && poop % 20 == 0)
        {
            fPoop.SetActive(true);
        }
        return true;
    }

    public bool Cow_Play()
    {
        if (play != 100 && play % 20 == 0)
        {
            fPlay.SetActive(true);
        }
        return true;
    }

    public bool Cow_Milk()
    {
        if(milkTimer != 0 && milkTimer % 1000 == 0)
        {
            //우유 생산
            GameObject milk = Instantiate(MilkPrefab);
            milk.transform.position = transform.position;
        }
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

    public bool Cow_BasicMove()
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

            return true;
        }
    }

    void Start()
    {
        //속성값 초기 설정
        hungry = hungryMax;
        poop = poopMax;
        play = playMax;

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
        timer++;
        milkTimer++;

        if (timer % 100 == 0)
        {
            hungry--;
            poop--;
            play--;
        }
    }
}
