using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NativeWebSocket;
using System.Text;

public class GameNetworking : MonoBehaviour
{
    [SerializeField]
    private GameObject MyPlayer;
    [SerializeField]
    private GameObject OtherPlayer;
    WebSocket websocket;
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
            Debug.Log(bytes);
            var message = System.Text.Encoding.UTF8.GetString(bytes);
            Debug.Log(message);
            string[] xyz = message.Split(',');

            if(float.Parse(xyz[0]) == 1.0f){
                OtherPlayer.transform.position = new Vector3(float.Parse(xyz[1]), float.Parse(xyz[2]), float.Parse(xyz[3]));
            }

            if(float.Parse(xyz[0]) == 2.0f){
                MyPlayer.transform.position = new Vector3(float.Parse(xyz[1]), float.Parse(xyz[2]), float.Parse(xyz[3]));
            }

            // getting the message as a string
            // var message = System.Text.Encoding.UTF8.GetString(bytes);
            // Debug.Log("OnMessage! " + message);
        };

        // Keep sending messages at every 0.3s
        InvokeRepeating("SendWebSocketMessage", 0.0f, 0.02f);

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
}
