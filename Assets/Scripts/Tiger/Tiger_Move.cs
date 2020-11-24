using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class Tiger_Move : MonoBehaviour
{
    int hungryTime =0; // 배고픔 재는 시간
    int BasicTime = 0; // 기본 움직임 재는 시간
    int playTime = 0; // 심심한 시간 재는 시간

    public bool playing = false; // 노는중 인지
    bool hunger; // 배고픈 상태인지 
    bool moving; // 움직이고 있는 상태 인지
    float move_length=0; // 얼마나 움직였는지의 벡터

    Vector2 move_vec; // 움직일 방향벡터
    Vector3 Start_Point; // 움직일때의 시작점


    // Start is called before the first frame update
    public bool Hungry()
    {
        if (hunger) // 배고픈 상태 일 때
        {
            return true;
        }
        else {
            if ((hungryTime / 100)%100 == 0 && (hungryTime) > 0) // 일정시간마다 배고파짐
            {
                hungryTime = 0;
              //  Debug.Log("호랑이 배고파!");
                hunger = true;
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
            return true;
        }
        else
        {

            if (playTime % 400 == 0 && (playTime) > 0) // 일정 시간마다 심심해짐
            {
                playTime = 0;
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
        if (moving && !playing) // 노는중 아닐 때 , 움직이는 중
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

            gameObject.transform.position = new Vector3(x,y, Start_Point.z); // 이동

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
        hungryTime ++; // 타이머 
        BasicTime++;
        playTime++;
    }
}
