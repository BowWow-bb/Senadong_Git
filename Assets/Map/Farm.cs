using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Farm : MonoBehaviour
{
    GameObject ItemSlot;
    Vector3 Pos;

    E_AttackData e_attack_data;
    // Start is called before the first frame update
    void Start()
    {
        ItemSlot = GameObject.Find("ItemSlot");
        Pos = ItemSlot.transform.position;//농장 가기 전 위치 

        //적군 공격력 증가 시작(전투 진행중이 아님을 알림)
        e_attack_data = GameObject.Find("E_AttackData").GetComponent<E_AttackData>();
        e_attack_data.isBattle = false;
    }

    // Update is called once per frame
    public void OnClick()
    {
        ItemSlot.transform.position = new Vector3(Pos.x - 10, Pos.y, Pos.z);
        SceneManager.LoadScene("Farm_Scene");
    }
}
