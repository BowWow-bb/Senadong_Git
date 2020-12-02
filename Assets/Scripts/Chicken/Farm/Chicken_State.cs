using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chicken_State : MonoBehaviour
{
    Chicken_Move chicken_move;
    ItemManager item_manager;
    int plus = 100; //요구충족 시, 속성 증가 값
    // Start is called before the first frame update
    void Start()
    {
        chicken_move = transform.parent.GetComponent<Chicken_Move>();
        item_manager = GameObject.Find("Main Camera").GetComponent<ItemManager>();
    }

    // Update is called once per frame
    void OnMouseDown()
    {
        if (this.tag == "chicken_poop")
        {
            if (this.transform.parent == transform.parent && item_manager.poop_item > 0)
            {
                this.transform.parent = null;
                Destroy(this.transform.gameObject);
                chicken_move.countPoop--;
                item_manager.poop_item--;

                if (chicken_move.hungry + plus >= chicken_move.valueMax)
                {
                    chicken_move.hungry = chicken_move.valueMax;
                }
                else
                {
                    chicken_move.hungry += plus;
                }
            }
        }
        else
        {//play_floating, poop 동시 사용 위해...
            if (this.transform.gameObject == (transform.parent).GetChild(2).gameObject) //play 말풍선 클릭
            {
                if (item_manager.play_item > 0)  //아이템 있는 경우만
                {
                    Debug.Log("소놀풍선 클릭됨");
                    chicken_move.playing = true; // 놀아주기 비활성화 시에 놀아주기 활성화
                    chicken_move.playTime = 0;
                    chicken_move.trace_mouse = true;
                    chicken_move.fPlay.SetActive(false);
                    item_manager.play_item--;

                    if (chicken_move.play + plus >= chicken_move.valueMax)
                    {
                        chicken_move.play = chicken_move.valueMax;
                    }
                    else
                    {
                        chicken_move.play += plus;
                    }
                }
            }
        }
    }
}
