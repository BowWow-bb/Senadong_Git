using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Infoclick_cow : MonoBehaviour
{
    GameObject floating, hungry, poop, play, exp;
    Cow_Move cow;

    float hpbar_tx, hpbar_sx, hpbar_tmp;
    int hungry_idx = 2, poop_idx = 4, play_idx = 6, exp_idx = 8;
    // Start is called before the first frame update
    void Start()
    {
        cow = transform.parent.GetComponent<Cow_Move>();
        floating = (transform.parent).transform.GetChild(4).gameObject;

        hpbar_tx = floating.transform.GetChild(2).localPosition.x;
        hpbar_sx = floating.transform.GetChild(2).localScale.x;
        hpbar_tmp = hpbar_sx / 1000;   //최대 체력에 따른 hp바 이동량 설정
    }
    // Update is called once per frame
    private void OnMouseDown()
    {
        floating.SetActive(true);
        hungry = floating.transform.GetChild(hungry_idx).gameObject;
        poop = floating.transform.GetChild(poop_idx).gameObject;
        play = floating.transform.GetChild(play_idx).gameObject;
        exp = floating.transform.GetChild(exp_idx).gameObject;

        hpMove(hungry, cow.hungry);
        hpMove(poop, cow.poop);
        hpMove(play, cow.play);
        hpMove(exp, cow.exp);
        StartCoroutine(delay(2.0f));
    }
    public void hpMove(GameObject bar, int value)    //hp바 동작 구현
    {
        if (value < 0)
            value = 0;

        int delta = 1000 - value;
        float move = delta * hpbar_tmp; //hp바 이동할 크기

        bar.transform.localScale = new Vector3(hpbar_sx - move, bar.transform.localScale.y, bar.transform.localScale.z);
        bar.transform.localPosition = new Vector3(hpbar_tx - move / 2.0f, bar.transform.localPosition.y, bar.transform.localPosition.z);
    }

    IEnumerator delay(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        floating.SetActive(false);
    }
}
