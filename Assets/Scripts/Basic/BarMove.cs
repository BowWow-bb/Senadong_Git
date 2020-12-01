using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarMove : MonoBehaviour
{ 
    GameObject hp_bar;      //hp바
    float hpbar_sx;         //hp바 스케일 x값
    float hpbar_tx;         //hp바 위치 x값
    float hpbar_tmp;        //hp바 감소 정도
    Transform parent;
    Vector3 Scale, Pos;

    int value_max;          //속성 최댓값
    int delta;              //변화량

    // Start is called before the first frame update
    void Start()
    {
        value_max = 1000;

        hp_bar = transform.gameObject;
        hpbar_sx = hp_bar.transform.localScale.x;
        hpbar_tx = hp_bar.transform.localPosition.x;
        hpbar_tmp = hpbar_sx / value_max;   //최대 체력에 따른 hp바 이동량 설정

        Scale = transform.localScale;
        Pos = transform.localPosition;
        parent = transform.parent;
    }

    // Update is called once per frame
    public void hpMove(int value)    //hp바 동작 구현
    {
        delta = value_max - value;
        float move = delta * hpbar_tmp; //hp바 이동할 크기
        
        transform.parent = null;
        transform.localScale = new Vector3(hpbar_sx - move, Scale.y, Scale.z);
        transform.localPosition = new Vector3(hpbar_tx - move / 2.0f, Pos.y, Pos.z);
        transform.parent = parent;
    }
}
