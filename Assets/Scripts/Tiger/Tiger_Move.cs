using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class Tiger_Move : MonoBehaviour
{
    int hungryTime =0; // 배고픔 재는 시간
    int BasicTime = 0; 
    bool hunger;
    bool moving;
    float move_length=0;
    Vector2 move_vec;
    Vector3 Start_Point;

    // Start is called before the first frame update
    public bool Hungry()
    {
        if (hunger)
        {
            return true;
        }
        else {
            if ((hungryTime / 100)%100 == 0 && (hungryTime) > 0)
            {
                hungryTime = 0;
                Debug.Log("호랑이 배고파!");
                hunger = true;
            }
            return true;
        }
    
    }
    public bool Poop()
    {
        return true;
    }
    public bool FollowMouse()
    {
        return true;
    }
    public bool Eat()
    {
        return true;
    }
    public bool BasicMove()
    {
        if (moving)
        {
            gameObject.transform.position = new Vector3(Start_Point.x+ move_vec.x * move_length,
                                                   Start_Point.y + move_vec.y * move_length, Start_Point.z);

            move_length += 0.1f;
            if (move_length > 10f)
            {
                moving = false;
                BasicTime = 0;
            }
            return true;
        }
        else
        {
            if (BasicTime % 55 == 0 && BasicTime > 0)
            {
                BasicTime = 0;
                moving = true;
                Start_Point = gameObject.transform.position;
                move_vec = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
                move_length = 0;
            }
            return true;
        }
    }
    void Start()
    {
        hunger = false;
        moving = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        hungryTime ++;
        BasicTime++;
    }
}
