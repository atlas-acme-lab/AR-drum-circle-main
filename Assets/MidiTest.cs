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

    public double tempFreq = 440.0;
    private double increment;
    private double samplingFreq = 48000.0;
    private float volume = 0.01f;
    private double phase = 0.0;

    private float gain = 0.0001f;
    private float noteTime = 0.0f;

    private float[] freq1 = new float[2048];

    // connect to drum pad here
    public Renderer drumPad;
    public Color drumColor = Color.red;

    public Animator animator;
    public bool shouldAnimate = false;
    public string triggerName = "l_hit";

    private AudioSource audioSource;
    public int position = 0;
    public int samplerate = 44100;
    public float frequency = 440;
    public float playTime = 0.0f;
    public float t;

    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        increment = tempFreq * 2.0 * Mathf.PI / samplingFreq;

        for (int i = 0; i < freq1.Length; i += 2) {
            phase += increment;
            freq1[i] = (float)(gain * Mathf.Sin((float)phase));
            freq1[i + 1] = freq1[i];

            if (phase >= (Mathf.PI * 2)) {
                phase = 0.0;
            }
        }

        _inputDevice = InputDevice.GetById(0);
        _inputDevice.EventReceived += OnMIDIEventReceived;
        _inputDevice.StartEventsListening();

        // var devicesConnector = _inputDevice.Connect(output);
        
        // foreach (var outputDevice in OutputDevice.GetAll())
        // {
        //     Console.WriteLine(outputDevice.Name);
        // }
        int sampleFreq = 44000;
        float frequency = 440;
        
        float[] samples = new float[44000];
        for(int i = 0; i < samples.Length; i++)
        {
            samples[i] = Mathf.Sin(Mathf.PI*2*i*frequency/sampleFreq);
        }
        AudioClip myClip = AudioClip.Create("MySinusoid", samplerate * 5, 1, samplerate, false);
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = myClip;
        // aud.Play();
        myClip.SetData(samples, 0);
    }

    
    // Update is called once per frame
    void Update()
    {
        noteTime -= Time.deltaTime;
        drumPad.material.SetColor("_Color", drumColor);
        if (gain >= 0.001f) {
            // gain -= 0.4f * Time.deltaTime;
            // gain *= 0.95f;
        }

        t = Time.time;
        if (shouldAnimate) {
            shouldAnimate = false;
            animator.SetTrigger(triggerName);
            // trigger sound
            audioSource.Play();
            playTime = 0.1f;
            Debug.Log(Time.time);
        }

        if (playTime > 0.0f) {
            playTime -= Time.deltaTime;

            if (playTime <= 0.0f) {
                audioSource.Stop();
            }
        }
    }

    void OnApplicationQuit() {
        (_inputDevice as IDisposable)?.Dispose();
    }

    void OnAudioRead(float[] data)
    {
        int count = 0;
        while (count < data.Length)
        {
            data[count] = Mathf.Sin(2 * Mathf.PI * frequency * position / samplerate);
            position++;
            count++;
        }
    }

    void OnAudioSetPosition(int newPosition)
    {
        position = newPosition;
    }

    private void OnMIDIEventReceived(object sender, MidiEventReceivedEventArgs e)
    {
        
        var midiDevice = (MidiDevice)sender;
        if ((e.Event as NoteEvent).Velocity != 0) {
            // Debug.Log((e.Event as NoteEvent).Velocity);
            if ((e.Event as NoteEvent).NoteNumber == 2) {
                tempFreq = 540.0;
                drumColor = Color.red;
                shouldAnimate = true;
                triggerName = "l_hit";
            }
            if ((e.Event as NoteEvent).NoteNumber == 95) {
                // Right high
                tempFreq = 440.0;
                drumColor = Color.blue;
                shouldAnimate = true;
                triggerName = "r_hit";
            }
            if ((e.Event as NoteEvent).NoteNumber == 3) {
                tempFreq = 340.0;
                drumColor = Color.green;
                shouldAnimate = true;
                triggerName = "l_hit";
            }
            if ((e.Event as NoteEvent).NoteNumber == 4) {
                // Right low
                tempFreq = 240.0;
                drumColor = Color.yellow;
                shouldAnimate = true;
                triggerName = "r_hit";
            }
            gain = 0.1f;
            noteTime = 0.001f * (e.Event as NoteEvent).Velocity;
            // Debug.Log((e.Event as NoteEvent).NoteNumber);
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
