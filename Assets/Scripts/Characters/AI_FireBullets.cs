using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_FireBullets : MonoBehaviour
{
    [Header("Bullet Prefab")]
    public GameObject bullet;
    public Transform bulletpoint;
    [Tooltip("발사 시간")]
    public float interval;
    [Tooltip("발사체 갯수")]
    public int count;
    [Tooltip("발사 각도")]
    public float angle;
    [Header("발사 모드")]
    public bool Count1Slow;
    public bool Count1Fast;
    public bool Count3Slow;
    public bool Count3Fast;
    public bool Count5Slow;
    public bool Count5Fast;

    private bool m_bIsFire = false;
    private bool m_bIsSpecial = false;
    private float m_DeployTime = 0;
    private float m_DeployCheckTime = 0;

    public void SetAttackType(int type)
    {
        switch (type)
        {
            case 0:
                Count1Slow = true;
                break;
            case 1:
                Count1Fast = true;
                break;
            case 2:
                Count3Slow = true;
                break;
            case 3:
                Count3Fast = true;
                break;
            case 4:
                Count5Slow = true;
                break;
            case 5:
                Count5Fast = true;
                break;
            default:
                break;
        }
    }

    private void Update()
    {
        if(!m_bIsFire)
        {
            if (Count1Slow)
            {
                SetFireType(0.3f, 1, 0);
                m_bIsFire = true;
                StartCoroutine(FireLoop());
            }
            else if (Count1Fast)
            {
                SetFireType(0.1f, 1, 0);
                m_bIsFire = true;
                StartCoroutine(FireLoop());
            }
            else if (Count3Slow)
            {
                SetFireType(0.4f, 3, 20);
                m_bIsFire = true;
                StartCoroutine(FireLoop());
            }
            else if (Count3Fast)
            {
                SetFireType(0.1f, 3, 20);
                m_bIsFire = true;
                StartCoroutine(FireLoop());
            }
            else if (Count5Slow)
            {
                SetFireType(0.5f, 5, 30);
                m_bIsFire = true;
                StartCoroutine(FireLoop());
            }
            else if (Count5Fast)
            {
                SetFireType(0.1f, 5, 30);
                m_bIsFire = true;
                StartCoroutine(FireLoop());
            }
        }
    }

    void SetFireType(float i, int c, float a)
    {
        interval = i;
        count = c;
        angle = a;
    }

    IEnumerator FireLoop()
    {
        while (IsValidCondition())
        {
            float gap = count > 1 ? angle / (count - 1) : 0;
            float startAngle = transform.eulerAngles.z / 2.0f - angle / 2.0f;
            for (int i = 0; i < count; i++)
            {
                float theta = startAngle + gap * i;
                theta *= Mathf.Deg2Rad;
                GameObject go = Instantiate(bullet, bulletpoint.position, Quaternion.identity, GameObject.Find("CV_Field").transform);
                go.GetComponent<Bullet>().Name = "Enemy_Bullet";
                Vector3 TempDir = bulletpoint.position - transform.position;
                TempDir = TempDir.normalized;
                //go.GetComponent<Bullet>().SetNormalizedDirection(TempDir);
                Vector3 TempQuat = new Vector3(0, 0, transform.eulerAngles.z + 90);
                //go.GetComponent<Bullet>().SetNormalizedDirection(new Vector3(Mathf.Cos(theta), Mathf.Sin(theta), 0));
                Debug.Log(Mathf.Cos(theta) + ", " + Mathf.Sin(theta) + ", ");
                go.GetComponent<Bullet>().SetNormalizedDirection(new Vector3(Mathf.Cos(theta) + TempDir.x, Mathf.Sin(theta) + TempDir.y, 0));
                go.GetComponent<Bullet>().SetRot(TempQuat);
            }

            yield return new WaitForSeconds(interval);
        }
    }

    bool IsValidCondition()
    {
        return interval > 0 && count > 0;
    }
}
