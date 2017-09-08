using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blast : MonoBehaviour {


    bool isTip = false;
    Vector3 Direction = new Vector3();
    public void setIsTip() { isTip = true; }

    public void Center() { Destroy(gameObject, .8f); }

    // Use this for initialization
    public void setDir(Vector3 dir) { Direction = dir;

        GetComponent<Animator>().SetBool("isTip", isTip);
        GetComponent<Animator>().SetFloat("X", Direction.x);
        GetComponent<Animator>().SetFloat("Y", Direction.y);

        if (!isTip)
        {
            GetComponent<Animator>().Play("Middle Blend Tree");
        }
        else
        {
            GetComponent<Animator>().Play("Tip Blend Tree");
        }

        Destroy(gameObject, .8f);



	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player") {
            // eliminate that player
            print("got you");
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
