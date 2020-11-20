using System;
using UnityEngine;
using System.IO;

public class LoopManager : MonoBehaviour
{
    public PlayerDrum drum;

    private MemoryStream memoryStream = null;
    private BinaryWriter binaryWriter = null;
    private BinaryReader binaryReader = null;

    private bool recordingInitialized = false;
    private bool recording = false;
    private bool looping = false;

    public Action OnStartedRecording;
    public Action OnStoppedRecording;
    public Action OnStartedLooping;
    public Action OnStoppedLooping;

    public void FixedUpdate()
    {
        if (recording)
        {
            UpdateRecording();
            drum.noteNum = 999; //ensures that if the PlayMIDINote function hasn't been called this frame, noteNum is set to -1
        }
        else if (looping)
        {
            UpdateLooping();
        }
        
    }

    public void StartStopRecording()
    {
        if (!recording)
        {
            StartRecording();
            drum.noteNum = 999;
        }
        else
        {
            StopRecording();
        }
    }

    private void InitializeRecording()
    {
        memoryStream = new MemoryStream();
        binaryWriter = new BinaryWriter(memoryStream);
        binaryReader = new BinaryReader(memoryStream);
        recordingInitialized = true;
    }

    private void StartRecording()
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

        recording = true;
        if (OnStartedRecording != null)
        {
            OnStartedRecording();
        }
    }

    private void UpdateRecording()
    {
        SaveHit(drum);
    }

    private void StopRecording()
    {
        recording = false;
        if (OnStoppedRecording != null)
        {
            OnStoppedRecording();
        }
    }

    private void ResetLoopFrame()
    {
        memoryStream.Seek(0, SeekOrigin.Begin);
        binaryWriter.Seek(0, SeekOrigin.Begin);
    }

    public void StartStopLooping()
    {
        if (!looping)
        {
            StartLooping();
        }
        else
        {
            StopLooping();
        }
    }

    private void StartLooping()
    {
        ResetLoopFrame();
        looping = true;
        if (OnStartedLooping != null)
        {
            OnStartedLooping();
        }
    }

    private void UpdateLooping()
    {
        if (memoryStream.Position >= memoryStream.Length)
        {
            StopLooping();
            return;
        }

        LoadHit(drum);

    }

    private void StopLooping()
    {
        looping = false;
        if (OnStoppedLooping != null)
        {
            OnStoppedLooping();
        }
    }

    private void SaveHit(PlayerDrum drum)
    {
        binaryWriter.Write(drum.noteNum);
        binaryWriter.Write(drum.noteVel);
        //Debug.Log(drum.noteNum);
    }

    private void LoadHit(PlayerDrum drum)
    {
        int note = binaryReader.ReadInt32();
        int vel = binaryReader.ReadInt32();
        //Debug.Log(binaryReader.ReadInt32());
        drum.PlayMIDINote(note, vel);
    }
}
