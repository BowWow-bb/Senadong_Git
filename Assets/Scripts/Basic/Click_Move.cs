﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Click_Move : MonoBehaviour
{
    GameObject chicken;

    public float chicken_scale;
    public float scale;

    // Start is called before the first frame update
    void Start()
    {
        chicken = GameObject.FindWithTag("chicken");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out hit);

            if (hit.collider != null)//hit한 오브젝트 있다면 오브젝트 확인 
            {
                if (hit.transform.gameObject.tag == "chicken")
                {
                    chicken.GetComponent<Chicken_Move>().isdrag = true;
                    if (hit.transform.localScale.x < 0)//오른쪽 
                    {
                        hit.transform.localScale = new Vector3(-chicken_scale, chicken_scale, chicken_scale);
                    }
                    else
                    {
                        hit.transform.localScale = new Vector3(chicken_scale, chicken_scale, chicken_scale);
                    }
                }

                //다른 오브젝트 스케일 변화 방지 
                if (hit.transform.gameObject.tag == "cow")
                {
                    if (hit.transform.localScale.x < 0)//오른쪽 
                    {
                        hit.transform.localScale = new Vector3(-scale, scale, scale);
                    }
                    else
                    {
                        hit.transform.localScale = new Vector3(scale, scale, scale);
                    }
                }

                if (hit.transform.gameObject.tag == "tiger")
                {
                    if (hit.transform.localScale.x < 0)//오른쪽 
                    {
                        hit.transform.localScale = new Vector3(-scale, scale, scale);
                    }
                    else
                    {
                        hit.transform.localScale = new Vector3(scale, scale, scale);
                    }
                }

                if (hit.transform.gameObject.tag == "hungry_follow_item")
                {
                    hit.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
                }
            }
        }
        else
        {
            if(chicken != null)
            {
                chicken.GetComponent<Chicken_Move>().isdrag = false;
            }
        }
    }
}
