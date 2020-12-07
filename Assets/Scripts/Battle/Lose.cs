using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lose : MonoBehaviour
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
        item_manager.coin -= 2000;
        SceneManager.LoadScene("Farm_Scene");
    }
}
