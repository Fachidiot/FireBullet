using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector3 velo;
    public float Damage;
    public float ttl;
    public float speed;
    public string Name;

    void Start()
    {
        Destroy(gameObject, ttl);    
    }

    void Update()
    {
        transform.position += velo * Time.deltaTime;
    }

    public void SetDamage(float amount)
    {
        Damage = amount;
    }

    public void SetNormalizedDirection(Vector3 dir)
    {
        velo = dir * speed;
    }

    public void SetRot(Vector3 Rot)
    {
        gameObject.transform.eulerAngles = Rot;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var str_Temp = Name.Split('_');
        if (str_Temp[0] == collision.gameObject.name)
        {
            return;
        }
        collision.gameObject.SendMessage("SetDamage", Damage);
        Destroy(gameObject);
    }
}
