using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Win : MonoBehaviour
{
    ItemManager item_manager;
    // Start is called before the first frame update
    void Start()
    {
        item_manager = GameObject.Find("ItemManager").GetComponent<ItemManager>();
    }

    // Update is called once per frame
    public void OnClick()
    {
        //이긴 경우 -> 코인 소실
        item_manager.coin += 3000;

        //농장 캐릭터 비활성화 해제
        GameObject.Find("Chicken_p").transform.GetChild(0).gameObject.SetActive(true);
        GameObject.Find("Cow_p").transform.GetChild(0).gameObject.SetActive(true);
        GameObject.Find("Tiger_p").transform.GetChild(0).gameObject.SetActive(true);

        //농장씬 다시 로드
        SceneManager.LoadScene("Farm_Scene");
    }
}
