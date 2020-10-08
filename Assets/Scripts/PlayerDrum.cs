using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDrum : MonoBehaviour
{
    public List<AudioClip> clips;
    public GameObject audioPrefab;

    public Renderer drumPad;

    public ParticleSystem hitParticles;

    public Animator animator;


    private float velocity;
    public float maxAnimSpeed = 3f;
    public float minAnimSpeed = 1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void PlayMIDINote(int note, int velocity) {
        // Debug.Log("Play note:" + note + "velocity: " + velocity);
        if (note > 3 || note < 0) return;

        GameObject newNote = Instantiate(audioPrefab, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
        newNote.GetComponent<AudioSource>().PlayOneShot(clips[note], velocity / 127.0f);

        if (velocity != 0)
        {
            animator.speed = VelocityMap(velocity);
            if (note == 1)
            {
                drumPad.material.SetColor("_Color", Color.red);
                animator.Play("Left Side.Left Hit", 1, 0);
            }
            if (note == 0)
            {
                drumPad.material.SetColor("_Color", Color.blue);
                animator.Play("Right Side.Right Hit", 2, 0);
            }
            if (note == 2)
            {
                drumPad.material.SetColor("_Color", Color.green);
                animator.Play("Left Side.Left Hit", 1, 0);
            }
            if (note == 3)
            {
                drumPad.material.SetColor("_Color", Color.yellow);
                animator.Play("Right Side.Right Hit", 2, 0);
            }
        }
    }

    // // Update is called once per frame
    void Update()
    {
        
    }

    public float VelocityMap(float vel)
    {
        return vel * ((maxAnimSpeed - minAnimSpeed) / 127f) + minAnimSpeed;

    }
}
