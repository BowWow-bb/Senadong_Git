﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TIger_State : MonoBehaviour
{
    Tiger_Move tiger_move;
    ItemManager item_manager;
    int plus = 100; //요구충족 시, 속성 증가 값
    // Start is called before the first frame update
    void Start()
    {
        tiger_move = transform.GetComponent<Tiger_Move>();
        item_manager = GameObject.Find("Main Camera").GetComponent<ItemManager>();
    }

    // Update is called once per frame
    void OnMouseDown()
    {
        if (this.transform.gameObject.tag == "tiger_poop" && this.transform.IsChildOf(transform) == true) //소똥 클릭
        {
            if (item_manager.poop_item > 0)
            {
                Destroy(this.transform.gameObject);
                tiger_move.countPoop--;
                item_manager.poop_item--;

                if (tiger_move.hungry + plus >= tiger_move.valueMax)
                {
                    tiger_move.hungry = tiger_move.valueMax;
                }
                else
                {
                    tiger_move.hungry += plus;
                }
            }
        }
        if (this.transform.gameObject == transform.GetChild(2).gameObject) //play 말풍선 클릭
        {
            if (item_manager.play_item > 0)  //아이템 있는 경우만
            {
                tiger_move.playing = true; // 놀아주기 비활성화 시에 놀아주기 활성화
                tiger_move.playTime = 0;
                tiger_move.trace_mouse = true;
                tiger_move.fPlay.SetActive(false);
                item_manager.play_item--;

                if (tiger_move.play + plus >= tiger_move.valueMax)
                {
                    tiger_move.play = tiger_move.valueMax;
                }
                else
                {
                    tiger_move.play += plus;
                }
            }
        }
    }
}
