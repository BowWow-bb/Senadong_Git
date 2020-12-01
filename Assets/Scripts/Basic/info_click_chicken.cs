using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class info_click_chicken : MonoBehaviour
{
    GameObject FloatingValue;
    Chicken_Move chicken;

    GameObject hp_bar;      //hp바
    float hpbar_sx;         //hp바 스케일 x값
    float hpbar_tx;         //hp바 위치 x값
    float hpbar_tmp;        //hp바 감소 정도
    int hungry_pre = 1000, poop_pre = 1000, play_pre = 1000; //이전 속성 값

    // Start is called before the first frame update
    void Start()
    {
        chicken = transform.GetComponent<Chicken_Move>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject == transform.gameObject) //info_icon 클릭
                {
                    FloatingValue = transform.GetChild(0).gameObject;

                    hpMove("hungry_hp", hungry_pre, hungry_pre - chicken.hungry);
                    hpMove("poop_hp", poop_pre, poop_pre - chicken.hungry);
                    hpMove("play_hp", play_pre, play_pre - chicken.hungry);

                    FloatingValue.SetActive(true);
                    StartCoroutine(Disabled(2.0f));

                }
            }
        }
    }
    IEnumerator Disabled(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        FloatingValue.SetActive(false);
    }
    public void hpMove(string tag, int value, int delta)    //hp바 동작 구현
    {
        hp_bar = GameObject.FindWithTag(tag);
        hpbar_sx = hp_bar.transform.localScale.x;
        hpbar_tx = hp_bar.transform.localPosition.x;
        hpbar_tmp = hpbar_sx / chicken.valueMax;   //최대 체력에 따른 hp바 이동량 설정

        if (delta < 0)
        {
            float move = ((chicken.valueMax - value) + delta) * hpbar_tmp; //hp바 이동할 크기
            value -= delta; //hp 재설정

            Vector3 Scale = hp_bar.transform.localScale;    //현재 스케일 값
            hp_bar.transform.localScale = new Vector3(hpbar_sx - move, Scale.y, Scale.z);

            Vector3 Pos = hp_bar.transform.localPosition;   //현재 포지션 값
            hp_bar.transform.localPosition = new Vector3(hpbar_tx - move / 2.0f, Pos.y, Pos.z);
        }
        if (delta > 0)
        {
            if (value + delta > chicken.valueMax)
                delta = (chicken.valueMax - value);

            float move = ((chicken.valueMax - value) + delta) * hpbar_tmp; //hp바 이동할 크기
            value -= delta; //hp 재설정

            Vector3 Scale = hp_bar.transform.localScale;    //현재 스케일 값
            hp_bar.transform.localScale = new Vector3(hpbar_sx + move, Scale.y, Scale.z);

            Vector3 Pos = hp_bar.transform.localPosition;   //현재 포지션 값
            hp_bar.transform.localPosition = new Vector3(hpbar_tx + move / 2.0f, Pos.y, Pos.z);
        }
    }
}
