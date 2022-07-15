using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSystemScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject computerPrefab;
    private List<GameObject> clone_arr;
    [SerializeField]
    private GameObject wallPrefab;

    private int move_mode;

    public float timer; 
    public int newtarget;

    void Start()
    {
        move_mode = 0;
        timer = 0f;

        clone_arr = new List<GameObject>();

        int dx = Random.Range(-1, 1);
        int dy = Random.Range(-1, 1);
        for(int i = 0; i < 20; i++){
            int x = Random.Range(-9, 9);
            int y = Random.Range(-4, 4);
            GameObject clone = Instantiate(computerPrefab, new Vector3(x, y, 0), Quaternion.identity);
            clone.name = "Box" + i;
            switch(move_mode){
                case 0:
                    clone.GetComponent<Movement2D>().SetTargetPos();
                    break;
                case 1:
                    clone.GetComponent<Movement2D>().SetDirection(new Vector3(dx, dy, 0));
                    break;
            }
            clone_arr.Add(clone);
        }

        for(int i = 0; i <= 40; i++){
            for(int j = 0; j <= 60; j++){
                if(i == 0 || i == 40 || j == 0 || j == 60)
                    Instantiate(wallPrefab, new Vector3(j-30, i-20, 0), Quaternion.identity);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= 5.0f){
            switch(move_mode){
                case 0:
                    ChangeTargetPos();
                    break;
                case 1:
                    ChangeDirection();
                    break;
            }
            move_mode = Random.Range(0, 2);
            Debug.Log("move_mode : " +  move_mode);
            timer = 0.0f;
        }
    }
    
    void ChangeDirection(){
        
        foreach (GameObject clone in clone_arr)
        {
            int dx = Random.Range(-1, 2);
            int dy = Random.Range(-1, 2);
            clone.GetComponent<Movement2D>().SetDirection(new Vector3(dx, dy, 0));
        }
    }

    void ChangeTargetPos(){
        int tx = Random.Range(-10, 10);
        int ty = Random.Range(-10, 10);
        foreach (GameObject clone in clone_arr)
        {
            clone.GetComponent<Movement2D>().SetTargetPos();
        }
    }
}
