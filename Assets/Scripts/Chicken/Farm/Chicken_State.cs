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
                if (hit.transform.gameObject.tag == "chicken_poop") //소똥 클릭
                {
                    if (item_manager.poop_item > 0)
                    {
                        Destroy(hit.transform.gameObject);
                        Debug.Log("닭똥 치움");
                        chicken_move.countPoop--;
                    }
                }
                if (hit.transform.gameObject.tag == "play") //play 말풍선 클릭
                {
                    if (item_manager.play_item > 0)  //아이템 있는 경우만
                    {
                        item_manager.play_item--; // 아이템 사용
                        chicken_move.playing = true; // 놀아주기 비활성화 시에 놀아주기 활성화
                        chicken_move.playTime = 0;
                        chicken_move.trace_mouse = false;
                        chicken_move.fPlay.SetActive(false);
                    }
                }
            }
        }
    }
}
