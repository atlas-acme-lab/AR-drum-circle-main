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

    public List<AudioClip> clips;
    public GameObject audioPrefab;


    public string ip = "127.0.0.1";
    // Start is called before the first frame update
    void Start()
    {
        udpLocalClass = new AndroidJavaObject("com.acme.networkinglibrary.UDPMidiConnector");
        udpLocalClass.Call("SetDebug", true);
        udpLocalClass.Call("StartConnectionThread", ip);
    }

    public void PlayNoteFromLocal(string packetInfo) {
        // Debug.Log("got msg from native " + midiInfo);
        if (!packetInfo.Contains("alert"))
        {
            string[] parsedPacket = packetInfo.Split(';');
            string[] midiInfo = parsedPacket[2].Split(',');
            // Debug.Log("play note?");
            if (Convert.ToInt32(midiInfo[0]) != 0 && Convert.ToInt32(midiInfo[2]) != 0)
            {
                int note = Convert.ToInt32(midiInfo[1]);
                if (note > 3 || note < 0) return;
                GameObject newNote = Instantiate(audioPrefab, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
                newNote.GetComponent<AudioSource>().PlayOneShot(clips[note], 0.7f);
            }
        }
    }
}
