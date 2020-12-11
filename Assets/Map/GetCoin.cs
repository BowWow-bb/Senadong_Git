using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetCoin : MonoBehaviour
{
    Chicken_Move chicken;
    Cow_Move cow;
    Tiger_Move tiger;

    public GameObject cc;
    public GameObject c;
    public GameObject t;

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
                GameObject chicken = Instantiate(cc);
                item_manager.chicken_level += 2;
            }
            else if (tagname == "cow")
            {
                GameObject cow = Instantiate(c);
                item_manager.cow_level += 2;
            }
            else if (tagname == "tiger")
            {
                GameObject cow = Instantiate(t);
                item_manager.tiger_level += 2;
            }

            //코인 지급
            item_manager.coin += 2000;
            GameObject.Find("Panel").gameObject.SetActive(false);    //패널 비활성화
        }
    }
}
