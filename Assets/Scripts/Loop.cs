using System;
using UnityEngine;
using System.IO;

public class Loop : MonoBehaviour
{
    private PlayerDrum drum;

    private MemoryStream memoryStream = null;
    private BinaryWriter binaryWriter = null;
    private BinaryReader binaryReader = null;

    private bool recordingInitialized = false;
    public bool recording = false;
    public bool looping = false;

    private int currentRecordingFrames;
    private int maxRecordingFrames;
    private int counter = 0;
    public int index;
    public MIC microphone;
    public int sampleRate = 44100;
    private string micName;
    AudioSource audioSource;

    public enum MIC
    {
        Realtek,
        Android,
        Builtin
    };
    void Start()
    {
        switch(microphone)
        {
            case MIC.Realtek:
                micName = "Microphone (Realtek(R) Audio)";
                break;
            case MIC.Android:
                micName = "Android audio input";
                break;
            case MIC.Builtin:
                micName = "Built-in Microphone";
                break;
            default:
                micName = "Built-in Microphone";
                break;
        }
        Debug.Log("Mic: " + micName);
        audioSource = GetComponent<AudioSource>();
        if(!LoopManager.instance.firstLoopCreated)
        {
            audioSource.clip = Microphone.Start(micName, false, 60, sampleRate); //Microphone (Realtek(R) Audio), Android audio input, Built-in Microphone
            //Debug.Log("Mic start");
        }
        else
        {
            audioSource.clip = Microphone.Start(micName, false, LoopManager.instance.recordingLength/sampleRate, sampleRate);
        }
        //recordText.SetActive(true);
        maxRecordingFrames = LoopManager.instance.loopLength-1; // if this is the first loop length will default to 0 (note: first loop condition is checked in UpdateRecording())
        drum = LoopManager.instance.drumList[index].GetComponentInChildren<PlayerDrum>();

        StartRecording();
    }

    public void FixedUpdate()
    {
        if (recording)
        {
            UpdateRecording();
            drum.noteNum = 0; //ensures that if the PlayMIDINote function hasn't been called this frame, noteNum is set to 0
        }
        else if (looping)
        {
            UpdateLooping();
        }
        if(!looping)
        {
            StopLooping();
        }
        
    }

    private void InitializeRecording()
    {
        memoryStream = new MemoryStream();
        binaryWriter = new BinaryWriter(memoryStream);
        binaryReader = new BinaryReader(memoryStream);
        recordingInitialized = true;
    }

    public void StartRecording()
    {
        if (!recordingInitialized)
        {
            InitializeRecording();
        }
        else
        {
            memoryStream.SetLength(0);
        }
        ResetLoopFrame();
        drum.noteNum = 0;
        recording = true;
    }

    private void UpdateRecording()
    {
        if(LoopManager.instance.firstLoopCreated)
        {
            if (currentRecordingFrames > maxRecordingFrames)
            {
                StopRecording();
                StartLooping();
                currentRecordingFrames = 0;
                return;
            }
        }
        else
        {
            counter++;
        }
        SaveHit(drum);
        currentRecordingFrames++;
    }

    public void StopRecording()
    {
        if(!LoopManager.instance.firstLoopCreated)
        {
            LoopManager.instance.loopLength = counter;
            //Microphone.End("Microphone (Realtek(R) Audio)"); //Microphone (Realtek(R) Audio), Android audio input, Built-in Microphone
            //Debug.Log("Mic end");
            EndRecording(audioSource, micName);
        }
        recording = false;
        //recordText.SetActive(false);
    }

    private void ResetLoopFrame()
    {
        memoryStream.Seek(0, SeekOrigin.Begin);
        binaryWriter.Seek(0, SeekOrigin.Begin);
    }

    public void StartLooping()
    {
        audioSource.Play();
        //Debug.Log("play audio");
        ResetLoopFrame();
        looping = true;
    }

    private void UpdateLooping()
    {
        if (memoryStream.Position >= memoryStream.Length)
        {
            StartLooping();
            return;
        }

        PlayHit(drum);
    }

    public void StopLooping()
    {
        audioSource.Stop();
        looping = false;
    }

    private void SaveHit(PlayerDrum drum)
    {
        binaryWriter.Write(drum.noteNum);
        binaryWriter.Write(drum.noteVel);
        //Debug.Log(drum.noteNum);
    }

    private void PlayHit(PlayerDrum drum)
    {
        int note = binaryReader.ReadInt32();
        int vel = binaryReader.ReadInt32();
        //Debug.Log(binaryReader.ReadInt32());
        drum.PlayMIDINote(note, vel);
    }

    void EndRecording(AudioSource audS, string deviceName)
    {
        //Capture the current clip data
        AudioClip recordedClip = audS.clip;
        var position = Microphone.GetPosition(micName);
        var soundData = new float[recordedClip.samples * recordedClip.channels];
        recordedClip.GetData(soundData, 0);
 
        //Create shortened array for the data that was used for recording
        var newData = new float[position * recordedClip.channels];
 
        Microphone.End (micName);
        //Copy the used samples to a new array
        for (int i = 0; i < newData.Length; i++)
        {
            newData[i] = soundData[i];
        }
 
        //One does not simply shorten an AudioClip,
        //    so we make a new one with the appropriate length
        Debug.Log("Length: " + position);
        var newClip = AudioClip.Create(recordedClip.name, position, recordedClip.channels, recordedClip.frequency, false);
        newClip.SetData(newData, 0);        //Give it the data from the old clip
 
        //Replace the old clip
        AudioClip.Destroy(recordedClip);
        audS.clip = newClip;
        LoopManager.instance.recordingLength = position;   
    }

}

