using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public GameObject Bab_Prefab;

    bool isbap=false;

    public int hungry_item = 3;
    public int poop_item = 0;
    public int play_item = 0;
    public int egg_item = 0;
    public int milk_item = 0;
    public TextMesh count;
    // Start is called before the first frame update
    void Start()
    {
        play_item = 10; // 잠시바꿈

    }

    // Update is called once per frame
    void Update()
    {
        
        count = GameObject.FindWithTag("hungry_count").GetComponent<TextMesh>();
        count.text = hungry_item.ToString();
        count = GameObject.FindWithTag("play_count").GetComponent<TextMesh>();
        count.text = play_item.ToString();
        count = GameObject.FindWithTag("egg_count").GetComponent<TextMesh>();
        count.text = egg_item.ToString();
        count = GameObject.FindWithTag("milk_count").GetComponent<TextMesh>();
        count.text = milk_item.ToString();
        count = GameObject.FindWithTag("poop_count").GetComponent<TextMesh>();
        count.text = poop_item.ToString();
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject.tag == "hungry_item_slot")  //hungry 아이템 슬롯 누름 
                {
                    Vector3 bapPos;

                    //테스트 위함 
                    GameObject bap = GameObject.Instantiate(Bab_Prefab);
                    
                    bapPos = new Vector3(hit.transform.position.x, hit.transform.position.y + 1, hit.transform.position.z);
                    bap.transform.position = bapPos;

                    if (hungry_item != 0 && isbap)
                    {
                        hungry_item--;
                    }
                }
            }
        }
    }
}
