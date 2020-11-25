using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnector : MonoBehaviour
{
    public string MagnectorName;

    private GameObject MagnetOwner;
    private Rigidbody2D m_Rigid;
    private Vector3 velocity;

    void Start()
    {
        if(MagnectorName != "")
        {
            MagnetOwner = GameObject.Find(MagnectorName);
            Destroy(gameObject, 30);
        }
    }

    void Update()
    {
        if (MagnectorName != "")
        {
            if(MagnetOwner == null)
            {
                return;
            }
            // p1은 플레이어의 위치, p2는 코인 위치
            Vector3 force = MagnetOwner.transform.position - gameObject.transform.position;
            float r = force.magnitude;

            force.Normalize();
            force *= 200; // 적당한 힘(자력의 세기)
            force /= r * r;  // 거리 제곱에 반비례

            Vector3 acceleration = force / 1; // m은 코인의 질량(적당한값)

            velocity += acceleration * Time.deltaTime; // 속도 적분
            transform.localPosition += velocity * Time.deltaTime; // 거리 적분
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(MagnectorName != "")
        {
            if(collision.gameObject.GetComponent<PlayerController>() != null)
            {
                var TempMgr = GameObject.Find("GameManager");
                TempMgr.GetComponent<GameMgr>().AddCoin();
                Destroy(gameObject);
            }
        }
    }
}
