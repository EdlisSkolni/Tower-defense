using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Cheats : MonoBehaviour
{
    private GameObject[] enemies;
    private Golds gold;
    private string m = "MainMain";
    private ManagerOfScenes manager;
    private BaseHP hp;
    private bool baseNotDie = false;
    private GameObject[] turretsTypeOne;
    private GameObject[] turretsTypeTwo;
    private GameObject[] turretsTypeThree;
    private GameObject[] turretsTypeFour;
    [HeaderAttribute("On or Off")]
    public bool On_Off;

    private void Start()
    {
        gold = GameObject.FindWithTag(m).GetComponent<Golds>();
        manager = GameObject.FindWithTag(m).GetComponent<ManagerOfScenes>();
        hp = GameObject.FindWithTag("Base").transform.GetChild(0).gameObject.GetComponent<BaseHP>();
    }

    private void Update()
    {
        On_Off = manager.cheats;
        turretsTypeOne = GameObject.FindGameObjectsWithTag("1");
        turretsTypeTwo = GameObject.FindGameObjectsWithTag("2");
        turretsTypeThree = GameObject.FindGameObjectsWithTag("3");
        turretsTypeFour = GameObject.FindGameObjectsWithTag("4");   
    }

    public void killAllExisting()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (var enemy in enemies)
        {
            Destroy(enemy);
        }
    }

    public void add1000Golds()
    {
        gold.plus(1000);
    }

    public void remove1000Golds()
    {
        if (gold.golds < 1000)
        {
            int minus = 1000 - gold.golds;
            if(minus > 0)
            {
                gold.minus(minus);
            }
        }
        else
        {
            gold.minus(1000);
        }
    }

    public void invencibleBaseOn_Off()
    {
        baseNotDie = !baseNotDie;
        if (baseNotDie)
        {
            StartCoroutine(invencibleBase());
            Debug.Log("cant die - " + baseNotDie);
        }
    }

    private IEnumerator invencibleBase()
    {
        while (baseNotDie)
        {
            yield return new WaitForSeconds(1);
            hp.setHP(1000);
            Debug.Log("setting hp to 1000");
        }
    }

    public void turretDMGPlus100Percent()
    {
        giveMoreDMG(turretsTypeOne);
        giveMoreDMG(turretsTypeTwo);
        giveMoreDMG(turretsTypeThree);
        giveMoreDMG(turretsTypeFour);
    }

    private void giveMoreDMG(GameObject[] turrets)
    {
        foreach(var turret in turrets)
        {
            Debug.Log(turret.gameObject.name);
            turret.gameObject.transform.GetChild(0).gameObject.GetComponent<Turret>().multi = 100;
        }
    }

    public void instantWin()
    {
        manager.gameWon();
    }
}