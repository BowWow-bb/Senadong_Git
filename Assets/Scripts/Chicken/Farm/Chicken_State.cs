using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chicken_State : MonoBehaviour
{
    Chicken_Move chicken_move;
    ItemManager item_manager;

    // Start is called before the first frame update
    void Start()
    {
        chicken_move = transform.GetComponent<Chicken_Move>();
        item_manager = GameObject.Find("Main Camera").GetComponent<ItemManager>();
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
                if (hit.transform.gameObject.tag == "chicken_poop") //똥 클릭
                {
                   
                }
                if (hit.transform.gameObject == transform.GetChild(2).gameObject) //play 말풍선 클릭
                {
                    //확인용 코드 (아이템 없어도 실행되게...)
                    Debug.Log("플레이풍선 클릭");
                    chicken_move.playing = true; // 놀아주기 비활성화 시에 놀아주기 활성화
                    chicken_move.playTime = 0;
                    chicken_move.trace_mouse = true;
                    chicken_move.fPlay.SetActive(false);

                    //if (item_manager.play_item > 0)  //아이템 있는 경우만
                    //{
                    //    chicken_move.playing = true; // 놀아주기 비활성화 시에 놀아주기 활성화
                    //    cow_chicken_movemove.playTime = 0;
                    //    chicken_move.trace_mouse = true;
                    //    chicken_move.fPlay.SetActive(false);
                    //}
                }
            }
        }
    }
}
