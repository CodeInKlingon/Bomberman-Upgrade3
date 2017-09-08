using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour {

    float bombTime;
    bool hasExploded = false;
    public int range = 3;
    bool remote = true;

    float startTime;

    public LayerMask blastCollision;
    public GameObject blast;

    bool armed = false;

    float blasthalfWidth = 0.4f;

	// Use this for initialization
	public void DisableRemote () {
        bombTime = Time.time + 2;
        armed = true;
	}

    private void Awake()
    {
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update () {
        
        if (armed && Time.time > bombTime && !hasExploded) {
            hasExploded = true;
            Blast();
        }
	}

    public void Blast () {
        Vector3[] directions = new Vector3[]{ Vector3.up, Vector3.left, Vector3.down, Vector3.right };
        foreach (Vector3 dir in directions) {
            for (int i = 0; i < range; i++)
            {
                RaycastHit2D hit = Physics2D.CircleCast(transform.position + (dir * (i+1)), 0.5f,new Vector3(0,0,1),3);
                if (hit.collider != null)
                {
                    if (hit.collider.tag == "Destructable") {
                        hit.collider.SendMessage("Destroy");
                    }if (hit.collider.tag == "Player")
                    {
                        Vector3 pos = transform.position + (dir * (i + 1));
                        GameObject blastInstance = Instantiate(blast, pos, Quaternion.identity) as GameObject;

                        if (i == range - 1)
                        {
                            blastInstance.GetComponent<Blast>().setIsTip();
                        }
                        blastInstance.GetComponent<Blast>().setDir(dir);
                    }
                    else
                    {
                        break;
                    }
                    
                }
                else
                {
                    Vector3 pos = transform.position + (dir * (i+ 1));
                    GameObject blastInstance = Instantiate(blast, pos, Quaternion.identity) as GameObject;

                    if (i == range-1) {
                        blastInstance.GetComponent<Blast>().setIsTip();
                    }
                    blastInstance.GetComponent<Blast>().setDir(dir);
                }
            }
            
            
        }
        GameObject blastInstance2 = Instantiate(blast, transform.position, Quaternion.identity) as GameObject;
        blastInstance2.GetComponent<Blast>().Center();
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Time.time - startTime > .2f)
        {
            if (collision.gameObject.tag == "Player")
            {
                print("is player");
                if (collision.GetComponent<PlayerMove>().kick)
                {
                    print("has kick");
                }
            }
        }
    }
}
