using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UDPServerConnector : MonoBehaviour
{
    public MidiTest drum;
    private AndroidJavaObject udpServerClass;
    public string id = "0";
    public string ip = "127.0.0.1";
    private float connectTimer = 0.5f;
    private bool sentConnect = false;

    public string username = "chad";
    // Start is called before the first frame update

    void Start() {
        udpServerClass = new AndroidJavaObject("com.acme.networkinglibrary.UDPServerConnector");
        udpServerClass.Call("StartConnectionThread", ip, username);
    }

    void Update() {
        // if (!sentConnect && connectTimer <= 0) {
        //     sentConnect = true;
        //     SendServerMessage("0,0,0," + id);
        // }
        // connectTimer -= Time.deltaTime;
    }

    public void PlayNoteFromServer(string midiInfo) {
        // Debug.Log("got msg from server " + midiInfo);
        if (!midiInfo.Contains("alert")) {
            string[] packetInfo = midiInfo.Split(';');
            string[] note = packetInfo[2].Split(',');
            if (Convert.ToInt32(note[0]) != 0) {
                drum.PlayMIDINote(Convert.ToInt32(note[1]), Convert.ToInt32(note[2]));
            }
        }
    }

    public void SendServerMessage(string msg) {
        // udpServerClass.Call("SendMessage", msg);
    }
}
