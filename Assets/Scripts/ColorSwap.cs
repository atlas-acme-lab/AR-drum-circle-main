using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NRKernal;

public class ColorSwap : MonoBehaviour
{
    public GameObject Obj;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
            if (NRInput.GetButtonDown(ControllerButton.TRIGGER))
            {
                Obj.GetComponent<Renderer>().material.color = new Color(Random.value, Random.value,
                    Random.value);
            }
    }
}
