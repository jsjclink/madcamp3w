using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{ 
    [SerializeField]
    private GameObject bullet;
    private float moveSpeed = 2.0f;
    private Vector3 moveDirection = Vector3.zero;
    private Rigidbody2D rigid2D;
    private int bulletcount = 300;
    private SpriteRenderer spriteRenderer;
    
    // Start is called before the first frame update
    void Start()
    {
        rigid2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("right")){
            if (bulletcount > 0){
                GameObject clone = Instantiate(bullet, transform.position + new Vector3(1,0,0), Quaternion.identity);
                clone.GetComponent<BulletMovement>().setDirection(0);
                bulletcount--;
                Destroy(clone, 3.0f);
            }
        }
        if (Input.GetKeyDown("up")){
            if (bulletcount > 0){
                GameObject clone = Instantiate(bullet, transform.position + new Vector3(0,1,0), Quaternion.identity);
                clone.GetComponent<BulletMovement>().setDirection(1);
                bulletcount--;
                Destroy(clone, 3.0f);
            }
        }
        if (Input.GetKeyDown("down")){
            if (bulletcount > 0){
                GameObject clone = Instantiate(bullet, transform.position + new Vector3(0,-1,0), Quaternion.identity);
                clone.GetComponent<BulletMovement>().setDirection(2);
                bulletcount--;
                Destroy(clone, 3.0f);
            }
        }
        if (Input.GetKeyDown("left")){
            if (bulletcount > 0){
                GameObject clone = Instantiate(bullet, transform.position + new Vector3(-1,0,0), Quaternion.identity);
                clone.GetComponent<BulletMovement>().setDirection(3);
                bulletcount--;
                Destroy(clone, 3.0f);
            }
        }
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        if(x < 0){
            spriteRenderer.flipX = true;
        }
        else if(x > 0){
            spriteRenderer.flipX = false;
        }

        rigid2D.velocity = new Vector3(x,y,0) * moveSpeed;
    }
}
