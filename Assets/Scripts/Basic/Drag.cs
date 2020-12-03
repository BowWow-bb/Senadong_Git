using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Drag : MonoBehaviour
{
    Chicken_Move c_m;
    Tiger_Move t_m;
    Cow_Move co_m;

    public float scale=1.1f;
    void OnMouseDrag()
    {
        if(this.tag == "tiger")
        {
            t_m = transform.GetComponent<Tiger_Move>();
            t_m.isdrag = true;
            t_m.moving = false;

            if (t_m.transform.localScale.x < 0)//오른쪽 
            {
                t_m.transform.localScale = new Vector3(-scale, scale, scale);
            }
            else
            {
                t_m.transform.localScale = new Vector3(scale, scale, scale);
            }
        }
        else if (this.tag == "chicken")
        {
            c_m = transform.GetComponent<Chicken_Move>();
            c_m.isdrag = true;
            c_m.moving = false;
            if (c_m.transform.localScale.x < 0)//오른쪽 
            {
                c_m.transform.localScale = new Vector3(-scale, scale, scale);
            }
            else
            {
                c_m.transform.localScale = new Vector3(scale, scale, scale);
            }

        }
        else if(this.tag == "cow")
        {
            co_m = transform.GetComponent<Cow_Move>();
            co_m.isdrag = true;
            co_m.moving = false;
            if (co_m.transform.localScale.x < 0)//오른쪽 
            {
                co_m.transform.localScale = new Vector3(-scale, scale, scale);
            }
            else
            {
                co_m.transform.localScale = new Vector3(scale, scale, scale);
            }
        }
        else if(this.tag == "hungry_follow_item")   //농장에 가져다 놓은 밥 이동
        {
            this.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
        }
        else if (this.tag == "milk_item_follow")   //농장에 가져다 우유 이동 
        {
            this.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
        }
        else if (this.tag == "egg_item_follow")   //농장에 가져다 계란 이동 
        {
            this.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
        }
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 13);
        this.transform.position = Camera.main.ScreenToWorldPoint(mousePosition);
    }
   void OnMouseUp()
    {
        if (this.tag == "tiger")
        {
            t_m = transform.GetComponent<Tiger_Move>();
            t_m.isdrag = false;
            if (t_m.transform.localScale.x < 0)//오른쪽 
            {
                t_m.transform.localScale = new Vector3(-1, 1, 1);
            }
            else
            {
                t_m.transform.localScale = new Vector3(1, 1, 1);
            }
        }
        else if (this.tag == "chicken")
        {
            c_m = transform.GetComponent<Chicken_Move>();
            c_m.isdrag = false;
            if (c_m.transform.localScale.x < 0)//오른쪽 
            {
                c_m.transform.localScale = new Vector3(-1, 1, 1);
            }
            else
            {
                c_m.transform.localScale = new Vector3(1, 1, 1);
            }
        }
        else if (this.tag == "cow")
        {
            co_m = transform.GetComponent<Cow_Move>();
            co_m.isdrag = false;
            if (co_m.transform.localScale.x < 0)//오른쪽 
            {
                co_m.transform.localScale = new Vector3(-1, 1, 1);
            }
            else
            {
                co_m.transform.localScale = new Vector3(1, 1, 1);
            }
        }
    }
}
