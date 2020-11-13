using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChange : MonoBehaviour
{
    public SkinnedMeshRenderer AvatarJoints;
    public SkinnedMeshRenderer AvatarSurface;

    public Material blueJoint, blueSurface;
    public Material greenJoint, greenSurface;
    public Material yellowJoint, yellowSurface;
    public Material orangeJoint, orangeSurface;
    public Material purpleJoint, purpleSurface;
    public Material pinkJoint, pinkSurface;

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
