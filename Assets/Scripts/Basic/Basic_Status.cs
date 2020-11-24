using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basic_Status : MonoBehaviour
{
    //0~100까지 속성 값 
    float hungryTime = 100; // 배고픔 재는 시간
    float PoopTime = 100;
    float PlayTime = 100;

    int statTime = 0;

    public float time = 5;

    bool ishunger = false;//배고픔
    bool isPoop = false;//똥
    bool isPlay = false;//심심도

    public GameObject c_d;//똥
    public GameObject c_b;//밥
    public GameObject c_p;//놀이

    public void Hungry()
    {
        if (!isPoop && ishunger && !isPlay)
        {
            statTime++;
            c_b.gameObject.SetActive(true);
            if (statTime > 100)//100동안 지속 
            {
                statTime = 0;
                hungryTime = 100;
                ishunger = false;
                c_b.gameObject.SetActive(false);
            }
        }
        else
        {
            if (hungryTime <= 30)//30보다 작으면 
            {
                hungryTime = 0;
                ishunger = true;
            }
        }
    }

    public void Poop()
    {
        if (isPoop && !ishunger && !isPlay)
        {
            statTime++;
            c_d.gameObject.SetActive(true);
            if (statTime > 100)
            {
                statTime = 0;
                PoopTime = 100;
                isPoop = false;
                c_d.gameObject.SetActive(false);
            }
        }
        else
        {
            if (PoopTime <= 20)
            {
                PoopTime = 0;
                isPoop = true;
            }
        }
    }

    public void Play()
    {
        if (!isPoop && !ishunger && isPlay)
        {
            statTime++;
            c_p.gameObject.SetActive(true);
            if (statTime > 100)
            {
                statTime = 0;
                PlayTime = 100;
                isPlay = false;
                c_p.gameObject.SetActive(false);
            }
        }
        else
        {
            if (PlayTime <= 10)
            {
                PlayTime = 0;
                isPlay = true;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        c_b.gameObject.SetActive(false);//밥 
        c_d.gameObject.SetActive(false);//똥
        c_p.gameObject.SetActive(false);//심심도
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("hungryTime: " + hungryTime);
        //Debug.Log("PoopTime: " + PoopTime);
        //Debug.Log("PlayTime: " + PlayTime);
        hungryTime -= Time.deltaTime * time;
        PoopTime -= Time.deltaTime * time;
        PlayTime -= Time.deltaTime * time;

        Hungry();
        Play();
        Poop();
    }
}
