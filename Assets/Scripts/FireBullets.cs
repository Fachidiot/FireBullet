using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBullets : MonoBehaviour
{
    public GameObject bullet;
    public Transform bulletpoint;
    public float interval;
    public int count;
    public float angle;

    void Start()
    {
        StartCoroutine("FireLoop");
    }

    IEnumerator FireLoop()
    {
        while (IsValidCondition())
        {
            float gap = count > 1 ? angle / (count - 1) : 0;
            float startAngle = -angle / 2.0f;
            for (int i = 0; i < count; i++)
            {
                float theta = startAngle + gap * i;
                theta *= Mathf.Deg2Rad;
                GameObject go = Instantiate(bullet, bulletpoint.position, Quaternion.identity);
                go.GetComponent<Bullet>().SetNormalizedDirection(new Vector3(Mathf.Cos(theta), Mathf.Sin(theta), 0));
            }
            yield return new WaitForSeconds(interval);
        }
    }

    bool IsValidCondition()
    {
        return interval > 0 && count > 0;
    }
}
