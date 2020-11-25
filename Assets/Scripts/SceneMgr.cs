using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMgr : MonoBehaviour
{
    public void MoveScene(string scenename)
    {
        SceneManager.LoadScene(scenename);
    }
}
