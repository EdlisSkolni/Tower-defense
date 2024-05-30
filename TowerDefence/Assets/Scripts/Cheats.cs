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
    private GameObject hp;
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
        On_Off = manager.cheats;
        hp = GameObject.FindWithTag("Target");
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

    public void invencibleBaseOn_Off()//nefunguje
    {
        baseNotDie = !baseNotDie;
        if (baseNotDie)
        {
            StartCoroutine(invencibleBase());
            Debug.Log("cant die");
        }
    }

    private IEnumerator invencibleBase()
    {
        while (baseNotDie)
        {
            yield return new WaitForSeconds(1);
            if(hp.GetComponent<BaseHP>().currentHP<= 1000)
            {
                hp.GetComponent<BaseHP>().setHP(1000);
            }
        }
    }

    public void turretDMGPlus100Percent()//nefunguje
    {
        giveMoreDMG(0);
        giveMoreDMG(1);
        giveMoreDMG(2);
        giveMoreDMG(3);
    }

    private void giveMoreDMG(int arrayNumber)
    {
        GameObject[][] arrays = {turretsTypeOne, turretsTypeTwo, turretsTypeThree, turretsTypeFour};
        
            foreach(var turret in arrays[arrayNumber])
            {
                turret.GetComponent<Turret>().multi = 100;
            }
        
    }
}