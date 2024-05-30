using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheatMenu : MonoBehaviour
{
    private Cheats cheats;
    private string m = "MainMain";

    [HeaderAttribute("Insert")]
    public Toggle toggleMenu;
    public Button addGolds;
    public Toggle invincibleBase;
    public Button killAll;
    public Button moreDamage;

    void Start()
    {
        cheats = GameObject.FindGameObjectWithTag(m).GetComponent<Cheats>();
        if (cheats.On_Off)
        {
            toggleMenu.gameObject.SetActive(true);
        }
    }

    public void turnMenuOnOff()
    {
        if (toggleMenu.isOn)
        {
            addGolds.gameObject.SetActive(true);
            killAll.gameObject.SetActive(true);
            invincibleBase.gameObject.SetActive(true);
            moreDamage.gameObject.SetActive(true);
        }else
        {
            addGolds.gameObject.SetActive(false);
            killAll.gameObject.SetActive(false);
            invincibleBase.gameObject.SetActive(false);
            moreDamage.gameObject.SetActive(false);
        }
    }
}
