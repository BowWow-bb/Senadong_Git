using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hungry_item_script : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "chicken")
        {
            Debug.Log("치킨이 먹음");
            Destroy(gameObject);
        }
        if(other.gameObject.tag == "cow")
        {
            Debug.Log("소가먹음");
            Destroy(gameObject);
        } 
        if (other.gameObject.tag == "tiger")
        {
            Debug.Log("호랑이가 먹음");
            Destroy(gameObject);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "chicken")
        {
            Debug.Log("치킨이 먹음");
            Destroy(gameObject);
        }
        if (other.gameObject.tag == "cow")
        {
            Debug.Log("소가먹음");
            Destroy(gameObject);
        }
        if (other.gameObject.tag == "tiger")
        {
            Debug.Log("호랑이가 먹음");
            Destroy(gameObject);
        }
    }
}
