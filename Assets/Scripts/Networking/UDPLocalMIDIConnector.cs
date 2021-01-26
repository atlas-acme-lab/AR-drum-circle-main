using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UDPLocalMIDIConnector : MonoBehaviour
{
    public PlayerDrum localDrum;
    public UDPServerConnector server;
    private AndroidJavaObject udpLocalClass;

    public List<AudioClip> clips;
    public GameObject audioPrefab;

    private bool sendToLocal = true;

    public string ip = "127.0.0.1";
    // Start is called before the first frame update
    void Start()
    {
        udpLocalClass = new AndroidJavaObject("com.acme.networkinglibrary.UDPMidiConnector");
        udpLocalClass.Call("SetDebug", true);
        udpLocalClass.Call("StartConnectionThread", ip);
    }

    void Update()
    {
        if(LoopManager.instance.loopList.Count > 0)
        {
            foreach(GameObject loop in LoopManager.instance.loopList)
            {
                if(!loop.GetComponent<Loop>().recording)
                {
                    sendToLocal = true;
                }
                else
                {
                    sendToLocal = false;
                    break;
                }
            }
        }
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
                //Debug.Log("Local hit");
                if(sendToLocal)
                {
                    localDrum.PlayMIDINote(Convert.ToInt32(midiInfo[1]), Convert.ToInt32(midiInfo[2]));
                }
                else
                {
                    LoopManager.instance.drumList[LoopManager.instance.drumList.Count-1].GetComponentInChildren<PlayerDrum>().PlayMIDINote(Convert.ToInt32(midiInfo[1]), Convert.ToInt32(midiInfo[2]));
                }
                //comment out if only rendering local sound
                // int note = Convert.ToInt32(midiInfo[1]);                                               //For when you only want to play sound locally but not play animation
                // if (note > 3 || note < 0) return;
                // GameObject newNote = Instantiate(audioPrefab, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity); 
                // newNote.GetComponent<AudioSource>().PlayOneShot(clips[note], Convert.ToInt32(midiInfo[2]) / 127.0f);
            }
        }
    }
}
