using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMgr : MonoBehaviour
{
    public GameObject GameOver;
    public GameObject MobSpawner;
    public GameObject PlayerPrefab;

    private GameObject Player;
    private int m_iCoinCount;
    private bool m_IsSave;
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
        float x = Screen.width;
        float y = Screen.height;
        var tempint = y / 1080;
        GameObject.Find("Field_Resol").transform.localPosition = new Vector3(-x / 2 / tempint, -1080 / 2);
        var temp = Instantiate(PlayerPrefab, GameObject.Find("Field_Resol").transform);
        temp.transform.localPosition = new Vector3(-700, 0, 0);
        temp.name = "Player";
        if(m_Desktop)
        {
            temp.GetComponent<PlayerController>().Desktop();
        }
        else if (m_Mobile)
        {
            temp.GetComponent<PlayerController>().Mobile();
        }
        Player = temp;
    }

    private bool m_bFire;
    public void MobileFire()
    {
        if(!m_bFire)
        {
            Player.GetComponent<FireBullets>().StartCoroutine("FireLoop");
            m_bFire = true;
        }
        else
        {
            Player.GetComponent<FireBullets>().StopAllCoroutines();
            m_bFire = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Player == null && !m_IsSave)
        {
            GameOver.SetActive(true);
            MobSpawner.GetComponent<MobSpawner>().StopAllCoroutines();
            if(!PlayerPrefs.HasKey("Coin"))
            {
                PlayerPrefs.SetInt("Coin", m_iCoinCount);
            }
            var TempSaver = PlayerPrefs.GetInt("Coin");
            TempSaver += m_iCoinCount;
            m_IsSave = true;
            PlayerPrefs.SetInt("Coin", TempSaver);
        }
    }

    public void reset()
    {
        var temp = Instantiate(PlayerPrefab, GameObject.Find("Field_Resol").transform);
        temp.transform.localPosition = new Vector3(-700, 0, 0);
        m_IsSave = false;
        temp.name = "Player";
        if (m_Desktop)
        {
            temp.GetComponent<PlayerController>().Desktop();
        }
        else if (m_Mobile)
        {
            temp.GetComponent<PlayerController>().Mobile();
        }
        Player = temp;
    }

    public void AddCoin()
    {
        m_iCoinCount++;
    }
}
