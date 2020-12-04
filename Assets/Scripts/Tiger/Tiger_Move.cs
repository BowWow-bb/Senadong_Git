using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class Tiger_Move : MonoBehaviour
{
    int hungryTime = 0; // 배고픔 재는 시간
    int BasicTime = 0; // 기본 움직임 재는 시간
    public int playTime = 0; // 심심한 시간 재는 시간
    int wait=0;
    public bool quarreling = false; // 시비거는중 인지
    public bool playing = false; // 노는중 인지
    bool hunger; // 배고픈 상태인지 
    public bool moving; // 움직이고 있는 상태 인지
    float move_length = 0; // 얼마나 움직였는지의 벡터
    float trace_length = 0;
    int check = 0;
    public bool trace_mouse; // 마우스를 향해 달리는 중인지

    Vector2 move_vec; // 움직일 방향벡터
    Vector3 Start_Point; // 움직일때의 시작점
    public Vector3 trace; // 마우스와 오브젝트 사이의 벡터 
    Vector3 Mouse;
    Vector3 tmp_Point;

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

    //속성값 관련
    public int Timer;
    int statTime = 450, statMax = 450;  //말풍선 지속 시간
    public int valueMax = 1000;
    public int hungry; public bool isHungry = false; int hungryTimer = 1400;
    public int poop; bool isPoop = false; int poopTimer = 1150;
    public int play; bool isPlay = false; int playTimer = 1300;
    public int exp = 0;

    //청결 관련
    public int countPoop = 0;           //똥 개수
    public GameObject TigerPoopPrefab;    //똥 오브젝트
    Vector3 toiletPos;                  //화장실 내 목표 위치
    float tx, ty;                       //화장실 내 목표 위치 x,y (랜덤)

    //속성관련 오브젝트 -자식
    public GameObject fHungry;                 //체력 오브젝트
    public GameObject fPoop;                   //청결 오브젝트
    public GameObject fPlay;                   //흥미 오브젝트 

    //시비 걸 동물
    public GameObject Chicken;
    public GameObject Cow;
    public GameObject tmp;
    int quarrel_check=0;
    
    //애니메이터 
    Animator animator;

    public bool isdrag=false;
    // Start is called before the first frame update

    void Update()
    {
        //애니메이터 
        if (!moving && !isPoop && !is_follow_food && !is_follow_egg && !is_follow_milk)
        {
            animator.SetBool("is_sleepy", true);
        }
        else
        {
            animator.SetBool("is_sleepy", false);
        }
        if(isPlay)
        {
            animator.SetBool("is_attack", true);
        }
        else
        {
            animator.SetBool("is_attack", false);
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
    public bool Quarrel()
    {

        if (quarreling && (!isHungry && !isPoop && !isPlay))
        {
            Debug.Log("qua");
            float x = gameObject.transform.position.x;
            float y = gameObject.transform.position.y;
            Start_Point = new Vector3(x, y, -8);
            x = tmp.transform.position.x;
            y = tmp.transform.position.y;
            Vector3 quarrel_point = new Vector3(x, y, -8);
            trace = (quarrel_point - Start_Point); // ㅎㅎㅗㄹㅏㅇㅇㅣㅇㅗ
            if (trace.x >= 0)
                gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
            //gameObject.transform.localScale = new Vector3(-1, 1, 1); // 왼쪽으로 움직인다면 왼쪽을 봄
            else
                gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
            //gameObject.transform.localScale = new Vector3(1, 1, 1); // 오른쪽이라면 오른쪽을 봄


            x = Start_Point.x + (trace.x * trace_length);  // (시작점 + 방향벡터 * 거리)를 화면이 아닌 유니티의 좌표로 바꿔줌
            y = Start_Point.y + (trace.y * trace_length);

            gameObject.transform.position = new Vector3(x, y, Start_Point.z); // 이동

            trace_length += 0.01f; // 빨라지는 추적속도
            if (Vector3.Distance(gameObject.transform.position, quarrel_point) < 1f)
            {
                quarrel_check = 0;
                // else if (tmp.tag == "cow")
                //   tmp.GetComponent<Cow_Move>().quarrel = true;
                Debug.Log("잡" + trace_length);

                gameObject.transform.position = new Vector3(transform.position.x, transform.position.y, Start_Point.z); // 이동
                trace_length = 0;
                quarreling = false;
            }
        }
        else
        {

            if (Vector3.Distance(Cow.transform.position, gameObject.transform.position) < 5f) // 닭 / 소가 호랑이의 일정범위 내의 들어온다면 
            {
                tmp = Cow; // 바로 전에 보인 동물
                quarrel_check++; // 눈앞에 걸리적거림 +1
            }
            else if (Vector3.Distance(Chicken.transform.position, gameObject.transform.position) < 10f)
            {
                tmp = Chicken;
                quarrel_check++;
                Debug.Log("qq" + quarrel_check);
            }

            if (quarrel_check > 500) // 조정 
            {
                quarrel_check = 0;
                if (!isHungry && !isPoop && !isPlay && !quarreling )
                {
                    if (tmp.tag == "chicken")
                    {
                        Chicken_Move c_m = tmp.GetComponent<Chicken_Move>();
                        if (!c_m.playing && !c_m.isdrag && !c_m.is_follow_food
                            && !c_m.isEggTime && !c_m.is_follow_food && !c_m.is_follow_milk && !c_m.is_follow_egg && !c_m.isPoop)
                        {
                            quarrel_check = 0;
                            c_m.quarrel = true;
                            quarreling = true;
                        }
                    }
                }
            }

        }
        return true;
    }
    public bool Hungry()
    {
        if ((Timer != 0 && Timer % hungryTimer == 0)
            && (!isHungry && !isPoop && !isPlay && !quarreling))
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
    public bool Poop()
    {
        if ((Timer != 0 && Timer % poopTimer == 0)
            && (!isHungry && !isPoop && !isPlay && !quarreling))
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

    public bool Play()
    {
        if ((Timer != 0 && Timer % playTimer == 0)
             && (!isHungry && !isPoop && !isPlay && !quarreling))
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

    public bool Tiger_Follow_Food()//알아서 바꿔서 쓰셈 
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

    public bool Tiger_Follow_Milk()
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

    public bool Tiger_Follow_Egg()
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

    public bool FollowMouse()
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
                        trace_length = 0;
                        play += 100;
                        exp += 50;
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
    public bool Eat()
    {
        return true;
    }
    public bool BasicMove()
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
                GameObject mini_poop = Instantiate(TigerPoopPrefab);
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
            if (!playing&& !isdrag&&! is_follow_food && !quarreling && !is_follow_egg && !is_follow_milk)
            {
                if (moving) // 노는중 아닐 때,음식따라다니지 않을 때 , 움직이는 중
                {
                    float x = Start_Point.x + move_vec.x * move_length; // 시작점 + 방향벡터 * 거리
                    float y = Start_Point.y + move_vec.y * move_length;
                    tmp_Point = gameObject.transform.position;

                    if (x >= 13f) // 울타리를 넘어가지 않기 위해 
                        x = 13f;
                    if (x <= -13f)
                        x = -13f;
                    if (y >= 5.7f)
                        y = 5.7f;
                    if (y <= -6.5f)
                        y = -6.5f;
                    if (x >= 3 && y >= -6.5f && y <= -4.5f)
                    {
                        x = tmp_Point.x; y = tmp_Point.y;
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
                            gameObject.transform.localScale = new Vector3(-1, 1, 1); // 왼쪽으로 움직인다면 왼쪽을 봄
                        else
                            gameObject.transform.localScale = new Vector3(1, 1, 1); // 오른쪽이라면 오른쪽을 봄
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

        Chicken = GameObject.FindWithTag("chicken");
        Cow = GameObject.FindWithTag("cow");
    }

    // Update is called once per frame
    
    public void FixedUpdate()
    {
        BasicTime++;
        Timer++;

        if (Timer > 1000 && exp == 0)    // 사망
        {
            transform.Find("die_msg").gameObject.SetActive(true);
            Destroy(transform.gameObject, 2.0f);
        }
        if (exp == valueMax)  //성장 완료
        {
            GameObject.Find("Canvas").transform.Find("Panel").gameObject.SetActive(true);
            ChildClone cc = GameObject.FindWithTag("expmax_panel").transform.GetChild(1).GetComponent<ChildClone>();
            cc.tagname = transform.tag;
            Destroy(transform.gameObject);
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
                if (exp + 5 > valueMax) exp = valueMax;
                else exp += 5;
            else
                if (exp - 30 < 0) exp = 0;
            else exp -= 30;
        }
    }
}