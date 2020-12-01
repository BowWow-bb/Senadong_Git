using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public TextMesh String;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        String = GameObject.FindWithTag("hungry_string").GetComponent<TextMesh>();
        String.text = ItemCounter.Variables.Hungry_Num.ToString() +"개";
 
        String = GameObject.FindWithTag("play_string").GetComponent<TextMesh>();
        String.text = ItemCounter.Variables.Play_Num.ToString() + "개";

        String = GameObject.FindWithTag("egg_string").GetComponent<TextMesh>();
        String.text = ItemCounter.Variables.Egg_Num.ToString() + "개";

        String = GameObject.FindWithTag("milk_string").GetComponent<TextMesh>();
        String.text = ItemCounter.Variables.Milk_Num.ToString() + "개";

        String = GameObject.FindWithTag("poop_string").GetComponent<TextMesh>();
        String.text = ItemCounter.Variables.Poop_Num.ToString() + "개";

    }
    public void Sell_Milk()
    {
        if(ItemCounter.Variables.Milk_Num>0)
            ItemCounter.Variables.Milk_Num--;
    }
    public void Buy_Hungry()
    {
        ItemCounter.Variables.Hungry_Num++;
       
    }
    public void Buy_Poop()
    {
        ItemCounter.Variables.Poop_Num++;
    }
    public void Sell_egg()
    {
        if (ItemCounter.Variables.Egg_Num>0)
            ItemCounter.Variables.Egg_Num--;
    }
    public void Buy_Play()
    {
        ItemCounter.Variables.Play_Num++;
    }
}
