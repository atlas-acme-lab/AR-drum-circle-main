using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Clock : MonoBehaviour
{
    public float bpm;
    public float time;
    public GameObject hand;
    public AudioSource tick;
    public Image marker;
    public List<Sprite> sprites = new List<Sprite>();
    private float rotSpeed;
    private bool rotate;
    public static Clock instance;

    void Start()
    {
        if(instance = null)
            instance = this;

        float rotTime = (60/bpm)*time;
        rotSpeed = 360/rotTime; // in degrees per second
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
            rotate = !rotate;
        if(rotate)
            hand.transform.Rotate ( Vector3.forward, ( rotSpeed * Time.deltaTime ) );
    }

    void PlaceMarker(KeyCode key)
    {
        switch(key)
        {
            case KeyCode.A:
                marker.sprite = sprites[0];
                break;
            case KeyCode.S:
                marker.sprite = sprites[1];
                break;
            case KeyCode.D:
                marker.sprite = sprites[2];
                break;
            case KeyCode.F:
                marker.sprite = sprites[3];
                break;
            case KeyCode.G:
                marker.sprite = sprites[4];
                break;
            case KeyCode.H:
                marker.sprite = sprites[5];
                break;
        }
        Instantiate(marker);
    }
}
