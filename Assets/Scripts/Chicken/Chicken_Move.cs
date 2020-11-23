using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class Chicken_Move : MonoBehaviour
{
    int hungryTime = 0; // 배고픔 재는 시간
    int PoopTime = 0;
    int MovedTime = 0;
    int PlayTime = 0;

    int EggTime = 0;//달걀 낳는 시간
    int C_EggTime;//달걀 낳는 속도 조정 

    int statTime = 0;

    public float movePower = 1f;//움직이는 속도 
    int movementFlag = 0;//0:idle, 1:left, 2:right

    bool ishunger;//배고픔
    bool isPoop;//똥
    bool isPlay;//심심도
    bool isEggTime;//달걀 낳는 거 
    bool ismoving = true;
    bool isRight = false;//보는 방향:왼쪽/오른쪽 

    public GameObject Egg_Prefab;//달걀 

    public GameObject c_d;//똥
    public GameObject c_b;//밥
    public GameObject c_p;//놀이 

    Vector3 movement;//z:-8

    // Start is called before the first frame update
    //상태 
    public bool Chicken_Hungry()
    {
        if (ishunger)
        {
            statTime++;
            c_b.gameObject.SetActive(true);
            if (statTime>100)
            {
                statTime = 0;
                hungryTime = 0;
                ishunger = false;
                c_b.gameObject.SetActive(false);
            }
            return true;
        }
        else
        {
            if (hungryTime>500)
            {
                ishunger = true;
                hungryTime = 0;
            }
            return true;
        }
    }

    public bool Chicken_Poop()
    {
        if (isPoop)
        {
            statTime++;
            c_d.gameObject.SetActive(true);
            if (statTime > 100)
            {
                statTime = 0;
                PoopTime = 0;
                isPoop = false;
                c_d.gameObject.SetActive(false);
            }
            return true;
        }
        else
        {
            if (PoopTime > 300)
            {
                isPoop = true;
                PoopTime = 0;
            }
            return true;
        }
    }

    public bool Chicken_Play()
    {
        if (isPlay)
        {
            statTime++;
            c_p.gameObject.SetActive(true);
            if (statTime > 100)
            {
                statTime = 0;
                PlayTime = 0;
                isPlay = false;
                c_p.gameObject.SetActive(false);
            }

            return true;
        }
        else
        {
            if (PlayTime > 100)
            {
                isPlay = true;
                PlayTime = 0;
            }
            
            return true;
        }
    }

    //행동 
    public bool Chicken_Egg()
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

            return true;
        }
        else
        {
            if (EggTime > 1000)
            {
                isEggTime = true;
                EggTime = 0;
            }
            return true;
        }   
    }

    public bool Chicken_FollowMouse()
    {
        return true;
    }

    public bool Chicken_Eat()
    {
        return true;
    }

    public bool Chicken_BasicMove()
    {
        if (ismoving)//움직이는 중인 상태-> 상태 안 바꾸게 
        {
            Vector3 moveVelocity = Vector3.zero;
            MovedTime++;
            if(MovedTime>150)//움직인 시간 일정 시간 넘으면 
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
        //상태 안뜨게 설정 
        ishunger = false;

        movementFlag = Random.Range(0, 5);//0,1,2,3,4

        c_b.gameObject.SetActive(false);//밥 
        c_d.gameObject.SetActive(false);//똥
        c_p.gameObject.SetActive(false);//심심도
    }

    // Update is called once per frame
    void Update()
    {
        hungryTime++;
        PoopTime++;
        PlayTime++;
        EggTime++;
    }
}
