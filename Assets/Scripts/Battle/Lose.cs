﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lose : MonoBehaviour
{
    ItemManager item_manager;
    public GameObject cow;
    public GameObject tiger;
    public GameObject chicken;

    E_AttackData e_attack_data;
    // Start is called before the first frame update
    void Start()
    {
        item_manager = GameObject.Find("ItemManager").GetComponent<ItemManager>();
        e_attack_data = GameObject.Find("E_AttackData").GetComponent<E_AttackData>();
    }

    // Update is called once per frame
    public void OnClick()
    {
        //진 경우 -> 코인 소실
        item_manager.coin -= 2000;

        item_manager.cow_die = false;
        item_manager.chicken_die = false;
        item_manager.tiger_die = false;

        //GameObject cow = Instantiate(cow);
        //GameObject chicken = Instantiate(chicken);
        //GameObject tiger = Instantiate(tiger);

        //농장씬 다시 로드
        e_attack_data.isBattle = false;
        SceneManager.LoadScene("Farm_Scene");
    }
}
