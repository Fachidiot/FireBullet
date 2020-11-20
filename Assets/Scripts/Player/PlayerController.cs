using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("이동 속도")]
    public float Speed;
    [Header("이동 범위")]
    public Vector2Int Max = new Vector2Int(500, 490);
    public Vector2Int Min = new Vector2Int(-900, -490);
    [Header("Popup")]
    public GameObject Popup;

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
            MoveD();
        }


        // UI
        if (Input.GetKey(KeyCode.Escape))
        {
            Popup.SetActive(true);
        }
    }

    void MoveM()
    {
        if (Input.GetMouseButton(0))
        {
            Touch touch = Input.GetTouch(0);
            switch(touch.phase)
            {
            case TouchPhase.Began:
                Debug.Log("Touch Began");
                m_Rigid.AddForce(CalMobileForce(touch.position) * Speed);
                break;
            case TouchPhase.Moved:
                Debug.Log("Touch Moved");
                m_Rigid.AddForce(CalMobileForce(touch.position) * Speed);
                break;
            case TouchPhase.Ended:
                Debug.Log("Touch Ended");
                break;
            }

        }
    }

    Vector3 CalMobileForce(Vector3 pos)
    {
        Vector3 Temp = new Vector3();
        if(transform.localPosition.y > pos.y)
        {
            Temp += Vector3.up;
        }
        else
        {
            Temp += Vector3.down;
        }
        if(transform.localPosition.x > pos.x)
        {
            Temp += Vector3.left;
        }
        else
        {
            Temp += Vector3.right;
        }

        return Temp;
    }

    void MoveD()
    {
        if (transform.localPosition.y > Max.y)
        {
            m_Rigid.velocity = Vector3.zero;
            m_Rigid.AddForce(Vector2.down * Speed);
        }
        else if (Input.GetKey(KeyCode.W))
        {
            m_Rigid.AddForce(Vector2.up * Speed);
        }

        if (transform.localPosition.y < Min.y)
        {
            m_Rigid.velocity = Vector3.zero;
            m_Rigid.AddForce(Vector2.up * Speed);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            m_Rigid.AddForce(Vector2.down * Speed);
        }
        if (transform.localPosition.x < Min.x)
        {
            m_Rigid.velocity = Vector3.zero;
            m_Rigid.AddForce(Vector2.right * Speed);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            m_Rigid.AddForce(Vector2.left * Speed);
        }
        if (transform.localPosition.x > Max.x)
        {
            m_Rigid.velocity = Vector3.zero;
            m_Rigid.AddForce(Vector2.left * Speed);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            m_Rigid.AddForce(Vector2.right * Speed);
        }
    }
}
