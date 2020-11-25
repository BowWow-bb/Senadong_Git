using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Drag : MonoBehaviour
{ 
    void OnMouseDrag()
    {
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 13);
        this.transform.position = Camera.main.ScreenToWorldPoint(mousePosition);
        if(this.gameObject.GetComponent<Chicken_Move>()!= null)
        {
            this.gameObject.GetComponent<Chicken_Move>().isdrag = true;
        }    
    }
}
