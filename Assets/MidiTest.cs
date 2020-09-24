using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MidiTest : MonoBehaviour
{
    public ParticleSystem hitParticles;

    // connect to drum pad here
    public Renderer drumPad;
    public Color drumColor = Color.red;

    public AudioSource topLeft;
    public AudioSource topRight;
    public AudioSource lowLeft;
    public AudioSource lowRight;

    public Animator animator;
    public bool shouldAnimate = false;
    public string triggerName = "l_hit";

    private AudioSource audioSource;
    private float velocity;
    public int position = 0;
    public float maxAnimSpeed, minAnimSpeed = 3f;

    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    
    // Update is called once per frame
    void Update()
    {
        drumPad.material.SetColor("_Color", drumColor);

        if (shouldAnimate) {
            hitParticles.Emit(30);
            shouldAnimate = false;
            // set speed of animation based on midi velocity
            animator.speed = VelocityMap(velocity);
            // animator.SetTrigger(triggerName);
            // trigger sound
            if (triggerName == "l_l_hit") {
                lowLeft.Play();
                animator.SetTrigger("l_hit");
            }
            if (triggerName == "l_r_hit") {
                lowRight.Play();
                animator.SetTrigger("r_hit");
            }
            if (triggerName == "u_l_hit") {
                topLeft.Play();
                animator.SetTrigger("l_hit");
            }
            if (triggerName == "u_r_hit") {
                topRight.Play();
                animator.SetTrigger("r_hit");
            }
        }
    }

    public void PlayMIDINote(int note, int velocity)
    {
        if (velocity != 0) {
            if (note == 2) {
                drumColor = Color.red;
                shouldAnimate = true;
                triggerName = "u_l_hit";
            }
            if (note == 95) {
                // Right high
                // tempFreq = 440.0;
                drumColor = Color.blue;
                shouldAnimate = true;
                triggerName = "u_r_hit";
            }
            if (note == 3) {
                // tempFreq = 340.0;
                drumColor = Color.green;
                shouldAnimate = true;
                triggerName = "l_l_hit";
            }
            if (note == 4) {
                // Right low
                // tempFreq = 240.0;
                drumColor = Color.yellow;
                shouldAnimate = true;
                triggerName = "l_r_hit";
            }
        } else {
            // gain = 0.0f;
        }
    }

    // maps midi velocity of 0-127 to range of animation speed
    public float VelocityMap(float vel)
    {
        return vel * ((maxAnimSpeed - minAnimSpeed) / 127f) + minAnimSpeed;

    }
}
