using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Text myText;
    public void Handshake()
    {
        javaClass.Call("Connect");
        //send handshake message to server
    }

    public void DisplayConnected()
    {
        myText.text = "You are now connected!";
    }

    public void Player2Connect()
    {
        myText.text = "You are now connected!";
    }
}
