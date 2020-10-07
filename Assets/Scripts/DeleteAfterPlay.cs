using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// For deleting audio clips after they are finished playing
// Currently it just waits 1 second to delete

public class DeleteAfterPlay : MonoBehaviour
{
    private AudioSource audio;
    private float timeToLive;
    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        timeToLive += Time.deltaTime;
        if (timeToLive > 1f) {
            Destroy(gameObject);
        }
    }
}
