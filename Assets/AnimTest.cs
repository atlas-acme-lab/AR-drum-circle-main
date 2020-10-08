using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimTest : MonoBehaviour
{
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Pressed E");
            anim.Play("Right Side.Right Hit", 2, 0);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log("Pressed W");
            anim.Play("Left Side.Left Hit", 1, 0);
        }
    }
}
