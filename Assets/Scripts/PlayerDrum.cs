using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDrum : MonoBehaviour
{
    public List<AudioClip> clips;
    public GameObject audioPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void PlayMIDINote(int note, int velocity) {
        // Debug.Log("Play note:" + note + "velocity: " + velocity);
        if (note > 3 || note < 0) return;

        GameObject newNote = Instantiate(audioPrefab, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
        newNote.GetComponent<AudioSource>().PlayOneShot(clips[note], 0.7f);
    }

    // // Update is called once per frame
    // void Update()
    // {
        
    // }
}
