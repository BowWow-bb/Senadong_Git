using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chicken_wind : MonoBehaviour
{
    Chicken_Attack chicken;
    Vector3 pos;

    public float speed =3;
    // Start is called before the first frame update
    void Start()
    {
        chicken = GameObject.FindWithTag("chicken").GetComponent<Chicken_Attack>();
        if(chicken.is_go_right)//적이 왼쪽이라면 
        {
            pos = Vector3.left;
        }
        else
        {
            pos = Vector3.right;
        }
        Destroy(gameObject, 5);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += pos * Time.deltaTime * speed;
    }
}
