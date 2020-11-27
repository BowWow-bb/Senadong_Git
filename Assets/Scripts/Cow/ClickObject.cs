using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickObject : MonoBehaviour
{
    Cow_Move cow;
    ItemManager item_manager;
    TextMesh egg_count;
    TextMesh milk_count;

    // Start is called before the first frame update
    void Start()
    {
        cow = GameObject.Find("Cow").GetComponent<Cow_Move>();
        item_manager = GameObject.Find("Main Camera").GetComponent<ItemManager>();
        egg_count = GameObject.FindWithTag("egg_count").GetComponent<TextMesh>();
        milk_count = GameObject.FindWithTag("milk_count").GetComponent<TextMesh>();
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
                    if(item_manager.play_item > 0)  //아이템 있는 경우만
                    {
                        item_manager.play_item--;   //아이템 사용
                    }
                }
                if (hit.transform.gameObject.tag == "cow_poop") //소똥 클릭
                {
                    if (item_manager.poop_item > 0)
                    {
                        Destroy(hit.transform.gameObject);
                        Debug.Log("소똥 치움");
                        cow.countPoop--;
                    }
                }
                if (hit.transform.gameObject.tag == "egg") //우유 클릭
                {
                    Debug.Log("계란 획득");
                    item_manager.egg_item++;
                    egg_count.text = item_manager.egg_item.ToString();
                    Destroy(hit.transform.gameObject);
                }
                if (hit.transform.gameObject.tag == "milk") //우유 클릭
                {
                    Debug.Log("우유 획득");
                    item_manager.milk_item++;
                    milk_count.text = item_manager.milk_item.ToString();
                    Destroy(hit.transform.gameObject);
                }
                
            }
        }
    }
}
