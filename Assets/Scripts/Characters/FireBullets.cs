using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBullets : MonoBehaviour
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

    private bool m_bIsFire = false;
    private float m_CoolTime;
    private bool m_bIsSpecial = false;
    private float m_DeployTime = 0;
    private float m_DeployCheckTime = 0;

    private void Start()
    {
        SetFireType(0.3f, 1, 0);
    }

    private void Update()
    {
        if(m_bIsSpecial)
        {
            if(m_DeployTime - m_DeployCheckTime <= 0)
            {
                SetFireType(0.3f, 1, 0);
            }
            m_DeployCheckTime += Time.deltaTime;
        }

        if(Input.GetKey(KeyCode.Space) && !m_bIsFire)
        {
            StartCoroutine(FireLoop());
        }
        else if (Input.GetKeyUp(KeyCode.Space) && m_bIsFire)
        {
            m_CoolTime = 0;
            StopAllCoroutines();
            StartCoroutine(CoolTime());
        }
    }

    void SetFireType(float i, int c, float a)
    {
        interval = i;
        count = c;
        angle = a;
    }

    public void SpecialAttack(int i)
    {
        switch (i)
        {
            case 1:
                m_bIsSpecial = true;
                m_DeployCheckTime = 0f;
                m_DeployTime = 15f;
                SetFireType(0.1f, 1, 0);
                break;
            case 2:
                m_bIsSpecial = true;
                m_DeployCheckTime = 0f;
                m_DeployTime = 30f;
                SetFireType(0.2f, 3, 10);
                break;
            case 3:
                m_bIsSpecial = true;
                m_DeployCheckTime = 0f;
                m_DeployTime = 20f;
                SetFireType(0.1f, 2, 15);
                break;
            case 4:
                m_bIsSpecial = true;
                m_DeployCheckTime = 0f;
                m_DeployTime = 10f;
                SetFireType(0.5f, 5, 30);
                break;
            default:
                break;
        }
    }

    public void FireOnOff()
    {
        if(!m_bIsFire)
        {
            m_bIsFire = true;
            StartCoroutine(FireLoop());
        }
        else
        {
            m_bIsFire = false;
            StopAllCoroutines();
        }
    }

    IEnumerator FireLoop()
    {
        m_bIsFire = true;
        while (IsValidCondition())
        {
            float gap = count > 1 ? angle / (count - 1) : 0;
            float startAngle = -angle / 2.0f;
            for (int i = 0; i < count; i++)
            {
                float theta = startAngle + gap * i;
                theta *= Mathf.Deg2Rad;
                GameObject go = Instantiate(bullet, bulletpoint.position, Quaternion.identity, GameObject.Find("CV_Field").transform);
                go.GetComponent<Bullet>().Name = "Player_Bullet";
                go.GetComponent<Bullet>().SetNormalizedDirection(new Vector3(Mathf.Cos(theta), Mathf.Sin(theta), 0));
            }

            yield return new WaitForSeconds(interval);
        }
    }

    IEnumerator CoolTime()
    {
        while (interval > m_CoolTime)
        {
            m_CoolTime += Time.deltaTime;
            if(interval < m_CoolTime)
            {
                m_bIsFire = false;
            }
            yield return null;
        }
    }

    bool IsValidCondition()
    {
        return interval > 0 && count > 0;
    }
}
