using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngineInternal;

public class PlayerDrum : MonoBehaviour
{
    public List<AudioClip> clips;
    public GameObject audioPrefab;

    public Renderer drumPad;

    public Animator animator;


    private List<Color> palette = new List<Color>();
    public ParticleSystem particles;

    public Color drumColor;

    private float velocity;
    public float maxAnimSpeed = 3f;
    public float minAnimSpeed = 1f;

    public int noteNum = 0;
    public int noteVel = 0;

    void Start()
    {
        palette.Add(new Color(.9f, .9f, .3f));
        palette.Add(new Color(0.9f, 0.55f, 0.21f));
        palette.Add(new Color(0.33f, 0.42f, 0.78f));
        palette.Add(new Color(0.47f, 0.59f, 0.93f));

        drumColor = palette[0];
    }

    public void PlayMIDINote(int note, int velocity) {
        // Debug.Log("Play note:" + note + "velocity: " + velocity);
        if (note > 39 || note < 36) return;


        GameObject newNote = Instantiate(audioPrefab, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
        newNote.GetComponent<AudioSource>().PlayOneShot(clips[note-36], velocity / 127.0f);
        Destroy(newNote, 2);

        if (velocity != 0)
        {
            animator.speed = VelocityMap(velocity);
            noteVel = velocity;
            if (note == 37)
            {
                noteNum = 37;
                drumPad.material.SetColor("_Color", palette[0]);
                particles.startColor = palette[0];
                animator.Play("Left Side.Left Hit", 1, 0);
            }
            if (note == 36)
            {
                noteNum = 36;
                drumPad.material.SetColor("_Color", palette[1]);
                particles.startColor = palette[1]; 
                animator.Play("Right Side.Right Hit", 2, 0);
            }
            if (note == 38)
            {
                noteNum = 38;
                drumPad.material.SetColor("_Color", palette[2]);
                particles.startColor = palette[2];
                animator.Play("Left Side.Left Hit", 1, 0);
            }
            if (note == 39)
            {
                noteNum = 39;
                drumPad.material.SetColor("_Color", palette[3]);
                particles.startColor = palette[3];
                animator.Play("Right Side.Right Hit", 2, 0);
            }
        }
        particles.Emit(30);
    }


    public float VelocityMap(float vel)
    {
        return vel * ((maxAnimSpeed - minAnimSpeed) / 127f) + minAnimSpeed;

    }
}
