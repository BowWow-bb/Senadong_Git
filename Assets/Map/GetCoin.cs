using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetCoin : MonoBehaviour
{
    public GameObject ChickenPrefab;
    public GameObject CowPrefab;
    public GameObject TigerPrefab;

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
                GameObject clone = Instantiate(ChickenPrefab);
                clone.transform.parent = GameObject.Find("Chicken_p").transform;
                clone.transform.position = Pos;
                item_manager.chicken_level += 2;
            }
            else if (tagname == "cow")
            {
                Debug.Log("태그확인!");
                GameObject clone = Instantiate(CowPrefab);
                clone.transform.parent = GameObject.Find("Cow_p").transform;
                clone.transform.position = Pos;
                item_manager.cow_level += 2;
            }
            else if (tagname == "tiger")
            {
                GameObject clone = Instantiate(TigerPrefab);
                clone.transform.parent = GameObject.Find("Tiger_p").transform;
                clone.transform.position = Pos;
                item_manager.tiger_level += 2;
            }

            //코인 지급
            item_manager.coin += 2000;
            GameObject.Find("Panel").gameObject.SetActive(false);    //패널 비활성화
        }
    }
}
