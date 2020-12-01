using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Infoclick_tiger : MonoBehaviour
{
    GameObject floating, hungry, poop, play, exp;
    Tiger_Move tiger;
    
    int hungry_idx = 2, poop_idx = 4, play_idx = 6, exp_idx = 8;
    // Start is called before the first frame update
    void Start()
    {
        tiger = transform.parent.GetComponent<Tiger_Move>();
        floating = (transform.parent).transform.GetChild(4).gameObject;
    }
    private void OnMouseDown()
    {
        floating.SetActive(true);
        hungry = floating.transform.GetChild(hungry_idx).gameObject;
        poop = floating.transform.GetChild(poop_idx).gameObject;
        play = floating.transform.GetChild(play_idx).gameObject;
        exp = floating.transform.GetChild(exp_idx).gameObject;

        hpMove(hungry, tiger.hungry);
        hpMove(poop, tiger.poop);
        hpMove(play, tiger.play);
        hpMove(exp, tiger.exp);

    }
    public void hpMove(GameObject bar, int value)    //hp바 동작 구현
    {
        if (value < 0)
            value = 0;

        float hpbar_sx = bar.transform.localScale.x;
        float hpbar_tx = bar.transform.localPosition.x;
        float hpbar_tmp = hpbar_sx / 1000;   //최대 체력에 따른 hp바 이동량 설정
        int delta = 1000 - value;
        float move = delta * hpbar_tmp; //hp바 이동할 크기

        bar.transform.localScale = new Vector3(hpbar_sx - move, bar.transform.localScale.y, bar.transform.localScale.z);
        bar.transform.localPosition = new Vector3(hpbar_tx - move / 2.0f, bar.transform.localPosition.y, bar.transform.localPosition.z);

        StartCoroutine(delay(2.0f));
    }

    IEnumerator delay(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        floating.SetActive(false);
    }
}
