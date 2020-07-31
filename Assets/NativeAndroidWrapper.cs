using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NativeAndroidWrapper : MonoBehaviour
{
    public Text myText;
    public MidiTest drum;
    private AndroidJavaObject jClass;
    public string deviceName = "petroochio";
    private float timer = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        jClass = new AndroidJavaObject("com.example.bluetoothmidilibrary.TestClass");
        jClass.Call("SetDeviceName", deviceName);
        jClass.Call("ConnectToBTDrum");
    }

    public void PlayNoteFromBluetooth(string midiInfo) {
        string[] note = midiInfo.Split(',');
        drum.PlayMIDINote(Convert.ToInt32(note[1]), 10);
    }
}
