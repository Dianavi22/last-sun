using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject credits;
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            credits.SetActive(false);
        }
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void LoadGame()
    {
        SceneManager.LoadScene(1);
    }

    public void OpenCredits()
    {
        credits.SetActive(true);
    }
}
