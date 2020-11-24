using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickObject : MonoBehaviour
{
    Cow_Move bring_cow; 
    ItemManager item_manager;
    Text egg_count;
    Text milk_count;

    // Start is called before the first frame update
    void Start()
    {
        bring_cow = GameObject.Find("Cow").GetComponent<Cow_Move>();
        item_manager = GameObject.Find("Main Camera").GetComponent<ItemManager>();
        egg_count = GameObject.FindWithTag("egg_count").GetComponent<Text>();
        milk_count = GameObject.FindWithTag("milk_count").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log(hit.transform.gameObject + "클릭됨");

                if (hit.transform.gameObject.tag == "hungry")  //hungry 말풍선 클릭
                {
                    hit.transform.gameObject.SetActive(false);
                    //if()//알맞은 아이템 있는 경우
                    //{
                    //    hit.transform.gameObject.SetActive(false); 
                    //    bring_cow.hungry += 10; //속성값 증가
                    //}

                }
                if (hit.transform.gameObject.tag == "poop") //poop 말풍선 클릭
                {

                }
                if (hit.transform.gameObject.tag == "play") //play 말풍선 클릭
                {

                }
                if (hit.transform.gameObject.tag == "milk") //우유 클릭
                {
                    Debug.Log("우유 획득");
                    item_manager.milk_item++;
                    milk_count.text = item_manager.milk_item.ToString();
                    Destroy(hit.transform.gameObject);
                    //우유 아이템 획득 표시
                }
            }
        }
    }

}
