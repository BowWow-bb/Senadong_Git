using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Drag : MonoBehaviour
{
    Chicken_Move c_m;
    Tiger_Move t_m;
    Cow_Move co_m;

    void OnMouseDrag()
    {
        if(this.tag == "tiger")
        {
            t_m = transform.GetComponent<Tiger_Move>();
            t_m.isdrag = true;
            t_m.moving = false;
        }
        else if (this.tag == "chicken")
        {
            c_m = transform.GetComponent<Chicken_Move>();
            c_m.is_drag = true;
            c_m.moving = false;

        }
        else if(this.tag == "cow")
        {
            co_m = transform.GetComponent<Cow_Move>();
            co_m.isdrag = true;
            co_m.moving = false;
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
            
        }
        else if (this.tag == "chicken")
        {
            c_m = transform.GetComponent<Chicken_Move>();
            c_m.is_drag = false;

        }
        else if (this.tag == "cow")
        {
            co_m = transform.GetComponent<Cow_Move>();
            co_m.isdrag = false;
        }
    }
}
