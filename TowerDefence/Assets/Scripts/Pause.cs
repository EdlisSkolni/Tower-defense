using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    private bool pauseMenuOn = false;
    [HeaderAttribute("Pause UI")]
    public GameObject ui;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            continuePlay();
        }
        Toggle();
    }

    private void Toggle()
    {
        ui.SetActive(pauseMenuOn);
        if(pauseMenuOn)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    public void continuePlay()
    {
        pauseMenuOn = !pauseMenuOn;
    }

    public void retry()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void soundSetting()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(3);
    }
}
