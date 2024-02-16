using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResolutionDisplay : MonoBehaviour
{
    public int width;
    public int height;

    public void SetWidth(int newWidth)
    {
        width = newWidth;
        //Debug.Log("1600/900");
    }

    public void SetHeight(int newHeight)
    {
        height = newHeight;
        //Debug.Log("1920/1080");
    }

    public void SetRes()
    {
        Screen.SetResolution(width, height, false);
    }
}
