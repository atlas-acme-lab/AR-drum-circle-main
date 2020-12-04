using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayback : MonoBehaviour
{
    public int loopLength = 4;
    public GameObject recordText;
    bool recordNew = false;
    bool recording = false;
    bool looping = false;
    bool recordingInitialized = false;
    AudioSource audioSource;

    void Start()
    {
        foreach (var device in Microphone.devices)
        {
            Debug.Log("Name: " + device);
        }
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if((Input.GetKeyDown(KeyCode.L)))
        {
            PedalPress();
        }
    }

    private void PedalPress()
    {
        if(!recordNew)
        {
            StartStopLooping();
            StartStopRecording();   
        }
        else
        {
            StartStopRecording(); //stop recording
            StartStopLooping(); //start looping
        }
        recordNew = !recordNew;
    }

    void StartStopRecording()
    {
        recordingInitialized = true;
        if(!recording)
        {
            audioSource.clip = Microphone.Start("Built-in Microphone", false, loopLength, 44100); //Microphone (Realtek(R) Audio), Android audio input, Built-in Microphone
            recordText.SetActive(true);
        }
        else
        {
            Microphone.End("Built-in Microphone"); //Microphone (Realtek(R) Audio), Android audio input, Built-in Microphone
            recordText.SetActive(false);
        }
        recording = !recording;
    }

    void StartStopLooping()
    {
        if(recordingInitialized)
        {
            if(!looping)
            {
                audioSource.Play();
            }
            else
            {
                audioSource.Stop();
            }
            looping = !looping;
        }
    }
}
