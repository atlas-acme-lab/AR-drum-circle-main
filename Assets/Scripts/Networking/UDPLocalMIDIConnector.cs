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

    private List<int> playQueue = new List<int>();

    public string ip = "127.0.0.1";
    // Start is called before the first frame update
    void Start()
    {
        udpLocalClass = new AndroidJavaObject("com.acme.networkinglibrary.UDPMidiConnector");
        udpLocalClass.Call("StartConnectionThread", ip);
    }

    void Update() {
        if (playQueue.Count > 0) {
            GameObject newNote = Instantiate(audioPrefab, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
            newNote.GetComponent<AudioSource>().PlayOneShot(clips[playQueue[0]], 0.7f);
            playQueue.RemoveAt(0);
        }
    }

    public void PlayNoteFromLocal(string midiInfo) {
        // Debug.Log("got msg from native " + midiInfo);
        if (!midiInfo.Contains("alert"))
        {
            string[] packetInfo = midiInfo.Split(';');
            string[] note = packetInfo[2].Split(',');
            // Debug.Log("play note?");
            if (Convert.ToInt32(note[0]) != 0 && Convert.ToInt32(note[2]) != 0)
            {
                playQueue.Add(Convert.ToInt32(note[1]));
                // audio.PlayOneShot(clips[Convert.ToInt32(note[1])], 0.7f);
                // drum.PlayLocalSound(Convert.ToInt32(note[1]), Convert.ToInt32(note[2]));
            }
        }
    }
}
