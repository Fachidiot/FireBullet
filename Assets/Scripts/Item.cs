using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [Tooltip("1. Ammo\n2. Stats")]
    public int ItemType;
    [Tooltip("1. Fast\n2. Triple\n3. Double\n4. Shotgun")]
    public int SpecialType;
    [Tooltip("1. Shield\n2. Health")]
    public int StatsType;

    void Update()
    {
        if(transform.localPosition.x <= -1100)
        {
            Destroy(gameObject);
        }
        gameObject.transform.localPosition += Vector3.left * 3f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<PlayerController>() != null)
        {
            if (ItemType == 1)
            {
                collision.gameObject.GetComponent<FireBullets>().SendMessage("SpecialAttack", SpecialType);
            }
            else if (ItemType == 2)
            {
                collision.gameObject.GetComponent<PlayerController>().SendMessage("Recover", StatsType);
            }
            Destroy(gameObject);
        }
    }
}
