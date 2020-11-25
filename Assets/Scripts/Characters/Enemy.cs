using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public GameObject Spark;
    public GameObject Smoke;
    public GameObject Coin;
    public Vector2Int Max;
    public Vector2Int Min;
    [Header("HP")]
    public float MaxHP;
    public Slider HPBar;
    [Header("Rotate Image")]
    public GameObject RotateComponent;
    [Header("Shield")]
    public bool Shield;
    [Header("Rot")]
    public bool IsRot;

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
        if(m_CurrentHP <= 0)
        {
            Instantiate(Smoke, gameObject.transform.position, gameObject.transform.localRotation, GameObject.Find("CV_Field").transform);
            Instantiate(Coin, gameObject.transform.position, gameObject.transform.localRotation, GameObject.Find("CV_Field").transform);
            Destroy(gameObject);
            return;
        }
        m_Rigid.AddForce(Vector2.left * 20f);
        if(transform.localPosition.x < Min.x)
        {
            Destroy(gameObject);
            return;
        }
        ForceToStay();
        HPBar.value = m_CurrentHP;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "Player" || collision.gameObject.name == "Enemy")
        {
            Vector3 Velo = collision.gameObject.GetComponent<Rigidbody2D>().velocity;
            if (Shield)
            {
                Vector3 Temp = new Vector3(Velo.x, 0, collision.transform.eulerAngles.z);
                if (Vector3.Dot(Temp, RotateComponent.transform.up) < 0)
                {
                    if (Velo.x < 0.5f)
                    {
                        return;
                    }
                    else if (Velo.x < 1)
                    {
                        m_CurrentHP -= 10f;
                    }
                    else if (Velo.x < 2)
                    {
                        m_CurrentHP -= 20f;
                    }
                    if (Spark != null)
                    {
                        Instantiate(Spark, gameObject.transform);
                    }
                }
            }
            else
            {

                if (Velo.x < 0.5f)
                {
                    return;
                }
                else if (Velo.x < 1)
                {
                    m_CurrentHP -= 10f;
                }
                else if (Velo.x < 2)
                {
                    m_CurrentHP -= 20f;
                }
                if (Spark != null)
                {
                    Instantiate(Spark, gameObject.transform);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Bullet bullet = collision.gameObject.GetComponent<Bullet>();
        if (bullet != null)
        {
            if(Shield)
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
            else
            {
                if(Spark != null)
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
