using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chicken_Attack : MonoBehaviour
{
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("is_Attack", true);
    }
    //행동 
    public bool Chicken_Basic_Attack()
    {
        return true;
    }
}
