using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimTest : MonoBehaviour
{
    private bool sendToLocal = true;
    public PlayerDrum localDrum;
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
        if(sendToLocal)
        {
            if(Input.GetKeyDown(KeyCode.A))
            {
                localDrum.PlayMIDINote(36, 100);
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                localDrum.PlayMIDINote(37, 100);
            }

            if(Input.GetKeyDown(KeyCode.D))
            {
                localDrum.PlayMIDINote(38, 100);         
            }
                
            if (Input.GetKeyDown(KeyCode.F))
            {
                localDrum.PlayMIDINote(39, 100);
            }
        }
        else
        {
            if(Input.GetKeyDown(KeyCode.A))
            {
                LoopManager.instance.drumList[LoopManager.instance.drumList.Count-1].GetComponentInChildren<PlayerDrum>().PlayMIDINote(36, 100);
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                LoopManager.instance.drumList[LoopManager.instance.drumList.Count-1].GetComponentInChildren<PlayerDrum>().PlayMIDINote(37, 100);
            }

            if(Input.GetKeyDown(KeyCode.D))
            {
                LoopManager.instance.drumList[LoopManager.instance.drumList.Count-1].GetComponentInChildren<PlayerDrum>().PlayMIDINote(38, 100);         
            }
                
            if (Input.GetKeyDown(KeyCode.F))
            {
                LoopManager.instance.drumList[LoopManager.instance.drumList.Count-1].GetComponentInChildren<PlayerDrum>().PlayMIDINote(39, 100);
            }
        }
    }
}
