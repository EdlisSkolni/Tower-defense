using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheats : MonoBehaviour
{
    private GameObject[] enemies;
    private WaveManager spawner;
    private Golds gold;
    private ObjectClickSpawn click;
    private string m = "MainMain";
    private ManagerOfScenes manager;
    private BaseHP hp;
    private bool baseNotDie = false;
    [HeaderAttribute("On or Off")]
    public bool On_Off = false;

    private void Start()
    {
        spawner = GameObject.FindGameObjectWithTag("Spawn").GetComponent<WaveManager>();
        enemies = spawner.enemiesNBossesRemaining;
        gold = GameObject.FindWithTag(m).GetComponent<Golds>();
        click = GameObject.FindWithTag(m).GetComponent<ObjectClickSpawn>();
        click.cheats = true;
        manager = GameObject.FindWithTag(m).GetComponent<ManagerOfScenes>();
        On_Off = manager.cheats;
        hp = GameObject.FindWithTag("Target").GetComponent<BaseHP>();
    }

    public void killAllExisting()
    {
        foreach(var enemy in enemies)
        {
            enemy.GetComponent<EnemyMovement>().getDamage(999999999999);
        }
    }

    public void add1000Golds()
    {
        gold.plus(1000);
    }

    public void invencibleBaseOn_Off()
    {
        baseNotDie = !baseNotDie;
        if (baseNotDie)
        {
            StartCoroutine(invencibleBase());
        }
    }

    public IEnumerator invencibleBase()
    {
        while (baseNotDie)
        {
            yield return new WaitForSeconds(1);
            if(hp.currentHP<= 1000)
            {
                hp.setHP(1000);
            }
        }
    }

    public void turretDMGPlus100Percent()
    {
        //zvednout DMG pomocí Bullet class
    }
}
