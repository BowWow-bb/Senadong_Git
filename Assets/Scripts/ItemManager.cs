using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public GameObject Bab_Prefab;

    bool isbap=false;

    public int hungry_item = 0;
    public int poop_item = 0;
    public int play_item = 0;
    public int egg_item = 0;
    public int milk_item = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject.tag == "hungry_item")  //hungry 말풍선 클릭
                {
                    Vector3 bapPos;

                    //테스트 위함 
                    Debug.Log("밥 아이템 눌림");

                    GameObject bap = GameObject.Instantiate(Bab_Prefab);
                    bapPos = new Vector3(hit.transform.position.x, hit.transform.position.y + 3, hit.transform.position.z);
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
