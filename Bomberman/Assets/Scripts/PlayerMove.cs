using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    Vector3 playerInput = new Vector3(0, 0, 0);
    Vector3 lastInput;
    Rigidbody2D rb;
    public Animator anim;
    float moveSpeed = 5f;

    public string playerPrefix;

    List<GameObject> bombs = new List<GameObject>();
    bool moving;

    //power ups
    public int blastRange = 2;
    public int bombCapacity = 1;
    public float speed = 5;
    public bool remoteDetonate = false;
    public bool kick = false;
    public bool punch = false;

    public GameObject BombPrefab;
    public RuntimeAnimatorController[] bombermanController;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        switch (playerPrefix) {
            case "P2":
                anim.runtimeAnimatorController = bombermanController[1];
                break;
            case "P3":
                anim.runtimeAnimatorController = bombermanController[2];
                break;
            case "P4":
                anim.runtimeAnimatorController = bombermanController[3];
                break;
            default:
                anim.runtimeAnimatorController = bombermanController[0];
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        moving = false;
        float horizontal = Input.GetAxis("Horizontal-" + playerPrefix);
        float vertical = Input.GetAxis("Vertical-" + playerPrefix);
        playerInput = new Vector3(horizontal, vertical, 0);
        

        if (playerInput.x > 0 || playerInput.x < 0 || playerInput.y > 0 || playerInput.y < 0)
        {
            lastInput = playerInput;
            moving = true;
        }



        Vector3 movement = playerInput * moveSpeed * Time.deltaTime;
        rb.MovePosition(transform.position + movement);

        anim.SetFloat("XSpeed", playerInput.x);
        anim.SetFloat("YSpeed", playerInput.y);

        anim.SetFloat("LastXSpeed", lastInput.x);
        anim.SetFloat("LastYSpeed", lastInput.y);

        anim.SetBool("isMoving", moving);

        if (Input.GetButtonDown("Bomb-" + playerPrefix))
        {
            for (int i = 0; i < bombs.Count; i++)
            {
                if (bombs[i] == null) { bombs.RemoveAt(i); }
            }
            if (bombs.Count < bombCapacity)
            {


                Vector3 bombLocation = new Vector3();
                bombLocation.x = Mathf.Round(transform.position.x);
                bombLocation.y = Mathf.Round(transform.position.y);
                //check if a bomb can go there
                RaycastHit2D hit = Physics2D.CircleCast(bombLocation, 0.4f, new Vector3(0, 0, 1), 3);
                if (hit.collider.tag == "Bomb")
                {
                    //bomb aleady there
                    print("bomb already there");
                }
                else
                {
                    //spawn bomb
                    GameObject bomb = Instantiate(BombPrefab, bombLocation, Quaternion.identity) as GameObject;
                    bomb.GetComponent<Bomb>().range = blastRange;
                    if (!remoteDetonate)
                        bomb.GetComponent<Bomb>().DisableRemote();
                    bombs.Add(bomb);
                }
            }
        }
        if (Input.GetButtonDown("Detonate-" + playerPrefix) && remoteDetonate)
        {
            foreach (GameObject bomb in bombs) {
                bomb.SendMessage("Blast");
            }
        }
    }


    public void Kill() {

        Destroy(gameObject);
        //check other player count.

    }
    
}
