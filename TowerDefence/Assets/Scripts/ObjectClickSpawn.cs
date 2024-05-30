using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Numerics;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ObjectClickSpawn : MonoBehaviour
{
    private bool correctLayer = false;
    private bool correctBaricade = false;
    private bool correctMine = false;
    private int choice = 0;
    private int[] price = {25,125,225,500,750,1000};
    private int golds = 0;
    private bool enough = false;
    private TMP_Text textNotEnough;
    /*
        choice = -1.. deleting - return 100%
        choice = 1.. turret 1 - costs 25
        choice = 2.. turret 2 - costs 125 dmg
        choice = 3.. turret 3 - costs 225 freeze
        choice = 4.. turret 4 - costs 500 splash
        choice = 5.. barrier - costs 750
        choice = 6.. mine - costs 1000
    */
    private GameObject game;
    private GameObject spawn;
    private GameObject mainCan;
    private Cheats cheatOnOff;
    private bool cheats = false;

    [HeaderAttribute("Public for other classes")]
    public int numMine = 0;

    [HeaderAttribute("Layers")]
    public LayerMask ableToPlace;
    public LayerMask ableToMine;
    public LayerMask ableToBlock;

    [HeaderAttribute("Models")]
    public GameObject prefabTT1;
    public GameObject prefabTT2;
    public GameObject prefabTT3;
    public GameObject prefabTT4;
    public GameObject prefabOR1;
    public GameObject prefabOR2;

    private void Start()
    {
        game = GameObject.FindWithTag("Canvas");
        spawn = GameObject.FindWithTag("Spawn");
        mainCan = GameObject.FindWithTag("MainMain");
        cheatOnOff = GameObject.FindWithTag("Can menu").GetComponent<Cheats>();
        cheats = cheatOnOff.On_Off;
    }
    void Update()
    {
        golds = GameObject.FindWithTag("MainMain").GetComponent<Golds>().golds;
        placeable();
        if (Input.GetMouseButtonDown(0) && correctLayer)
        {
            placeTurret();
        }
        if (Input.GetMouseButtonDown(0) && correctMine)
        {
            placeMine();
        }
        if (Input.GetMouseButtonDown(0) && correctBaricade)
        {
            placeBarricade();
        }
        if (Input.GetMouseButtonDown(0) && (choice == -1))
        {
            deleteObject();
            choice = 0;
        }
    }

    public void choices(int number)
    {
        choice = number;
        if(number>0 && number<5)
        {
            game.gameObject.GetComponent<CanvasOnPlace>().visible();
        }
        if (number == -1|| number ==5 || number ==6)
        {
            game.gameObject.GetComponent<CanvasOnPlace>().invisible();
        }
    }

    public void start()
    {
        spawn.gameObject.GetComponent<WaveManager>().startWave(GameObject.FindWithTag("StartButton").GetComponent<Button>());
        
    }

    public void placeTurret()
    {
        GameObject[] objects = { prefabTT1, prefabTT2, prefabTT3, prefabTT4};
        enoughGolds(choice-1);
        if (choice != 0 && enough)
        {
            place(objects[choice - 1]);
            mainCan.gameObject.GetComponent<Golds>().minus(price[choice-1]);
        }
        game.gameObject.GetComponent<CanvasOnPlace>().invisible();
        choice = 0;
    }

    public void placeMine()
    {
        enoughGolds(5);
        if (choice!=0 && choice == 6 && enough)
        {
            place(prefabOR2);
            numMine++;
            mainCan.gameObject.GetComponent<Golds>().minus(price[5]);
        }
        choice = 0;
    }

    public void placeBarricade()
    {
        enoughGolds(4);
        if (choice != 0 && choice==5 && enough)
        {
            place(prefabOR1);
            mainCan.gameObject.GetComponent<Golds>().minus(price[4]);
        }
        choice = 0;
    }

    public void enoughGolds(int pricePlace)
    {
        if (cheats)
        {
            enough = true;
        }
        else
        {
            if (golds >= price[pricePlace])
            {
                enough = true;
            }
            else
            {
                enough = false;
                StartCoroutine(showNotEnough());
            }
        }
    }

    public IEnumerator showNotEnough()
    {
        textNotEnough.text = "Not enough golds";
        yield return new WaitForSeconds(3);
        textNotEnough.text = "";
    }

    private void placeable()
    {
        Ray cameroToPoint = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit endOfPoint;
        if ((Physics.Raycast(cameroToPoint, out endOfPoint)) && (endOfPoint.transform.gameObject.layer == 7))
        {
            correctLayer = true;
        }
        else
        {
            correctLayer = false;
        }
        if ((Physics.Raycast(cameroToPoint, out endOfPoint)) && (endOfPoint.transform.gameObject.layer == 6))
        {
            correctBaricade = true;
        }
        else
        {
            correctBaricade = false;
        }
        if ((Physics.Raycast(cameroToPoint, out endOfPoint)) && (endOfPoint.transform.gameObject.layer == 9))
        {
            correctMine = true;
        }
        else
        {
            correctMine = false;
        }
    }

    private void testLayer()
    {
        Ray cameroToPoint = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit endOfPoint;
        if (Physics.Raycast(cameroToPoint, out endOfPoint)&& Input.GetMouseButtonDown(0))
        {
            GameObject pomoc = endOfPoint.transform.gameObject;
            int pomoc2 = pomoc.layer;
            Debug.Log("Objekt " + pomoc + ", má layer: " + pomoc2);
        }
    }

    private void deleteObject()
    {
        Ray cameroToPoint = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit endOfPoint;
        if (Physics.Raycast(cameroToPoint, out endOfPoint) && (endOfPoint.transform.gameObject.layer == 8 ||
            endOfPoint.transform.gameObject.layer==10 || endOfPoint.transform.gameObject.layer==11))
        {
            if(endOfPoint.transform.gameObject.layer == 10)
            {
                mainCan.gameObject.GetComponent<Golds>().plus(price[5]);
                numMine--;
            }
            if(endOfPoint.transform.gameObject.layer == 11)
            {
                mainCan.gameObject.GetComponent<Golds>().plus(price[4]);
            }
            if(endOfPoint.transform.gameObject.layer == 8)
            {
                checkTag(endOfPoint, 1);
                checkTag(endOfPoint, 2);
                checkTag(endOfPoint, 3);
                checkTag(endOfPoint, 4);
            }
            Destroy(endOfPoint.transform.gameObject);
        }
    }

    public void checkTag(RaycastHit endOfPoint, int tagInt)
    {
        string tag = Convert.ToString(tagInt);
        if (endOfPoint.transform.gameObject.tag == tag)
        {
            mainCan.gameObject.GetComponent<Golds>().plus(price[tagInt-1]);
        }
    }

    private void place(GameObject prefabHere)
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray cameroToPoint = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit endOfPoint;
            if (Physics.Raycast(cameroToPoint, out endOfPoint))
            {
                if(choice!=5)
                {
                    Instantiate(prefabHere, endOfPoint.point, endOfPoint.transform.rotation);
                }
                else 
                {
                    GameObject bar = Instantiate(prefabHere, endOfPoint.point, endOfPoint.transform.rotation);
                    bar.transform.Rotate(0, 90, 0);
                }
            }
        }
    }
}
