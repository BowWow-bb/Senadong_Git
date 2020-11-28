using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickObject : MonoBehaviour
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

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject.tag == "egg") //달걀 클릭
                {
                    Debug.Log("계란 획득");
                    item_manager.egg_item++;
                    egg_count.text = item_manager.egg_item.ToString();
                    Destroy(hit.transform.gameObject);
                }
                if (hit.transform.gameObject.tag == "milk") //우유 클릭
                {
                    Debug.Log("우유 획득");
                    item_manager.milk_item++;
                    milk_count.text = item_manager.milk_item.ToString();
                    Destroy(hit.transform.gameObject);
                }
            }
        }
    }
}
