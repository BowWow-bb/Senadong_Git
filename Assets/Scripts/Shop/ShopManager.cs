using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    ItemManager item_manager;
    public TextMesh String;
    // Start is called before the first frame update
    void Start()
    {
        item_manager = GameObject.Find("ItemManager").GetComponent<ItemManager>();
    }

    // Update is called once per frame
    void Update()
    {
        String = GameObject.FindWithTag("hungry_string").GetComponent<TextMesh>();
        String.text = item_manager.hungry_item.ToString() + "개";

        String = GameObject.FindWithTag("poop_string").GetComponent<TextMesh>();
        String.text = item_manager.poop_item.ToString() + "개";

        String = GameObject.FindWithTag("play_string").GetComponent<TextMesh>();
        String.text = item_manager.play_item.ToString() + "개";

        String = GameObject.FindWithTag("egg_string").GetComponent<TextMesh>();
        String.text = item_manager.egg_item.ToString() + "개";

        String = GameObject.FindWithTag("milk_string").GetComponent<TextMesh>();
        String.text = item_manager.milk_item.ToString() + "개";

        String = GameObject.FindWithTag("coin").GetComponent<TextMesh>();
        String.text = item_manager.coin.ToString();

    }
    public void Buy_Hungry()
    {
        if(item_manager.coin>100)
        {
            item_manager.coin -= 100;
            item_manager.hungry_item++;
        }
    }
    public void Buy_Poop()
    {
        if (item_manager.coin > 100)
        {
            item_manager.coin -= 100;
            item_manager.poop_item++;
        }
    }
    public void Buy_Play()
    {
        if (item_manager.coin > 100)
        {
            item_manager.coin -= 100;
            item_manager.play_item++;
        }
    }
    public void Sell_egg()
    {
        if (item_manager.egg_item > 0)
        {
            item_manager.egg_item--;
            item_manager.coin += 100;
        }
            
    }
    public void Sell_Milk()
    {
        if (item_manager.milk_item > 0)
        {
            item_manager.milk_item--;
            item_manager.coin += 100;
        }
    }
}
