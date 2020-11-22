using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chicken_Move : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //테스트용 
        Vector3 moveVelocity = Vector3.left;
        transform.position += moveVelocity * Time.deltaTime*0.5f;
    }
}
