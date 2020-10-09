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


    private List<Color> palette = new List<Color>();
    public ParticleSystem particles;

    public Color drumColor;

    private float velocity;
    public float maxAnimSpeed = 3f;
    public float minAnimSpeed = 1f;
    // Start is called before the first frame update
    void Start()
    {
        palette.Add(new Color(0.9f, 0.31f, 0.42f));
        palette.Add(new Color(0.9f, 0.55f, 0.21f));
        palette.Add(new Color(0.33f, 0.42f, 0.78f));
        palette.Add(new Color(0.47f, 0.59f, 0.93f));

        drumColor = palette[0];
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
                drumPad.material.SetColor("_Color", palette[0]);
                particles.startColor = palette[0];
                animator.Play("Left Side.Left Hit", 1, 0);
            }
            if (note == 0)
            {
                drumPad.material.SetColor("_Color", palette[1]);
                particles.startColor = palette[1];
                animator.Play("Right Side.Right Hit", 2, 0);
            }
            if (note == 2)
            {
                drumPad.material.SetColor("_Color", palette[2]);
                particles.startColor = palette[2];
                animator.Play("Left Side.Left Hit", 1, 0);
            }
            if (note == 3)
            {
                drumPad.material.SetColor("_Color", palette[3]);
                particles.startColor = palette[3];
                animator.Play("Right Side.Right Hit", 2, 0);

            }
        }
    }

    // // Update is called once per frame


    public float VelocityMap(float vel)
    {
        return vel * ((maxAnimSpeed - minAnimSpeed) / 127f) + minAnimSpeed;

    }
}
