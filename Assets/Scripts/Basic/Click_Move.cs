using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Click_Move : MonoBehaviour
{
    public float chicken_scale;
    public float scale;

    public bool chicken_drag = false;

    // Start is called before the first frame update
    void Start()
    {
        
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
                    chicken_drag = true;
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
    }
}
