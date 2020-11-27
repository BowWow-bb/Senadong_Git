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
            Destroy(gameObject);
        }
        if(other.gameObject.tag == "cow")
        {
            Destroy(gameObject);
        }
        if (other.gameObject.tag == "tiger")
        {
            Destroy(gameObject);
        }
    }
}
