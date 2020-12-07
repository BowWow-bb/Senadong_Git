using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Battle : MonoBehaviour
{
    GameObject cow;
    GameObject tiger;
    GameObject chicken;
    // Start is called before the first frame update
    void Start()
    {
        cow = GameObject.FindWithTag("cow");
        tiger = GameObject.FindWithTag("tiger");
        chicken = GameObject.FindWithTag("chicken");
    }

    // Update is called once per frame
    public void OnClick()
    {
        cow.SetActive(false);
        tiger.SetActive(false);
        chicken.SetActive(false);
        SceneManager.LoadScene("Battle_Scene");

    }
}
