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

    public bool kicked = false;


    public LayerMask bombLayerMask;

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
                RaycastHit2D hit = Physics2D.CircleCast(transform.position + (dir * (i+1)), 0.45f,new Vector3(0,0,1), Mathf.Infinity, blastCollision);
                if (hit.collider != null)
                {
                    if (hit.collider.tag == "Destructable") {
                        hit.collider.SendMessage("Destroy");
                    }if (hit.collider.tag == "FlameHit")
                    {
                        Vector3 pos = transform.position + (dir * (i + 1));
                        GameObject blastInstance = Instantiate(blast, pos, Quaternion.identity) as GameObject;
                        print("blast hit player collider");
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

    public void Kick(Vector3 dir) {
        kicked = true;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir.normalized,15,bombLayerMask);
        Vector3 end = new Vector3(0, 0, 0);
        if (hit.collider == null)
        {
            end = transform.position + (dir.normalized * 15);
        }
        else
        {
            Vector3 point = hit.point;
            point = point - (dir.normalized * 0.2f);
            end.x = Mathf.Round(point.x);
            end.y = Mathf.Round(point.y);

            
        }
        StartCoroutine("MoveBomb", end);
    }
    public void StopKick() {
        if (kicked) {
            StopCoroutine("MoveBomb");
            
        }
    }

    IEnumerator MoveBomb(Vector3 destination) {
        print(transform.position + ":" + destination);
        float i = 0;
        while (transform.position != destination) {
            i += 0.1f * Time.deltaTime;
            Vector3 lerp = Vector3.Lerp(transform.position, destination, i);
            transform.position = lerp;
            yield return null;
        }
        kicked = false;
    }
}
