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
    private bool fromGameToSetting = false;
    [HeaderAttribute("Setting to Game button")]
    public Button backGameButton;
    [HeaderAttribute("Public for other classes")]
    public bool cheats;
    [HeaderAttribute("Camera movent show")]
    public GameObject cameraMovement;
    [HeaderAttribute("Secret")]
    public Button menuButton;
    public Button gameButton;
    public Button quitButton;
    public Button secretButton;
    public TMP_Text wonText;
    public TMP_Text playingText;
    public GameObject secretText;

    private void Start()
    {
        if(SceneManager.GetActiveScene().buildIndex == 1)
        {
            StartCoroutine(showMovement());
        }
        fromGameToSetting = Keeping.keepingBool2;
        if (fromGameToSetting)
        {
            backGameButton.gameObject.SetActive(true);
        }
        else
        {
            backGameButton.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            win = GameObject.FindWithTag("Spawn").GetComponent<WaveManager>().won;
            cheatsClass = GetComponent<Cheats>();
            cheatsClass.On_Off = cheats;
            Keeping.keepingBool2 = true;
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

    public IEnumerator showMovement()
    {
        yield return new WaitForSeconds(3);
        RawImage image1 = cameraMovement.transform.GetChild(2).GetComponent<RawImage>();
        RawImage image2 = cameraMovement.transform.GetChild(3).GetComponent<RawImage>();
        TextMeshProUGUI text1 = cameraMovement.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI text2 = cameraMovement.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>();
        for (int i = 10; i>= 0; i--)
        {
            yield return new WaitForSeconds(0.05f);
            text1.color = new Color(text1.color.r, text1.color.g, text1.color.b, i / 10f);
            text2.color = new Color(text2.color.r, text2.color.g, text2.color.b, i / 10f);
            image1.color = new Color(image1.color.r, image1.color.g, image1.color.b, i / 10f);
            image2.color = new Color(image2.color.r, image2.color.g, image2.color.b, i / 10f);
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

    public void toGame()
    {
        SceneManager.LoadScene(1);
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

    public void hideFinishedWave()
    {
        GameObject finished = GameObject.FindGameObjectWithTag("Finished");
        if (finished != null)
        {
            finished.SetActive(false);
        }
    }
}