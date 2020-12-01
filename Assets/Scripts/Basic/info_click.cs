using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class info_click : MonoBehaviour
{
    GameObject FloatingValue;

    // Start is called before the first frame update
    void Start()
    {
        
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
                if (hit.transform.gameObject == transform.gameObject) //info_icon 클릭
                {
                    FloatingValue = transform.GetChild(0).gameObject;
                    FloatingValue.SetActive(true);
                    StartCoroutine(Disabled(2.0f));
                    
                } 
            }
        }
    }
    IEnumerator Disabled(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        FloatingValue.SetActive(false);
    }
}
