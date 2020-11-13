using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LoopMananger : MonoBehaviour
{

    public UDPLocalMIDIConnector localAvatar;

    private MemoryStream memoryStream = null;
    private BinaryWriter binaryWriter = null;
    private BinaryReader binaryReader = null;

    private bool recordingInitialized;
    private bool recording;
    private bool looping;

    void FixedUpdate()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            if(!recording)
            {
                StartRecording();
            }
            else
            {
                StopRecording();
            }
        }
        if(recording)
        {
            if(localAvatar.notePlayed)
            {
                binaryWriter.Write(1);
                localAvatar.notePlayed = false;
            }
            else
            {
                binaryWriter.Write(0);
            }          
        }
    }

    private void InitializeRecording()
    {
        memoryStream = new MemoryStream();
        binaryWriter = new BinaryWriter(memoryStream);
        binaryReader = new BinaryReader(memoryStream);
        recordingInitialized = true;
    }

    void StartRecording()
    {
        if (!recordingInitialized)
        {
            InitializeRecording();
        }
        else
        {
            memoryStream.SetLength(0);
        }
        ResetLoopingFrame();
        recording = true;
    }

    void StopRecording()
    {
        recording = false;
    }

    void StartLooping()
    {
        ResetLoopingFrame();
    }

    void StopLooping()
    {

    }

    private void ResetLoopingFrame()
    {
        memoryStream.Seek(0, SeekOrigin.Begin);
        binaryWriter.Seek(0, SeekOrigin.Begin);
    }

}
