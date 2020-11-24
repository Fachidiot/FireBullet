using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("이동 속도")]
    public float Speed;
    [Header("HP")]
    public float MaxHP;
    public Slider HPBar;
    [Header("Armor")]
    public float MaxArmor;
    public Slider ArmorBar;
    [Header("이동 범위")]
    public Vector2Int Max = new Vector2Int(500, 490);
    public Vector2Int Min = new Vector2Int(-900, -490);
    [Header("Popup")]
    public GameObject Popup;
    [Header("Spark")]
    public GameObject Spark;
    [Header("Shield")]
    public GameObject Shield;

    private Vector2 m_PrevPos = Vector2.zero;
    private float m_CurrentHP;
    private float m_CurrentArmor;
    private float m_Damage;
    private Rigidbody2D m_Rigid;
    private bool m_Desktop = false;
    public void Desktop()
    {
        m_Desktop = true;
    }
    private bool m_Mobile = false;
    public void Mobile()
    {
        m_Mobile = true;
    }

    void Start()
    {
        m_CurrentArmor = MaxArmor;
        m_CurrentHP = MaxHP;
        HPBar.maxValue = MaxHP;
        ArmorBar.maxValue = MaxArmor;
        m_Rigid = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        // Move
        if (m_Mobile)
        {
            MoveM();
        }
        if (m_Desktop)
        {
            MoveM();
            MoveD();
        }


        // UI
        if (Input.GetKey(KeyCode.Escape))
        {
            Popup.SetActive(true);
        }
    }

    void Update()
    {
        if (m_CurrentHP <= 0)
        {
            Destroy(gameObject);
        }
        ForceToStay();
        HPBar.value = m_CurrentHP;
        ArmorBar.value = m_CurrentArmor;
        if(m_CurrentArmor <= 0 && Shield.activeSelf)
        {
            Shield.SetActive(false);
        }
    }

    void MoveM()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 CurrentPos = new Vector3(transform.localPosition.x + 960, transform.localPosition.y + 540, 0);
            Vector2 CalPos = Input.mousePosition - CurrentPos;
            CalPos = CalPos.normalized;

            if (CalPos.x > 0)
            {
                if (m_PrevPos.x < 0)
                {
                    CalPos.x = 0;
                    if(m_Rigid.velocity.x + 0.1 < 0)
                    {
                        m_Rigid.velocity += new Vector2(0.1f, 0);
                    }
                }
            }
            else
            {
                if (m_PrevPos.x > 0)
                {
                    CalPos.x = 0;
                    if(m_Rigid.velocity.x - 0.1 > 0)
                    {
                        m_Rigid.velocity -= new Vector2(0.1f, 0);
                    }
                }
            }
            if (CalPos.y > 0)
            {
                if(m_PrevPos.y < 0)
                {
                    CalPos.y = 0;
                    if (m_Rigid.velocity.y + 0.1 < 0)
                    {
                        m_Rigid.velocity += new Vector2(0, 0.1f);
                    }
                }
            }
            else
            {
                if(m_PrevPos.y > 0)
                {
                    CalPos.y = 0;
                    if (m_Rigid.velocity.y - 0.1 > 0)
                    {
                        m_Rigid.velocity -= new Vector2(0, 0.1f);
                    }
                }
            }

            m_Rigid.AddForce(CalPos * Speed);
            m_PrevPos = CalPos;
        }
    }

    void ForceToStay()
    {
        if (transform.localPosition.y > Max.y)
        {
            m_Rigid.velocity = new Vector2(m_Rigid.velocity.x, 0);
            transform.localPosition = new Vector3(transform.localPosition.x, Max.y, transform.localPosition.z);
        }
        if (transform.localPosition.y < Min.y)
        {
            m_Rigid.velocity = new Vector2(m_Rigid.velocity.x, 0);
            transform.localPosition = new Vector3(transform.localPosition.x, Min.y, transform.localPosition.z);
        }
        if (transform.localPosition.x < Min.x)
        {
            m_Rigid.velocity = new Vector2(m_Rigid.velocity.y, 0);
            transform.localPosition = new Vector3(Min.x, transform.localPosition.y, transform.localPosition.z);
        }
        if (transform.localPosition.x > Max.x)
        {
            m_Rigid.velocity = new Vector2(m_Rigid.velocity.y, 0);
            transform.localPosition = new Vector3(Max.x, transform.localPosition.y, transform.localPosition.z);
        }
    }
    
    void MoveD()
    {
        if (Input.GetKey(KeyCode.W))
        {
            m_Rigid.AddForce(Vector2.up * Speed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            m_Rigid.AddForce(Vector2.down * Speed);
        }
        if (Input.GetKey(KeyCode.A))
        {
            m_Rigid.AddForce(Vector2.left * Speed);
        }
        if (Input.GetKey(KeyCode.D))
        {
            m_Rigid.AddForce(Vector2.right * Speed);
        }
    }

    void SetDamage(float amount)
    {
        m_Damage = amount;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Enemy")
        {
            Vector3 Velo = collision.gameObject.GetComponent<Rigidbody2D>().velocity;
            if (m_CurrentArmor <= 0)
            {
                bool ICD = true;
                if (Velo.x > 0)
                {
                    if (Velo.x < 0.2f)
                    {
                        ICD = false;
                    }
                    else if (Velo.x < 1)
                    {
                        Debug.Log("10 Damaged to HP");
                        m_CurrentHP -= 10f;
                    }
                    else if (Velo.x < 2)
                    {
                        Debug.Log("20 Damaged to HP");
                        m_CurrentHP -= 20f;
                    }
                    else
                    {
                        Debug.Log("25 Damaged to HP");
                        m_CurrentHP -= 25;
                    }
                    if (Spark != null && ICD)
                    {
                        Instantiate(Spark, gameObject.transform);
                        return;
                    }
                }
                else
                {
                    if (-Velo.x < 0.2f)
                    {
                        ICD = false;
                    }
                    else if (-Velo.x < 1)
                    {
                        Debug.Log("10 Damaged to HP");
                        m_CurrentHP -= 10f;
                    }
                    else if (-Velo.x < 2)
                    {
                        Debug.Log("20 Damaged to HP");
                        m_CurrentHP -= 20f;
                    }
                    else
                    {
                        Debug.Log("25 Damaged to HP");
                        m_CurrentHP -= 25;
                    }
                    if (Spark != null && ICD)
                    {
                        Instantiate(Spark, gameObject.transform);
                        return;
                    }
                }
                if (Velo.y > 0)
                {
                    if (Velo.y < 0.2f)
                    {
                        Debug.Log("None Damaged to HP");
                        return;
                    }
                    else if (Velo.y < 1)
                    {
                        Debug.Log("10 Damaged to HP");
                        m_CurrentHP -= 10f;
                    }
                    else if (Velo.y < 2)
                    {
                        Debug.Log("20 Damaged to HP");
                        m_CurrentHP -= 20f;
                    }
                    else
                    {
                        Debug.Log("25 Damaged to HP");
                        m_CurrentHP -= 25;
                    }
                    if (Spark != null)
                    {
                        Instantiate(Spark, gameObject.transform);
                    }
                }
                else
                {
                    if (-Velo.y < 0.2f)
                    {
                        Debug.Log("None Damaged to HP");
                        return;
                    }
                    else if (-Velo.y < 1)
                    {
                        Debug.Log("10 Damaged to HP");
                        m_CurrentHP -= 10f;
                    }
                    else if (-Velo.y < 2)
                    {
                        Debug.Log("20 Damaged to HP");
                        m_CurrentHP -= 20f;
                    }
                    else
                    {
                        Debug.Log("25 Damaged to HP");
                        m_CurrentHP -= 25;
                    }
                    if (Spark != null)
                    {
                        Instantiate(Spark, gameObject.transform);
                        return;
                    }
                }
            }
            else
            {
                bool ICD = true;
                if (Velo.x > 0)
                {
                    if (Velo.x < 0.2f)
                    {
                        ICD = false;
                    }
                    else if (Velo.x < 1)
                    {
                        Debug.Log("10 Damaged to HP");
                        m_CurrentArmor -= 10f;
                    }
                    else if (Velo.x < 2)
                    {
                        Debug.Log("20 Damaged to HP");
                        m_CurrentArmor -= 20f;
                    }
                    else
                    {
                        Debug.Log("25 Damaged to HP");
                        m_CurrentArmor -= 25;
                    }
                    if (Spark != null && ICD)
                    {
                        Instantiate(Spark, gameObject.transform);
                        return;
                    }
                }
                else
                {
                    if (-Velo.x < 0.2f)
                    {
                        ICD = false;
                    }
                    else if (-Velo.x < 1)
                    {
                        Debug.Log("10 Damaged to HP");
                        m_CurrentArmor -= 10f;
                    }
                    else if (-Velo.x < 2)
                    {
                        Debug.Log("20 Damaged to HP");
                        m_CurrentArmor -= 20f;
                    }
                    else
                    {
                        Debug.Log("25 Damaged to HP");
                        m_CurrentArmor -= 25;
                    }
                    if (Spark != null && ICD)
                    {
                        Instantiate(Spark, gameObject.transform);
                        return;
                    }
                }
                if (Velo.y > 0)
                {
                    if (Velo.y < 0.2f)
                    {
                        Debug.Log("None Damaged to HP");
                        return;
                    }
                    else if (Velo.y < 1)
                    {
                        Debug.Log("10 Damaged to HP");
                        m_CurrentArmor -= 10f;
                    }
                    else if (Velo.y < 2)
                    {
                        Debug.Log("20 Damaged to HP");
                        m_CurrentArmor -= 20f;
                    }
                    else
                    {
                        Debug.Log("25 Damaged to HP");
                        m_CurrentArmor -= 25;
                    }
                    if (Spark != null)
                    {
                        Instantiate(Spark, gameObject.transform);
                    }
                }
                else
                {
                    if (-Velo.y < 0.2f)
                    {
                        Debug.Log("None Damaged to HP");
                        return;
                    }
                    else if (-Velo.y < 1)
                    {
                        Debug.Log("10 Damaged to HP");
                        m_CurrentArmor -= 10f;
                    }
                    else if (-Velo.y < 2)
                    {
                        Debug.Log("20 Damaged to HP");
                        m_CurrentArmor -= 20f;
                    }
                    else
                    {
                        Debug.Log("25 Damaged to HP");
                        m_CurrentArmor -= 25;
                    }
                    if (Spark != null)
                    {
                        Instantiate(Spark, gameObject.transform);
                        return;
                    }
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var str_Temp = collision.gameObject.GetComponent<Bullet>().Name.Split('_');
        if (name == str_Temp[0])
        {
            return;
        }

        if(m_CurrentArmor <= 0)
        {
            m_CurrentHP -= m_Damage;
        }
        else
        {
            m_CurrentArmor -= m_Damage;
        }

    }
}
