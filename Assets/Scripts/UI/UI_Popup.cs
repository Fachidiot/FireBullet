using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_Popup : MonoBehaviour
{
    public GameObject OptionPrefab;

    public void Close()
    {
        gameObject.SetActive(false);

    }

    public void Option()
    {
        Instantiate(OptionPrefab, gameObject.transform);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
