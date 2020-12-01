using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Infoclick_cow : MonoBehaviour
{
    GameObject FloatingValue;
    Cow_Move cow;
    BarMove hungry, poop, play;

    public int hungry_child = 2, poop_child = 4, play_child = 6;

    // Start is called before the first frame update
    void Start()
    {
        cow = transform.GetComponent<Cow_Move>();
        FloatingValue = transform.GetChild(0).gameObject;
        FloatingValue.GetComponent<Renderer>().enabled = true;
    }

    // Update is called once per frame
    private void OnMouseDown()
    {
        FloatingValue = transform.GetChild(0).gameObject;
        FloatingValue.SetActive(true);
        //FloatingValue.GetComponent<Renderer>().enabled = false;

        hungry = (transform.GetChild(0)).GetChild(hungry_child).GetComponent<BarMove>();
        poop = (transform.GetChild(0)).GetChild(poop_child).GetComponent<BarMove>();
        play = (transform.GetChild(0)).GetChild(play_child).GetComponent<BarMove>();

        hungry.hpMove(cow.hungry);
        poop.hpMove(cow.poop);
        play.hpMove(cow.play);

        StartCoroutine(Disabled(2.0f));
    }
    IEnumerator Disabled(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        //FloatingValue.GetComponent<Renderer>().enabled = true;
        FloatingValue.SetActive(true);
    }
}
