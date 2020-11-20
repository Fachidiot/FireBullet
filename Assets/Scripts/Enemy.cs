using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public GameObject Spark;
    public Vector2Int Max;
    public Vector2Int Min;
    [Header("HP")]
    public float MaxHP;
    public Slider HPBar;
    [Header("Rotate Image")]
    public GameObject RotateComponent;

    private float m_Damage;
    private float m_CurrentHP;
    private Rigidbody2D m_Rigid;

    private void Start()
    {
        m_Rigid = GetComponent<Rigidbody2D>();
        m_CurrentHP = MaxHP;
        HPBar.maxValue = MaxHP;
    }

    private void Update()
    {
        ForceToStay();
        HPBar.value = m_CurrentHP;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Bullet bullet = collision.collider.gameObject.GetComponent<Bullet>();
        if (bullet != null)
        {
            if (Vector3.Dot(bullet.velo.normalized, RotateComponent.transform.up) < 0)
            {
                if (Spark != null)
                {
                    Instantiate(Spark, gameObject.transform);
                }

                m_CurrentHP -= m_Damage;
            }
        }
    }

    void ForceToStay()
    {
        if (transform.localPosition.y > Max.y)
        {
            m_Rigid.AddForce(Vector2.down * 2);
        }
        else
        {
            m_Rigid.velocity = Vector3.zero;
        }
        if (transform.localPosition.y < Min.y)
        {
            m_Rigid.AddForce(Vector2.up * 2);
        }
        else
        {
            m_Rigid.velocity = Vector3.zero;
        }
        if (transform.localPosition.x < Min.x)
        {
            m_Rigid.AddForce(Vector2.right * 2);
        }
        else
        {
            m_Rigid.velocity = Vector3.zero;
        }
        if (transform.localPosition.x > Max.x)
        {
            m_Rigid.AddForce(Vector2.left * 2);
        }
        else
        {
            m_Rigid.velocity = Vector3.zero;
        }
    }

    public void SetDamage(float amount)
    {
        m_Damage = amount;
    }
}
