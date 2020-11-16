using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector3 velo;
    public float ttl;
    public float speed;

    void Start()
    {
        Destroy(gameObject, ttl);    
    }

    void Update()
    {
        transform.position += velo * Time.deltaTime;
    }

    public void SetNormalizedDirection(Vector3 dir)
    {
        velo = dir * speed;
    }
}
