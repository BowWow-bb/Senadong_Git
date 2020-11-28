using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TIger_State : MonoBehaviour
{
    Tiger_Move tiger_move;
    ItemManager item_manager;

    // Start is called before the first frame update
    void Start()
    {
        tiger_move = transform.GetComponent<Tiger_Move>();
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
                if (hit.transform.gameObject.tag == "tiger_poop") //호랑이 똥클릭 
                {
                   
                }
                if (hit.transform.gameObject == transform.GetChild(2).gameObject) //play 말풍선 클릭
                {
                    if (item_manager.play_item > 0)
                    { //아이템 있는 경우만

                        item_manager.play_item--; // 아이템 사용
                        tiger_move.playing = true; // 놀아주기 비활성화 시에 놀아주기 활성화
                        tiger_move.playTime = 0;
                        tiger_move.trace_mouse = false;
                        tiger_move.fPlay.SetActive(false);
                    }
                
                }
                
            }
        }
    }
}
