using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class item : MonoBehaviour {

    public enum ItemSprite { extraBomb, rangeBooster, speedBoost, detonator, maxRange, kick, punch, skull }

    public powerup effect;
    public ItemSprite itemSprite;
	// Use this for initialization
	void Start () {
        GetComponent<Animator>().Play(itemSprite.ToString());
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        print(other.tag);
        if (other.tag == "Player" && other.GetComponent<PowerUpManager>() !=null)
        {
            other.GetComponent<PowerUpManager>().AddPowerUp(effect);
            Destroy(gameObject);
        }
    }

    public void Destroy()
    {
        GetComponent<Animator>().Play("itemDestruction");
        Destroy(gameObject, .6f);
    }
    // Update is called once per frame
    void Update () {
		
	}
}
