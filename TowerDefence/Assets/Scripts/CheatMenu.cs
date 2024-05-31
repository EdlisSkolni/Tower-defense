using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheatMenu : MonoBehaviour
{
    [HeaderAttribute("Insert")]
    public GameObject toggleMenu;
    public Button addGolds;
    public Toggle invincibleBase;
    public Button killAll;
    public Button moreDamage;
    public Button instantWin;
    public Button removeGolds;

    private void Update()
    {
        if (Keeping.keepingBool)
        {
            toggleMenu.gameObject.transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    public void turnMenuOnOff()
    {
        if (toggleMenu.gameObject.transform.GetChild(0).gameObject.GetComponent<Toggle>().isOn)
        {
            addGolds.gameObject.SetActive(true);
            killAll.gameObject.SetActive(true);
            invincibleBase.gameObject.SetActive(true);
            moreDamage.gameObject.SetActive(true);
            instantWin.gameObject.SetActive(true);
            removeGolds.gameObject.SetActive(true);
        }else
        {
            addGolds.gameObject.SetActive(false);
            killAll.gameObject.SetActive(false);
            invincibleBase.gameObject.SetActive(false);
            moreDamage.gameObject.SetActive(false);
            instantWin.gameObject.SetActive(false);
            removeGolds.gameObject.SetActive(false);
        }
    }
}
