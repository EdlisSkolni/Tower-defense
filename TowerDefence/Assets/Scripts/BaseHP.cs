using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BaseHP : MonoBehaviour
{
    public float maxHP;
    public float currentHP;
    private int destroyed = 1;
    public Slider Slider;
    private Camera cam;

    // Start is called before the first frame update
    private void Start()
    {
        currentHP = maxHP;
        Slider.maxValue = maxHP;
        Slider.value = currentHP;
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentHP <= 0 && destroyed == 1)
        {
            Debug.Log("base je znièená");
            destroyed++;
        }
        if (Slider != null)
        {
            Slider.value = currentHP;
            if (Slider.gameObject.transform.tag != "TargetSlider")
            {
                Slider.transform.LookAt(cam.transform.position);
            }
        }
    }

    public void damageBase(float damage)
    {
        currentHP -= damage;
        if(currentHP <= 0)
        {
            Destroy(gameObject);
            if(gameObject.transform.tag=="Target")
            {
                gameLost();
            }
        }
    }

    public void setHP(float hps)
    {
        currentHP = hps;
    }

    public void gameLost()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
