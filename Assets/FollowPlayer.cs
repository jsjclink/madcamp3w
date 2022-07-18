using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField]
    public GameObject player;
    [SerializeField]
    private GameObject model;

    private GameObject clone;
    private static float term;
    private static float time = 0.0f;
    private bool isappeared = false;
    private static int choice = 0;
    private static int flag = 0;
    // Start is called before the first frame update
    void Start()
    {
        term = 3.0f;
    }
    public static void setterm(float amount, int choi){
        term = amount;
        choice = choi;
        time = 0.0f;
        flag = 1;
    }
    // Update is called once per frame
    void Update()
    {

        time += Time.deltaTime;
        
        if (time > term && flag == 1){
            if (transform.GetComponent<Camera>().orthographicSize <20){
                transform.GetComponent<Camera>().orthographicSize += 0.05f;
            }
            if (time > term + 3.0f && !isappeared){
                isappeared = true;
                Debug.Log("appeared");
                clone = Instantiate(model, new Vector3(-1,0,-1), Quaternion.identity);
                clone.GetComponent<ModelMovement>().setOption(choice);
                Destroy(clone, 3.0f);
            }
            else if (time > term + 8.0f){
                flag = 0;
                isappeared = false;
                transform.GetComponent<Camera>().orthographicSize = 5;
                time = 0.0f;
            }
            
        }
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -100);
        

        

    }

}

