using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    private int mode;
    private float time;
    // Start is called before the first frame update\
    
    public void setDirection(int direction){
        mode = direction;
    }
    void Start()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision){
        Debug.Log(collision.transform.name);
        Destroy(gameObject);
    }


    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        switch(mode){
            case 0:
                transform.position += Vector3.right * 15 * Time.deltaTime;
                break;
            case 1:
                transform.position += Vector3.up * 15 * Time.deltaTime;
                break;
            case 2:
                transform.position += Vector3.down * 15 * Time.deltaTime;
                break;
            case 3:
                transform.position += Vector3.left * 15 * Time.deltaTime;
                break;
        }
        
    }
}
