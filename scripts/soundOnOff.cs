using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundOnOff : MonoBehaviour
{
    public GameObject soundOffImage;
    private bool isSoundOn = true;

    private void Start()
    {
        soundOffImage.SetActive(!isSoundOn);
    }

    public void ToggleSound()
    {
        isSoundOn = !isSoundOn;
        soundOffImage.SetActive(!isSoundOn);
    }
}
