using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Devices;

public class MidiTest : MonoBehaviour
{
    private Playback _playback;
    private IInputDevice _inputDevice;

    private IOutputDevice output;
    // Start is called before the first frame update

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
    public int position = 0;

    void Start()
    {
        animator = gameObject.GetComponent<Animator>();

        _inputDevice = InputDevice.GetById(0);
        _inputDevice.EventReceived += OnMIDIEventReceived;
        _inputDevice.StartEventsListening();
    }

    
    // Update is called once per frame
    void Update()
    {
        drumPad.material.SetColor("_Color", drumColor);

        if (shouldAnimate) {
            shouldAnimate = false;
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

    void OnApplicationQuit() {
        (_inputDevice as IDisposable)?.Dispose();
    }

    private void OnMIDIEventReceived(object sender, MidiEventReceivedEventArgs e)
    {
        
        var midiDevice = (MidiDevice)sender;
        if ((e.Event as NoteEvent).Velocity != 0) {
            // Debug.Log((e.Event as NoteEvent).Velocity);
            if ((e.Event as NoteEvent).NoteNumber == 2) {
                drumColor = Color.red;
                shouldAnimate = true;
                triggerName = "u_l_hit";
            }
            if ((e.Event as NoteEvent).NoteNumber == 95) {
                // Right high
                // tempFreq = 440.0;
                drumColor = Color.blue;
                shouldAnimate = true;
                triggerName = "u_r_hit";
            }
            if ((e.Event as NoteEvent).NoteNumber == 3) {
                // tempFreq = 340.0;
                drumColor = Color.green;
                shouldAnimate = true;
                triggerName = "l_l_hit";
            }
            if ((e.Event as NoteEvent).NoteNumber == 4) {
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

    void OnEnable()
    {
        AssemblyReloadEvents.beforeAssemblyReload += OnBeforeAssemblyReload;
        AssemblyReloadEvents.afterAssemblyReload += OnAfterAssemblyReload;
    }

    void OnDisable()
    {
        AssemblyReloadEvents.beforeAssemblyReload -= OnBeforeAssemblyReload;
        AssemblyReloadEvents.afterAssemblyReload -= OnAfterAssemblyReload;
    }

    public void OnBeforeAssemblyReload()
    {
        Debug.Log("Before Assembly Reload");
    }

    public void OnAfterAssemblyReload()
    {
        Debug.Log("After Assembly Reload");
    }
}
