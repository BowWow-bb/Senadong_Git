using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cow_State : MonoBehaviour
{
    Cow_Move cow_move;
    ItemManager item_manager;
    int plus = 100; //요구충족 시, 속성 증가 값
    // Start is called before the first frame update
    void Start()
    {
        cow_move = transform.parent.GetComponent<Cow_Move>();
        item_manager = GameObject.Find("Main Camera").GetComponent<ItemManager>();
    }

    // Update is called once per frame
    void OnMouseDown()
    {
        if (this.tag == "cow_poop") //소똥 클릭
        {          
            if (this.transform.parent == transform.parent && item_manager.poop_item > 0)
            {
                this.transform.parent = null;
                Destroy(this.transform.gameObject);
                cow_move.countPoop--;
                item_manager.poop_item--;

                if (cow_move.hungry + plus >= cow_move.valueMax)
                {
                    cow_move.hungry = cow_move.valueMax;
                }
                else
                {
                    cow_move.hungry += plus;
                }
            }
        }
        else
        {//play_floating, poop 동시 사용 위해...
            if (this.transform.gameObject == (transform.parent).transform.GetChild(2).gameObject) //play 말풍선 클릭
            {
                if (item_manager.play_item > 0)  //아이템 있는 경우만
                {
                    Debug.Log("소놀풍선 클릭됨");
                    cow_move.playing = true; // 놀아주기 비활성화 시에 놀아주기 활성화
                    cow_move.playTime = 0;
                    cow_move.trace_mouse = true;
                    cow_move.fPlay.SetActive(false);
                    item_manager.play_item--;

                    if (cow_move.play + plus >= cow_move.valueMax)
                    {
                        cow_move.play = cow_move.valueMax;
                    }
                    else
                    {
                        cow_move.play += plus;
                    }
                }
            }
        }
    }
}
