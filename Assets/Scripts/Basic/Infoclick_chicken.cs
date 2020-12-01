﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Infoclick_chicken : MonoBehaviour
{
    public GameObject FloatingPrefab;
    GameObject floating;
    Chicken_Move chicken;
    Vector3 pos;
    BarMove hungry, poop, play;

    int hungry_idx = 2, poop_idx = 4, play_idx = 6;
    // Start is called before the first frame update
    void Start()
    {
        chicken = transform.parent.GetComponent<Chicken_Move>();
    }

    // Update is called once per frame
    private void OnMouseDown()
    {
        floating = GameObject.Instantiate(FloatingPrefab);
        pos = chicken.transform.position;
        floating.transform.position = new Vector3(pos.x + 1, pos.y, pos.z);

        hungry = (floating.transform.GetChild(hungry_idx).gameObject).GetComponent<BarMove>();
        poop = (floating.transform.GetChild(poop_idx).gameObject).GetComponent<BarMove>();
        play = (floating.transform.GetChild(play_idx).gameObject).GetComponent<BarMove>();

        hungry.hpMove(chicken.hungry);
        poop.hpMove(chicken.poop);
        play.hpMove(chicken.play);

        Destroy(floating, 2.0f);
    }
}