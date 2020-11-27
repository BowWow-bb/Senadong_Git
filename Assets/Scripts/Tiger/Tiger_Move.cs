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

    public bool playing = false; // 노는중 인지
    bool hunger; // 배고픈 상태인지 
    bool moving; // 움직이고 있는 상태 인지
    float move_length = 0; // 얼마나 움직였는지의 벡터
    public bool trace_mouse; // 마우스를 향해 달리는 중인지

    Vector2 move_vec; // 움직일 방향벡터
    Vector3 Start_Point; // 움직일때의 시작점
    Vector3 trace; // 마우스와 오브젝트 사이의 벡터 
    Vector3 Mouse;

    public GameObject t_Poop;//똥
    public GameObject t_Hungry;//밥
    public GameObject t_Play;//놀이

    //밥 추적 위함 
    GameObject Bap;
    public float follow_distance = 15;//밥 추적 범위 
    float distance;
    bool is_follow_food = false;//밥 추적 중인지
    //

    // Start is called before the first frame update
    public bool Hungry()
    {
        if (hunger && !playing) // 배고픈 상태 일 때
        {
            t_Hungry.gameObject.SetActive(true);
            return true;
        }
        else
        {
            if ((hungryTime / 100) % 100 == 0 && (hungryTime) > 0) // 일정시간마다 배고파짐
            {
                hungryTime = 0;
                //  Debug.Log("호랑이 배고파!");
                hunger = true;
                t_Hungry.gameObject.SetActive(false);
            }
            return true;
        }

    }
    public bool Poop()
    {
        return true;
    }

    public bool Tiger_Follow_Food()//알아서 바꿔서 쓰셈 
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

    public bool FollowMouse()
    {
        if (playing) // 놀고 있는 상태
        {
            t_Play.gameObject.SetActive(false);
            if (trace_mouse)
            {
                Debug.Log("쫓");
                float x = Start_Point.x - (trace.x * move_length); // 시작점 + 방향벡터 * 거리
                float y = Start_Point.y - (trace.y * move_length);


                gameObject.transform.position = new Vector3(x, y, Start_Point.z); // 이동

                move_length += 0.1f; // 거리를 차근차근 움직임
                Debug.Log(move_length);
                if (move_length > 10f) // 다 움직였다면 다시 움직임 타이머를 잼
                {
                    move_length = 0;
                    Debug.Log("ddd");
                    trace_mouse = false;
                    playTime = 0;
                }
            }
            else
            {
                if (playTime % 50 == 0 && (playTime) > 0)
                {
                    Mouse = Input.mousePosition;
                    //float x = Input.mousePosition.x / 250.0f - 1.0f;
                    //float y = Input.mousePosition.y / 250.0f - 1.0f;
                    //float dx = Mathf.Round(x * 10.0f) / 10.0f;
                    //float dy = Mathf.Round(y * 10.0f) / 10.0f;
                    //int ix = (int)Mathf.Round(dx * 10.0f + 10.0f);
                    //int iy = (int)Mathf.Round(10.0f - (dy * 10.0f));
                    Mouse.z = -8f;
                    Debug.Log(Mouse);
                    Start_Point = gameObject.transform.position;
                    trace = (Mouse - Start_Point).normalized;
                    if (trace.x >= 0)
                        gameObject.transform.localScale = new Vector3(-1, 1, 1); // 왼쪽으로 움직인다면 왼쪽을 봄
                    else
                        gameObject.transform.localScale = new Vector3(1, 1, 1); // 오른쪽이라면 오른쪽을 봄
                    move_length = 0;
                    trace_mouse = true;
                }
            }
            return true;
        }
        else
        {

            if (playTime % 400 == 0 && (playTime) > 0) // 일정 시간마다 심심해짐
            {
                t_Play.gameObject.SetActive(true);
                Debug.Log("호랑이 심심해!");
            }
            return true;
        }

    }
    public bool Eat()
    {
        return true;
    }
    public bool BasicMove()
    {
        if (!playing)
        {
            if (moving) // 노는중 아닐 때 , 움직이는 중
            {
                float x = Start_Point.x + move_vec.x * move_length; // 시작점 + 방향벡터 * 거리
                float y = Start_Point.y + move_vec.y * move_length;

                if (x >= 2.5f) // 울타리를 넘어가지 않기 위해 
                    x = 2.5f;
                if (x <= -13f)
                    x = -13f;
                if (y >= -4.5f)
                    y = -4.5f;
                if (y <= -9f)
                    y = -9f;

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
        return true;
    }
    void Start()
    {
        hunger = false; // 변수 초기화
        moving = false;
        playing = false;
    }

    // Update is called once per frame
    void Update()
    {
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
    private void FixedUpdate()
    {
        hungryTime++; // 타이머 
        BasicTime++;
        playTime++;
    }
}