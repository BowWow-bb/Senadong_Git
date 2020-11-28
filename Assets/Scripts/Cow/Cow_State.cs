using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cow_State : MonoBehaviour
{
    Cow_Move cow_move;
    ItemManager item_manager;

    // Start is called before the first frame update
    void Start()
    {
        cow_move = transform.GetComponent<Cow_Move>();
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
                if (hit.transform.gameObject.tag == "cow_poop") //소똥 클릭
                {
                    if (item_manager.poop_item > 0)
                    {
                        Destroy(hit.transform.gameObject);
                        Debug.Log("소똥 치움");
                        cow_move.countPoop--;
                    }
                }
                if (hit.transform.gameObject.tag == "play") //play 말풍선 클릭
                {
                    if (item_manager.play_item > 0)  //아이템 있는 경우만
                    {
                        item_manager.play_item--; // 아이템 사용
                        cow_move.playing = true; // 놀아주기 비활성화 시에 놀아주기 활성화
                        cow_move.playTime = 0;
                        cow_move.trace_mouse = false;
                        cow_move.fPlay.SetActive(false);
                    }
                }
            }
        }
    }
}
