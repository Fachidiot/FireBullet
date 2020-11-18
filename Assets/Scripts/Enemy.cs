using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject Spark;

    private Vector3 temp = new Vector3(0, 0, 1);

    private void Update()
    {
        transform.Rotate(temp);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Bullet bullet = collision.collider.gameObject.GetComponent<Bullet>();
        if (bullet != null)
        {
            if (Vector3.Dot(bullet.velo.normalized, transform.up) < 0)
            {
                if (Spark != null)
                {
                    Instantiate(Spark, gameObject.transform);
                }
            }
        }
    }
}
