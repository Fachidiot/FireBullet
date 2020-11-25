using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Popup : MonoBehaviour
{
    public GameObject OptionPrefab;

    public void Close()
    {
        Destroy(gameObject);
    }

    public void Option()
    {
        if(GameObject.Find("PN_Option") == null)
        {
            var temp = Instantiate(OptionPrefab, GameObject.Find("CV_UI").transform);
            temp.name = "PN_Option";
        }
    }
}
