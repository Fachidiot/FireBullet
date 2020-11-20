using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransformViewer : MonoBehaviour
{
    public Text txtMx;
    public GameObject Target;

    void Update()
    {
        Matrix4x4 mx = Target.transform.localToWorldMatrix;
        string text;
        text = string.Format("{0:0.00} {1:0.00} {2:0.00} {3:0.00} \n", mx.m00, mx.m01, mx.m02, mx.m03);
        text += string.Format("{0:0.00} {1:0.00} {2:0.00} {3:0.00} \n", mx.m10, mx.m11, mx.m12, mx.m13);
        text += string.Format("{0:0.00} {1:0.00} {2:0.00} {3:0.00} \n", mx.m20, mx.m21, mx.m22, mx.m23);
        text += string.Format("{0:0.00} {1:0.00} {2:0.00} {3:0.00} \n", mx.m30, mx.m31, mx.m32, mx.m33);
        txtMx.text = text;
    }
}
