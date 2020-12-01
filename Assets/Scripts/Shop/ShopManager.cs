using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Sell_Milk()
    {
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
        ItemCounter.Variables.Egg_Num--;
    }
    public void Buy_Play()
    {
        ItemCounter.Variables.Play_Num++;
        Debug.Log(ItemCounter.Variables.Play_Num);
    }
}
