using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScripts : MonoBehaviour
{
    public GameObject Player;

    private void Update()
    {
        if(Player != null)
        {
            if(Player.transform.localPosition.x > 1100)
            {
                Player.transform.localPosition = new Vector3(-1100, Player.transform.localPosition.y, Player.transform.localPosition.z);
            }
            Player.transform.localPosition += Vector3.right * 5;
        }
    }
}
