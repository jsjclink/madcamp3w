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
    private int term = 3;
    private float time = 0.0f;
    private bool isappeared = false;
    // Start is called before the first frame update
    void Start()
    {
        term = Random.Range(3,5);
    }

    // Update is called once per frame
    void Update()
    {

        time += Time.deltaTime;
        
        if (time > term){
            if (transform.GetComponent<Camera>().orthographicSize <20){
                transform.GetComponent<Camera>().orthographicSize += 0.05f;
            }
            if (time > 5 && !isappeared){
                isappeared = true;
                Debug.Log("appeared");
                clone = Instantiate(model, new Vector3(3,3,-1), Quaternion.identity);
                Destroy(clone, 3.0f);
            }
            else if (time > 10){
                isappeared = false;
                transform.GetComponent<Camera>().orthographicSize = 5;
                term = Random.Range(3,5);
                time = 0.0f;
            }
            
        }
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -100);
        

        

    }

}

