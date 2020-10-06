using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UDPLocalMIDIConnector : MonoBehaviour
{
    public MidiTest drum;
    public UDPServerConnector server;
    private AndroidJavaObject udpLocalClass;
    public string ip = "127.0.0.1";
    // Start is called before the first frame update
    void Start()
    {
        udpLocalClass = new AndroidJavaObject("com.acme.networkinglibrary.UDPMidiConnector");
        udpLocalClass.Call("StartConnectionThread", ip);
    }

    public void PlayNoteFromLocal(string midiInfo) {
        Debug.Log("got msg from native " + midiInfo);
        if (!midiInfo.Contains("alert"))
        {
            string[] packetInfo = midiInfo.Split(';');
            string[] note = packetInfo[2].Split(',');
            if (Convert.ToInt32(note[0]) != 0)
            {
                drum.PlayLocalSound(Convert.ToInt32(note[1]), Convert.ToInt32(note[2]));
            }
        }
    }
}
