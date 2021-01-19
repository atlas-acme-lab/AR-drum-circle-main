using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LoopManager : MonoBehaviour
{
    public int loopLength; //for midi/animation playback
    public bool firstLoopCreated = false;
    public int counter = 0;
    public GameObject loopPrefab;
    public List<GameObject> loopList = new List<GameObject>();
    public static LoopManager instance;

    public GameObject playerPrefab;
    public List<GameObject> drumList = new List<GameObject>();

    public int recordingLength; //for audio playback

    void Start()
    {
        if(instance == null)
        {
            instance = this;
        }
        //drumList.Add(GameObject.FindGameObjectWithTag("LocalDrum"));
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            PedalPress();
        }
    }

    private void PedalPress()
    {
        /*
        First press should start recording for indeterminate length
        Second press should end recording and save length
        Every subsequent press should start recording for determined length and automatically stop after that length of time
        (Loop number is determined in Loop script)
        */
        if (counter == 1)
        {
            //call stop recording on first instance and start looping
            loopList[0].GetComponent<Loop>().StopRecording();
            loopList[0].GetComponent<Loop>().StartLooping();
            firstLoopCreated = true;
        }
        else
        {
            //create new loop
            loopList.Add(Instantiate(loopPrefab));
            drumList.Add(Instantiate(playerPrefab)); //will have to add transform to get proper positioning
            //drumList[drumList.Count-1].GetComponent<MeshRenderer>().enabled = false;
            loopList[loopList.Count-1].GetComponent<Loop>().index = loopList.Count-1; //set index of loop based on position in loopList
        }
        counter++;
    }
}
