using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TIger_State : MonoBehaviour
{
    Cow_Move cow;
    ItemManager item_manager;
    bool active = false;
    Tiger_Move tiger_move;
    public GameObject t_Play;//놀이
    // Start is called before the first frame update
    void Start()
    {
        item_manager = GameObject.Find("Main Camera").GetComponent<ItemManager>();
        tiger_move = GameObject.FindWithTag("tiger").GetComponent<Tiger_Move>();
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
                if (hit.transform.gameObject.tag == "play") //play 말풍선 클릭
                {
                    if (item_manager.play_item > 0)
                    { //아이템 있는 경우만

                        item_manager.play_item--; // 아이템 사용
                        tiger_move.playing = true; // 놀아주기 비활성화 시에 놀아주기 활성화
                        tiger_move.playTime = 0;
                        tiger_move.trace_mouse = false;
                        t_Play.active = false;
                    }
                
                }
                if (hit.transform.gameObject.tag == "tiger_poop") //호랑이 똥클릭 // 수정필요
                {
                    if (item_manager.poop_item > 0)
                    {
                        Destroy(hit.transform.gameObject);
                        Debug.Log("소똥 치움");
                        cow.countPoop--;
                    }
                }
            }
        }
    }
}
