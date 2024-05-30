using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManagerOfScenes : MonoBehaviour
{
    public bool cheats;
    private int cheatPart = 0;
    private int[] req = {0,1,2,3,4,5};
    private KeyCode[] keys = {KeyCode.UpArrow, KeyCode.DownArrow,KeyCode.LeftArrow,KeyCode.RightArrow,KeyCode.DownArrow, KeyCode.UpArrow };
    private bool good = false;
    private Cheats cheatsClass;

    private void Start()
    {
        cheatsClass = GetComponent<Cheats>();
    }

    private void Update()
    {
        if(SceneManager.GetActiveScene().buildIndex == 0)
        {
            good = false;
            if (Input.anyKeyDown && cheatPart <= 5)
            {
                cheatCode(keys);
            }
            if (cheatPart == 6 && Input.GetKeyDown("s"))
            {
                cheats = !cheats;
                cheatPart = 0;
                cheatsClass.On_Off = cheats;               
            }
            if (Input.anyKeyDown && !good)
            {
                cheatPart = 0;
                Debug.Log("back to 0");
            }
        }
    }

    public void cheatCode(KeyCode[] key)
    {
        int requirement = req[cheatPart];
        if (Input.GetKeyDown(key[cheatPart])&&requirement==cheatPart)
        {
            cheatPart++;
            Debug.Log(cheatPart);
            good = true;
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