using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WaveManager : MonoBehaviour
{
    private int waveNumber = 0;
    private bool help = false;
    private int[] numberOfEnemiesArray = new int[20];
    private int[] numberOfBossesArray = new int[20];
    private TMP_Text text;

    [HeaderAttribute("Public for other classes")]
    public bool increaseGold = false;
    public float numberOfEnemies;
    public GameObject[] enemiesNBossesRemaining;

    [HeaderAttribute("Enemies")]
    public GameObject enemy;
    public GameObject boss;

    void Start()
    {
        setWave();
        text = GameObject.FindWithTag("EnemiesRemains").GetComponent<TMP_Text>();
    }

    private void Update()
    {
        if(help)
        {
            StartCoroutine(spawn());
            help=false;
        }
        enemiesNBossesRemaining = GameObject.FindGameObjectsWithTag("Enemy");
        text.text = "Enemies Remaining: " + enemiesNBossesRemaining.Length;
    }

    private IEnumerator spawn()
    {
        for(int i = 0; i < numberOfEnemiesArray[waveNumber-1]; i++)
        {
            Instantiate(enemy,transform.position,transform.rotation);
            yield return new WaitForSeconds(1);
        }
        if(numberOfBossesArray[waveNumber-1]!=0)
        {
            for (int i = 0; i < numberOfEnemies; i++)
            {
                Instantiate(boss, transform.position, transform.rotation);
                yield return new WaitForSeconds(1);
            }
        }
    }

    public void startWave(Button but)
    {
        waveNumber++;
        increaseGold = true; //<=================golds
        help = true;
        but.gameObject.SetActive(false);
        StartCoroutine(waitButton(but));
    }

    public IEnumerator waitButton(Button but)
    {
        while (true)
        {
        yield return new WaitForSeconds(2);
            if (enemiesNBossesRemaining.Length <= 0)
            {
                but.gameObject.SetActive(true);
                increaseGold=false; //<================golds
                break;
            }
        }
    }

    private void setWave()
    {
        //bosses
        for (int i = 0; i < 4; i++)//wave 1-4, 6-9, 11-14, 16-19 no bosses
        {
            numberOfBossesArray[i] = 0;
            numberOfBossesArray[i + 5] = 0;
            numberOfBossesArray[i + 10] = 0;
            numberOfBossesArray[i + 15] = 0;
        }
        numberOfBossesArray[4] = 1; //wave 5
        numberOfBossesArray[9] = 1; //wave 10
        numberOfBossesArray[14] = 2; //wave 15
        numberOfBossesArray[19] = 3; //wave 20
        //enemies
        numberOfEnemiesArray[0] = 10; //wave 1
        numberOfEnemiesArray[1] = 20; //wave 2
        numberOfEnemiesArray[2] = 30; //wave 3
        numberOfEnemiesArray[3] = 30; //wave 4
        numberOfEnemiesArray[4] = 30; //wave 5 with boss.. 1
        for(int i = 0;i < 10;i++)
        {
            numberOfEnemiesArray[i+5] = 40; //wave 6-15
        }
        numberOfEnemiesArray[15] = 50; //wave 16
        numberOfEnemiesArray[16] = 50; //wave 17
        numberOfEnemiesArray[17] = 50; //wave 18
        numberOfEnemiesArray[18] = 50; //wave 19
        numberOfEnemiesArray[19] = 40; //wave 20 with boss.. 3
    }
}
