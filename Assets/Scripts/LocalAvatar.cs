using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalAvatar : MonoBehaviour
{
    public Transform cam;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = cam.position + new Vector3(0f, 1f, 0f);
    }
}
