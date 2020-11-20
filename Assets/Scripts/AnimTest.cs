using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimTest : MonoBehaviour
{
    public PlayerDrum drum;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
            drum.PlayMIDINote(36, 100);

        if (Input.GetKeyDown(KeyCode.S))
            drum.PlayMIDINote(37, 100);

        if(Input.GetKeyDown(KeyCode.D))
            drum.PlayMIDINote(38, 100);
            
        if (Input.GetKeyDown(KeyCode.F))
            drum.PlayMIDINote(39, 100);

    }
}
