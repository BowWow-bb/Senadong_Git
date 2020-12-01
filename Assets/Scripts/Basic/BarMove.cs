using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarMove : MonoBehaviour
{ 
    GameObject hp_bar;      //hp바
    float hpbar_sx;         //hp바 스케일 x값
    float hpbar_tx;         //hp바 위치 x값
    float hpbar_tmp;        //hp바 감소 정도

    int value_max;          //속성 최댓값
    int value_pre;          //이전 값

    int delta;              //변화량

    // Start is called before the first frame update
    void Start()
    {
        value_max = 1000;
        value_pre = value_max;

        hp_bar = transform.gameObject;
        hpbar_sx = hp_bar.transform.localScale.x;
        hpbar_tx = hp_bar.transform.localPosition.x;
        hpbar_tmp = hpbar_sx / value_max;   //최대 체력에 따른 hp바 이동량 설정

    }

    // Update is called once per frame
    void Update()
    {
        value_pre = value_max;
    }
    public void hpMove(int value)    //hp바 동작 구현
    {
        delta = value - value_pre;
        if (delta < 0)
        {
            if (value_pre + delta < 0)
                delta = -value_pre;

            delta *= (-1);
            float move = ((value_max - value_pre) + delta) * hpbar_tmp; //hp바 이동할 크기
            value_pre -= delta; //hp 재설정
            Vector3 Scale = hp_bar.transform.localScale;    //현재 스케일 값
            hp_bar.transform.localScale = new Vector3(hpbar_sx - move, Scale.y, Scale.z);

            Vector3 Pos = hp_bar.transform.localPosition;   //현재 포지션 값
            hp_bar.transform.localPosition = new Vector3(hpbar_tx - move / 2.0f, Pos.y, Pos.z);
            Debug.Log(hp_bar.transform.localScale);
        }
        if (delta > 0)
        {
            if (value + delta > value_max)
                delta = (value_max - value);

            float move = ((value_max - value) + delta) * hpbar_tmp; //hp바 이동할 크기
            value += delta; //hp 재설정

            Vector3 Scale = hp_bar.transform.localScale;    //현재 스케일 값
            hp_bar.transform.localScale = new Vector3(hpbar_sx + move, Scale.y, Scale.z);

            Vector3 Pos = hp_bar.transform.localPosition;   //현재 포지션 값
            hp_bar.transform.localPosition = new Vector3(hpbar_tx + move / 2.0f, Pos.y, Pos.z);
        }
    }
}
