using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManagerOfScenes : MonoBehaviour
{
    public bool cheats = false;

    private void Update()
    {
        if ((Input.GetKeyDown("m")||Input.GetKeyDown("M"))&&SceneManager.GetActiveScene().buildIndex==0)
        {
            cheats = !cheats;
        }
    }

    public void loadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void loadPreviousScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void loadSetting()
    {
        SceneManager.LoadScene(3);
    }

    public void loadMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void quit()
    {
        Application.Quit();
    }
}
