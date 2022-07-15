using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NativeWebSocket;
using System.Text;

public class GameNetworking : MonoBehaviour
{
    [SerializeField]
    private GameObject MyPlayer;

    WebSocket websocket;
    private int id;
    // Start is called before the first frame update
    async void Start()
    {
        websocket = new WebSocket("ws://192.249.18.176:443");

        websocket.OnOpen += () =>
        {
        Debug.Log("Connection open!");
        };

        websocket.OnError += (e) =>
        {
        Debug.Log("Error! " + e);
        };

        websocket.OnClose += (e) =>
        {
        Debug.Log("Connection closed!");
        };

        websocket.OnMessage += (bytes) =>
        {
            var byteStr = System.Text.Encoding.UTF8.GetString(bytes);

            Debug.Log("byteStr : " + byteStr);
            

            var event_name = byteStr.Split('!')[0];
            var message = byteStr.Split('!')[1];
            
            
            switch(event_name){
                case "id_set":
                    id = int.Parse(message);
                    Debug.Log("ID_SET : " + id);
                    MyPlayer.name = id+"";
                    GetComponent<GameSystemScript>().InitGame(id);
                    break;
                case "count_down":
                    Debug.Log("COUNT_DOWN");
                    InvokeRepeating("SendUnitPosition", 3.0f, 0.02f);
                    break;
                case "all_position":
                    break;
            }

            /*
            string[] xyz = message.Split(',');

            if(float.Parse(xyz[0]) == 1.0f){
                OtherPlayer.transform.position = new Vector3(float.Parse(xyz[1]), float.Parse(xyz[2]), float.Parse(xyz[3]));
            }

            if(float.Parse(xyz[0]) == 2.0f){
                MyPlayer.transform.position = new Vector3(float.Parse(xyz[1]), float.Parse(xyz[2]), float.Parse(xyz[3]));
            }*/

            // getting the message as a string
            // var message = System.Text.Encoding.UTF8.GetString(bytes);
            // Debug.Log("OnMessage! " + message);
        };

        // Keep sending messages at every 0.3s
        //InvokeRepeating("SendWebSocketMessage", 0.0f, 0.02f);

        // waiting for messages
        await websocket.Connect();
    }

    // Update is called once per frame
    void Update()
    {
        #if !UNITY_WEBGL || UNITY_EDITOR
            websocket.DispatchMessageQueue();
        #endif
    }

    async void SendWebSocketMessage()
    {
        if (websocket.State == WebSocketState.Open)
        {
            // Sending bytes
            // Sending plain text
            await websocket.SendText("2,"+ MyPlayer.transform.position.x + "," +MyPlayer.transform.position.y + "," + "asdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdaasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdsdasdasdasdasdasdasdaaaaaaaaaaaasdasdasdasdasaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaadasdasdasdasdasdasdasdasdasdasdasdasdasdasdasd" );
        }
    }

    private async void OnApplicationQuit()
    {
        await websocket.Close();
    }

    private async void SendUnitPosition(){
        //Debug.Log("SendUnitPosition");
        string send_str = "unit_position!" + id + "," + MyPlayer.transform.position.x + "," + MyPlayer.transform.position.y + ";" ;
        foreach (GameObject npc in GetComponent<GameSystemScript>().npc_arr)
        {
            if(npc.name.Split('_')[0] == id + ""){
                send_str += id + "," + npc.transform.position.x + "," + npc.transform.position.y + ";";
            }
        }
        await websocket.SendText(send_str);
    }
}