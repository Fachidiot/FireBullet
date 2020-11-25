using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [Header("Abble Area")]
    public float MaxY;
    public float MinY;
    [Header("Rand Time")]
    public float MaxRandTime;
    public float MinRandTime;
    [Header("MobList")]
    public List<GameObject> ItemList;

    private List<GameObject> MakeList;
    private float m_RandTime;
    private Vector2 m_RandPos;
    private int m_RandType;

    private void Start()
    {
        MakeList = new List<GameObject>();
        if(ItemList.Count <= 0)
        {
            return;
        }
        m_RandPos.x = 1100;
        StartCoroutine(RandSpawn());
    }

    public void reset()
    {
        int count = MakeList.Count;
        for (int i = 0; i < count; i++)
        {
            Destroy(MakeList[i]);
        }
        MakeList = new List<GameObject>();
        if (ItemList.Count <= 0)
        {
            return;
        }
        m_RandPos.x = 1100;
        StartCoroutine(RandSpawn());
    }

    IEnumerator RandSpawn()
    {
        while (true)
        {
            m_RandPos.y = Random.Range(MinY, MaxY);
            m_RandTime = Random.Range(MinRandTime, MaxRandTime);
            m_RandType = Random.Range(0, ItemList.Count);
            var temp = Instantiate(ItemList[m_RandType], gameObject.transform);
            temp.name = "Item";
            temp.transform.localPosition = m_RandPos;
            MakeList.Add(temp);
            yield return new WaitForSeconds(m_RandTime);
        }
    }
}
