using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChange : MonoBehaviour
{
    public bool makePink = false;
    public bool makeOrange = false;
    public SkinnedMeshRenderer AvatarJoints;
    public SkinnedMeshRenderer AvatarSurface;

    public Material blueJoint, blueSurface;
    public Material greenJoint, greenSurface;
    public Material yellowJoint, yellowSurface;
    public Material orangeJoint, orangeSurface;
    public Material purpleJoint, purpleSurface;
    public Material pinkJoint, pinkSurface;

    void Start()
    {
        AvatarJoints = GetComponentsInChildren<SkinnedMeshRenderer>()[0];
        AvatarSurface = GetComponentsInChildren<SkinnedMeshRenderer>()[1];
    }

    void Update()
    {
        if(makePink)
        {
            ChangeToPink();
            makePink = false;
        }
        if(makeOrange)
        {
            ChangeToYellow();
            makeOrange = false;
        }
    }
    public void ChangeToBlue()
    {
        AvatarSurface.material = blueSurface;
        AvatarJoints.material = blueJoint;
    }

    public void ChangeToGreen()
    {
        AvatarSurface.material = greenSurface;
        AvatarJoints.material = greenJoint;
    }

    public void ChangeToYellow()
    {
        AvatarSurface.material = yellowSurface;
        AvatarJoints.material = yellowJoint;
    }

    public void ChangeToOrange()
    {
        AvatarSurface.material = orangeSurface;
        AvatarJoints.material = orangeJoint;
    }
    public void ChangeToPurple()
    {
        AvatarSurface.material = purpleSurface;
        AvatarJoints.material = purpleJoint;
    }
    public void ChangeToPink()
    {
        AvatarSurface.material = pinkSurface;
        AvatarJoints.material = pinkJoint;
    }
}
