using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDrum : MonoBehaviour
{
    public List<AudioClip> clips;
    public GameObject audioPrefab;

    public Renderer drumPad;
    public Color drumColor = Color.red;

    public ParticleSystem hitParticles;

    public Animator animator;
    public bool shouldAnimate = false;
    public string triggerName = "l_hit";

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
            if (note == 1)
            {
                drumColor = Color.red;
                shouldAnimate = true;
                triggerName = "u_l_hit";
            }
            if (note == 0)
            {
                // Right high
                // tempFreq = 440.0;
                drumColor = Color.blue;
                shouldAnimate = true;
                triggerName = "u_r_hit";
            }
            if (note == 2)
            {
                // tempFreq = 340.0;
                drumColor = Color.green;
                shouldAnimate = true;
                triggerName = "l_l_hit";
            }
            if (note == 3)
            {
                // Right low
                // tempFreq = 240.0;
                drumColor = Color.yellow;
                shouldAnimate = true;
                triggerName = "l_r_hit";
            }
        }
    }

    // // Update is called once per frame
    void Update()
    {
        drumPad.material.SetColor("_Color", drumColor);

        if (shouldAnimate)
        {
            hitParticles.Emit(30);
            shouldAnimate = false;
            // set speed of animation based on midi velocity
            animator.speed = VelocityMap(velocity);
            // animator.SetTrigger(triggerName);
            // trigger sound
            if (triggerName == "l_l_hit")
            {
                animator.SetTrigger("l_hit");
            }
            if (triggerName == "l_r_hit")
            {
                animator.SetTrigger("r_hit");
            }
            if (triggerName == "u_l_hit")
            {
                animator.SetTrigger("l_hit");
            }
            if (triggerName == "u_r_hit")
            {
                animator.SetTrigger("r_hit");
            }
        }
    }

    public float VelocityMap(float vel)
    {
        return vel * ((maxAnimSpeed - minAnimSpeed) / 127f) + minAnimSpeed;

    }
}
