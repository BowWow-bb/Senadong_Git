using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_AttackData : MonoBehaviour
{
    //레벨
    public int Echicken_level = 0;
    public int Ecow_level = 0;
    public int Etiger_level = 0;

    //공격력
    public int Echicken_attack = 0;
    public int Ecow_attack = 0;
    public int Etiger_attack = 0;

    int timer = 0;
    int threshold = 500;
    int rand_tmp = 0;

    public bool isBattle = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!isBattle)    //전투 중이 아닌 경우!
        {
            //Debug.Log("적군어택타이머: " + timer);
            timer++;
            if (timer > threshold) //임계점 도달
            {
                threshold += 1200;   //임계점 업뎃

                //적군 레벨 업
                Echicken_level++;
                Ecow_level++;
                Etiger_level++;

                rand_tmp = Random.Range(5, 11); //5~10 사이 랜덤값
                Echicken_attack = Echicken_level * rand_tmp;    //랜덤 배수값

                rand_tmp = Random.Range(5, 11); //5~10 사이 랜덤값
                Ecow_attack = Ecow_level * rand_tmp;    //랜덤 배수값

                rand_tmp = Random.Range(5, 11); //5~10 사이 랜덤값
                Etiger_attack = Etiger_level * rand_tmp;    //랜덤 배수값
            }
        }
    }
    //게임 시작과 동시에 타이머는 계속 증가
    //타이머 값의 임계점에 따라 적군의 공격력은 증가...
}
