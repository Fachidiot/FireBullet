using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemAutoSet : MonoBehaviour
{
    [Header("Desktop")]
    public GameObject Desktop1;
    public GameObject Desktop2;
    [Header("Mobile")]
    public GameObject Mobile1;
    public GameObject Mobile2;
    [Header("Player")]
    public GameObject Controller;

    private void Awake()
    {
        if (SystemInfo.deviceType == DeviceType.Desktop)
        {
            if(Desktop1 != null)
                Desktop1.SetActive(true);
            if (Desktop2 != null)
                Desktop2.SetActive(true);
            if (Controller != null)
                Controller.gameObject.SendMessage("Desktop", SendMessageOptions.DontRequireReceiver);
        }
        else if (SystemInfo.deviceType == DeviceType.Handheld)
        {
            if (Mobile1 != null)
                Mobile1.SetActive(true);
            if (Mobile2 != null)
                Mobile2.SetActive(true);
            if (Controller != null)
                Controller.gameObject.SendMessage("Mobile", SendMessageOptions.DontRequireReceiver);
        }
        else
        {

        }
    }
}
