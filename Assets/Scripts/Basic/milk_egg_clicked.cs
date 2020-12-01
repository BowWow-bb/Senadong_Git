using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class milk_egg_clicked : MonoBehaviour
{
    ItemManager item_manager;
    TextMesh egg_count;
    TextMesh milk_count;

    // Start is called before the first frame update
    void Start()
    {
        item_manager = GameObject.Find("Main Camera").GetComponent<ItemManager>();
        egg_count = GameObject.FindWithTag("egg_count").GetComponent<TextMesh>();
        milk_count = GameObject.FindWithTag("milk_count").GetComponent<TextMesh>();
    }

    void OnMouseDown()
    {
        if(this.tag == "milk")
        {
            item_manager.milk_item++;
            milk_count.text = item_manager.milk_item.ToString();
            Destroy(this.transform.gameObject);
        }
        else if (this.tag == "egg")
        {
            item_manager.egg_item++;
            egg_count.text = item_manager.egg_item.ToString();
            Destroy(this.transform.gameObject);
        }
        //똥싸기 
        else if(this.tag == "chicken_poop")
        {
            Destroy(this.transform.gameObject);
        }
        else if (this.tag == "tiger_poop")
        {
            Destroy(this.transform.gameObject);
        }
        else if (this.tag == "cow_poop")
        {
            Destroy(this.transform.gameObject);
        }
    }
}
