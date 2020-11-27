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
    float trace_length = 0;
    int check = 0;
    public bool trace_mouse; // 마우스를 향해 달리는 중인지

    Vector2 move_vec; // 움직일 방향벡터
    Vector3 Start_Point; // 움직일때의 시작점
    Vector3 trace; // 마우스와 오브젝트 사이의 벡터 
    Vector3 Mouse;

    public GameObject t_Poop;//똥
    public GameObject t_Hungry;//밥
    public GameObject t_Play;//놀이

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

            if (playTime % 100 == 0 && (playTime) > 0) // 일정 시간마다 심심해짐
            {
                t_Play.gameObject.SetActive(true); // 말풍선 띄움
                playTime = 0;
                trace_length = 0;
                trace_mouse = true; // 추적 시작
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

                if (x >= 13f) // 울타리를 넘어가지 않기 위해 
                    x = 13f;
                if (x <= -13f)
                    x = -13f;
                if (y >= 6.5f)
                    y = 6.5f;
                if (y <= -6.5f)
                    y = -6.5f;

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

    }
    private void FixedUpdate()
    {
        hungryTime++; // 타이머 
        BasicTime++;
        playTime++;
        
    }
}