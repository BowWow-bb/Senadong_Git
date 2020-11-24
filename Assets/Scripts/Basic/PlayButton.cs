using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayButton : MonoBehaviour
{
    bool active = false;
    Tiger_Move tiger_move;
    public void Click_PlayButton() // 놀아주기 버튼
    {
        if (active == false)
        {
            tiger_move.playing = true; // 놀아주기 비활성화 시에 놀아주기 활성화
            active = true;
        }
        else
        {
            tiger_move.playing = false ; // 비활성화
            active = false;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        tiger_move = GameObject.FindWithTag("tiger").GetComponent<Tiger_Move>(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
