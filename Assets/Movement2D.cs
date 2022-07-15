using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement2D : MonoBehaviour
{
    private float moveSpeed = 1.0f;

    private int move_mode;

    private Vector3 target_pos;
    private Vector3 moveDirection;

    public void SetTargetPos(){
        move_mode = 0;
        
        float xPos = transform.position.x + Random.Range(transform.position.x - 10, transform.position.x + 10);
        float yPos = transform.position.y + Random.Range(transform.position.y - 10, transform.position.y + 10);

        target_pos = new Vector3(xPos, yPos, 0);
    }

    public void SetDirection(Vector3 direction){
        move_mode = 1;
        moveDirection = direction;
    }

    void Update(){
        switch(move_mode){
            case 0:
                transform.position = Vector3.MoveTowards(transform.position, target_pos, moveSpeed * Time.deltaTime);
                break;
            case 1:
                transform.position += moveDirection * moveSpeed * Time.deltaTime;
                break;
        }
    }
}
