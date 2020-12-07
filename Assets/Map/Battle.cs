using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Battle : MonoBehaviour
{
    GameObject cow;
    GameObject tiger;
    GameObject chicken;

    ItemManager item_manager;
    // Start is called before the first frame update
    void Start()
    {
        cow = GameObject.FindWithTag("cow");
        tiger = GameObject.FindWithTag("tiger");
        chicken = GameObject.FindWithTag("chicken");

        item_manager = GameObject.Find("ItemManager").GetComponent<ItemManager>();
    }

    // Update is called once per frame
    public void OnClick()
    {
        if(item_manager.coin > 2000)
        {
            cow.SetActive(false);
            tiger.SetActive(false);
            chicken.SetActive(false);
            SceneManager.LoadScene("Battle_Scene");
        }

        //errr msg floating (2,000 코인 이상 전장진출 가능) 
        GameObject.Find("Canvas").transform.GetChild(3).gameObject.SetActive(true);
        StartCoroutine(delay(2.0f));
    }

    IEnumerator delay(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        GameObject.Find("Canvas").transform.GetChild(3).gameObject.SetActive(false);
    }
}
