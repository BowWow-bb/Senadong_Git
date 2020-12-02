using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChildClone : MonoBehaviour
{
    public GameObject ChickenPrefab;
    public GameObject CowPrefab;
    public GameObject TigerPrefab;

    public string tagname;  //복제될 캐릭터
    Vector3 Pos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void OnClick()
    {
        Pos = new Vector3(0, 0, -8);
        if (tagname != null)
        {
            if (tagname == "chicken")
            {
                for (int i = 0; i < 2; i++)
                {
                    GameObject clone = Instantiate(ChickenPrefab);
                    clone.transform.position = Pos;
                }
            }
            else if (tagname == "cow")
            {
                for (int i = 0; i < 2; i++)
                {
                    GameObject clone = Instantiate(CowPrefab);
                    clone.transform.position = Pos;
                }
            }
            else if (tagname == "tiger")
            {
                for (int i = 0; i < 2; i++)
                {
                    GameObject clone = Instantiate(TigerPrefab);
                    clone.transform.position = Pos;
                }
            }

            GameObject.Find("Panel").gameObject.SetActive(false);    //패널 비활성화
        }
    }
}
