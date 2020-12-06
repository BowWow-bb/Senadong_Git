using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class DontDestroyOnLoad : MonoBehaviour
{
    //singleton pattern: 전역변수를 사용하지 않고 객체 하나만 생성..?
    //씬 전환에 따른 오브젝트 보존 및 중복 생성 방지...
    private static int count = 0;
    private int index;

    void Awake()
    {
        index = count;
        Debug.Log("awake, " + gameObject.name + ", index is " + index);
        if (index == 0)
        {
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }
    private void Start()
    {
        count++;
        if(SceneManager.GetActiveScene().name == "Battle_Scene")   //배틀씬인 경우
        {
            gameObject.SetActive(false);
        }

    }
}
