using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poop_Fix : MonoBehaviour
{
    Vector3 pos;
    // Start is called before the first frame update
    void Start()
    {
        pos = transform.position;
        transform.position = pos;
    }

    // Update is called once per frame
    void Update()
    {
        //부모에 따라 움직이지 않게 똥 고정
        transform.position = pos;
    }
}
