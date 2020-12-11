using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetCoin : MonoBehaviour
{
    Chicken_Move chicken;
    Cow_Move cow;
    Tiger_Move tiger;

    ItemManager item_manager;

    public string tagname;  //복제될 캐릭터
    Vector3 Pos;
    // Start is called before the first frame update
    void Start()
    {
        item_manager = GameObject.Find("ItemManager").GetComponent<ItemManager>();
    }

    // Update is called once per frame
    public void OnClick()
    {
        Pos = new Vector3(0, 0, -8);
        if (tagname != null)
        {
            //성장완료 후, 새로운 캐릭터로 생성 (이전 캐릭터 보다 레벨이 높아짐)
            if (tagname == "chicken")
            {
                chicken = GameObject.Find("Chicken_p").transform.GetChild(0).GetComponent<Chicken_Move>();
                chicken.gameObject.SetActive(true);
                chicken.hungry = chicken.valueMax; chicken.poop = chicken.valueMax;   chicken.play = chicken.valueMax;
                chicken.exp = 0; chicken.Timer = 0;
                item_manager.chicken_level += 2;
            }
            else if (tagname == "cow")
            {
                cow = GameObject.Find("Cow_p").transform.GetChild(0).GetComponent<Cow_Move>();
                cow.gameObject.SetActive(true);
                cow.hungry = cow.valueMax; cow.poop = cow.valueMax; cow.play = cow.valueMax;
                cow.exp = 0; cow.Timer = 0;
                item_manager.cow_level += 2;
            }
            else if (tagname == "tiger")
            {
                tiger = GameObject.Find("Tiger_p").transform.GetChild(0).GetComponent<Tiger_Move>();
                tiger.gameObject.SetActive(true);
                tiger.hungry = tiger.valueMax; tiger.poop = tiger.valueMax; tiger.play = tiger.valueMax;
                tiger.exp = 0;  tiger.Timer = 0;
                item_manager.tiger_level += 2;
            }

            //코인 지급
            item_manager.coin += 2000;
            GameObject.Find("Panel").gameObject.SetActive(false);    //패널 비활성화
        }
    }
}
