using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Golds : MonoBehaviour
{
    public int golds = 0;
    private TMP_Text goldShow;
    public bool goldsIncome = false;
    private int mineNum = 0;
    // Start is called before the first frame update
    void Start()
    {
        goldShow = GameObject.FindWithTag("Gold").GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        goldsIncome = GameObject.FindWithTag("Spawn").GetComponent<WaveManager>().increaseGold;
        mineNum = GameObject.FindWithTag("MainMain").GetComponent<ObjectClickSpawn>().numMine;
        goldShow.text = "Golds: " + golds;
        goldsPlus();
    }

    public void goldsPlus()
    {
        if (goldsIncome)
        {
            golds++;
            if(mineNum >0)
            {
                for (int i = 0; i < mineNum; i++) 
                {
                    Debug.Log(mineNum);
                    golds++;
                }
            }
        }
    }

    public void minus(int amount)
    {
        golds -= amount;
    }

    public void plus(int amount)
    {
        golds += amount;
    }
}
