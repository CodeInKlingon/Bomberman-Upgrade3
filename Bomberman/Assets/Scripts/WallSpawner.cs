using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSpawner : MonoBehaviour {

    public int height;
    public int width;
    public GameObject wall;
    string wallTag = "Wall";

	// Use this for initialization
	void Start () {
        for (int y = 0; y < height; y++){
            for (int x = 0; x < width; x++) {
                RaycastHit2D hit = Physics2D.CircleCast(transform.position + new Vector3(x,y,0), 0.2f, new Vector3(0, 0, 1), 3);
                if (hit.collider == null)
                {
                    float chance = Random.Range(0.0f, 100.0f);
                    if (chance < 90)
                    {
                        Instantiate(wall, transform.position + new Vector3(x, y, 0), Quaternion.identity);
                    }
                }
            }
        }
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
