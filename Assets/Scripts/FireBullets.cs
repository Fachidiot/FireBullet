using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBullets : MonoBehaviour
{
    [Header("Bullet Prefab")]
    public GameObject bullet;
    public Transform bulletpoint;
    [Tooltip("이동 속도")]
    public float Speed;
    [Tooltip("발사 시간")]
    public float interval;
    [Tooltip("발사체 갯수")]
    public int count;
    [Tooltip("발사 각도")]
    public float angle;
    [Header("이동 범위")]
    public Vector2Int Max;
    public Vector2Int Min;

    private bool m_bIsFire = false;
    private float m_Time;
    private bool m_bIsSpecial = false;
    private float m_DeployTime = 0;
    private float m_DeployCheckTime = 0;
    private Rigidbody2D m_Rigid;

    private void Start()
    {
        SetFireType(0.3f, 1, 0);
        m_Rigid = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if(m_bIsSpecial)
        {
            if(m_DeployTime - m_DeployCheckTime <= 0)
            {
                SpecialAttack(1);
            }
            m_DeployCheckTime += Time.deltaTime;
        }

        if (SystemInfo.deviceType == DeviceType.Handheld)
        {
            MoveM();
        }
        if (SystemInfo.deviceType == DeviceType.Desktop)
        {
            MoveD();
        }

        // 이동 제한
        if (transform.localPosition.x < Min.x)
        {
            Vector3 temp = transform.localPosition;
            temp.x = Min.x;
            transform.localPosition = temp;
        }
        if (transform.localPosition.x > Max.x)
        {
            Vector3 temp = transform.localPosition;
            temp.x = Max.x;
            transform.localPosition = temp;
        }
        if (transform.localPosition.y < Min.y)
        {
            Vector3 temp = transform.localPosition;
            temp.y = Min.y;
            transform.localPosition = temp;
        }
        if (transform.localPosition.y > Max.y)
        {
            Vector3 temp = transform.localPosition;
            temp.y = Max.y;
            transform.localPosition = temp;
        }

        // 사용자 편의 발사 버튼
        //if(Input.GetMouseButtonDown(0) && !m_bIsFire)
        //{
        //    m_bIsFire = true;
        //    SetFireType(0.3f, 1, 0);
        //    StartCoroutine(FireLoop());
        //}
        //else
        //{
        //    if (m_Time >= interval && m_bIsFire)
        //    {
        //        m_Time = 0;
        //        m_bIsFire = false;
        //    }
        //    m_Time += Time.deltaTime;
        //}


        //if (Input.GetMouseButtonUp(0))
        //{
        //    StopAllCoroutines();
        //}
    }

    void MoveM()
    {
        
    }

    void MoveD()
    {
        if(Input.GetKey(KeyCode.W))
        {
            m_Rigid.AddForce(Vector2.up * Speed);
        }
        else if(Input.GetKey(KeyCode.S))
        {
            m_Rigid.AddForce(Vector2.down * Speed);
        }
        if(Input.GetKey(KeyCode.A))
        {
            m_Rigid.AddForce(Vector2.left * Speed);
        }
        else if(Input.GetKey(KeyCode.D))
        {
            m_Rigid.AddForce(Vector2.right * Speed);
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
                m_bIsSpecial = false;
                SetFireType(0.3f, 1, 0);
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
        while (IsValidCondition())
        {
            float gap = count > 1 ? angle / (count - 1) : 0;
            float startAngle = -angle / 2.0f;
            for (int i = 0; i < count; i++)
            {
                float theta = startAngle + gap * i;
                theta *= Mathf.Deg2Rad;
                GameObject go = Instantiate(bullet, bulletpoint.position, Quaternion.identity, bulletpoint);
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
