using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public GameObject Bab_Prefab;
    public GameObject Egg_Prefab;
    public GameObject Milk_Prefab;

    bool isbap=false;

    public int hungry_item = 0;
    public int poop_item = 0;
    public int play_item = 0;
    public int egg_item = 0;
    public int milk_item = 0;
    public TextMesh count;

    public int coin = 0;    

    //공격 레벨
    public int chicken_level;
    public int cow_level;
    public int tiger_level;

    public bool chicken_die = false, chicken_grow = false;
    public bool cow_die = false, cow_grow = false;
    public bool tiger_die = false, tiger_grow = false;

    public int cc_timer, cc_hungry, cc_poop, cc_play, cc_exp, cc_i = 0;
    public int c_timer, c_hungry, c_poop, c_play, c_exp, c_i = 0;
    public int t_timer, t_hungry, t_poop, t_play, t_exp, t_i = 0;
    // Start is called before the first frame update
    void Start()
    {
        hungry_item = 2;
        poop_item = 2;
        play_item = 2;

        coin = 1000;

        chicken_level = 0;
        cow_level = 0;
        tiger_level = 0;

        chicken_die = false;
        cow_die = false;
        tiger_die = false;
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

        count = GameObject.FindWithTag("coin").GetComponent<TextMesh>();
        count.text = coin.ToString();

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject.tag == "hungry_item_slot" && hungry_item > 0)  //hungry 아이템 슬롯 누름 
                {
                    Vector3 bapPos;
                    GameObject bap = GameObject.Instantiate(Bab_Prefab);
                    bap.transform.parent = GameObject.Find("ItemManager").GetComponent<ItemManager>().transform;
                    bapPos = new Vector3(hit.transform.position.x, hit.transform.position.y + 1, hit.transform.position.z);
                    bap.transform.position = bapPos;
                }
                if (hit.transform.gameObject.tag == "egg_item_slot" && egg_item > 0)  //hungry 아이템 슬롯 누름 
                {
                    Vector3 eggPos;
                    GameObject egg = GameObject.Instantiate(Egg_Prefab);
                    egg.transform.parent = GameObject.Find("ItemManager").GetComponent<ItemManager>().transform;
                    eggPos = new Vector3(hit.transform.position.x, hit.transform.position.y + 1, hit.transform.position.z);
                    egg.transform.position = eggPos;
                }
                if (hit.transform.gameObject.tag == "milk_item_slot" && milk_item > 0)  //hungry 아이템 슬롯 누름 
                {
                    Vector3 milkPos;
                    GameObject milk = GameObject.Instantiate(Milk_Prefab);
                    milk.transform.parent = GameObject.Find("ItemManager").GetComponent<ItemManager>().transform;
                    milkPos = new Vector3(hit.transform.position.x, hit.transform.position.y + 1, hit.transform.position.z);
                    milk.transform.position = milkPos;
                }
            }
        }
    }
}
