using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lose : MonoBehaviour
{
    ItemManager item_manager;

    Cow_AI cow_ai;
    // Start is called before the first frame update
    void Start()
    {
        item_manager = GameObject.Find("ItemManager").GetComponent<ItemManager>();
        cow_ai = GameObject.Find("Cow_p").transform.GetChild(0).gameObject.GetComponent<Cow_AI>();
    }

    // Update is called once per frame
    public void OnClick()
    {
        //진 경우 -> 코인 소실
        item_manager.coin -= 2000;

        //농장 캐릭터 비활성화 해제
        GameObject.Find("Chicken_p").transform.GetChild(0).gameObject.SetActive(true);
        GameObject.Find("Cow_p").transform.GetChild(0).gameObject.SetActive(true);
        GameObject.Find("Tiger_p").transform.GetChild(0).gameObject.SetActive(true);

        //농장씬 다시 로드
        SceneManager.LoadScene("Farm_Scene");
    }
}
