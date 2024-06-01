using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ManagerOfScenes : MonoBehaviour
{
    private int cheatPart = 0;
    private int[] req = {0,1,2,3,4,5};
    private KeyCode[] keys = {KeyCode.UpArrow, KeyCode.DownArrow,KeyCode.LeftArrow,KeyCode.RightArrow,KeyCode.DownArrow, KeyCode.UpArrow };
    private bool good = false;
    private Cheats cheatsClass;
    private bool win;
    private bool secret = false;
    [HeaderAttribute("Public for other classes")]
    public bool cheats;
    [HeaderAttribute("Secret")]
    public Button menuButton;
    public Button gameButton;
    public Button quitButton;
    public Button secretButton;
    public TMP_Text wonText;
    public TMP_Text playingText;
    public GameObject secretText;


    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            win = GameObject.FindWithTag("Spawn").GetComponent<WaveManager>().won;
            cheatsClass = GetComponent<Cheats>();
            cheatsClass.On_Off = cheats;
        }
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
                Keeping.keepingBool = cheats;
            }
            if (Input.anyKeyDown && !good)
            {
                cheatPart = 0;
                Debug.Log("back to 0");
            }
        }
        if ((SceneManager.GetActiveScene().buildIndex == 1) && win)
        {
            gameWon();
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

    public void gameWon()
    {
        SceneManager.LoadScene(4);
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

    public void setSecret()
    {
        secret = !secret;
        if(secret)
        {
            showSecret();
        }
        else
        {
            hideSecret();
        }
    }
    public void showSecret()
    {
        menuButton.gameObject.SetActive(false);
        gameButton.gameObject.SetActive(false);
        quitButton.gameObject.SetActive(false);
        secretButton.gameObject.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = "Back";
        wonText.gameObject.SetActive(false);
        playingText.gameObject.SetActive(false);
        secretText.gameObject.transform.GetChild(0).gameObject.SetActive(true);
        secretText.gameObject.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
    }

    public void hideSecret()
    {
        menuButton.gameObject.SetActive(true);
        gameButton.gameObject.SetActive(true);
        quitButton.gameObject.SetActive(true);
        secretButton.gameObject.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = "Secret";
        wonText.gameObject.SetActive(true);
        playingText.gameObject.SetActive(true);
        secretText.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        secretText.gameObject.transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
    }
}