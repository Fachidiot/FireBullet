using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSpawner : MonoBehaviour
{
    [Header("Abble Area")]
    public float MaxY;
    public float MinY;
    [Header("Rand Time")]
    public float MaxRandTime;
    public float MinRandTime;
    [Header("Type")]
    public List<int> MobTypeList;
    [Header("MobList")]
    public List<GameObject> MobList;

    private List<GameObject> m_SpawnList = new List<GameObject>();
    private float m_RandTime;
    private Vector2 m_RandPos;
    private int m_RandIndex;
    private int m_RandType;

    private void Start()
    {
        m_RandPos.x = 1100;
        StartCoroutine(RandSpawn());
    }

    IEnumerator RandSpawn()
    {
        while (true)
        {
            m_RandPos.y = Random.Range(MinY, MaxY);
            m_RandTime = Random.Range(MinRandTime, MaxRandTime);
            m_RandIndex = Random.Range(0, MobList.Count);
            m_RandType = Random.Range(0, MobTypeList[m_RandIndex]);
            var temp = Instantiate(MobList[m_RandIndex], gameObject.transform);
            SetType(m_RandType, temp);
            temp.name = "Enemy";
            temp.transform.localPosition = m_RandPos;
            m_SpawnList.Add(temp);
            yield return new WaitForSeconds(m_RandTime);
        }
    }

    void SetType(int type, GameObject Obj)
    {
        Obj.GetComponentInChildren<AI_FireBullets>().SetAttackType(type);
    }
}
