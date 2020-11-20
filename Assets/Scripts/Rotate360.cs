using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate360 : MonoBehaviour
{
    private Vector3 temp = new Vector3(0, 0, 1);
    void Update()
    {
        transform.Rotate(temp);
    }
}
