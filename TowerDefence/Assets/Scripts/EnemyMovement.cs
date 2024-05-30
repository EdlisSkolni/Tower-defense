using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyMovement : MonoBehaviour
{
    private bool touchingBase = false;
    private GameObject[] bariccades = { };
    private NavMeshAgent agent;
    private float maxHP = 100;
    private float speed;
    private bool cooldown = false;

    [HeaderAttribute("Basic settings")]
    public GameObject target;
    public Slider slider;
    public Camera camera;
    public GameObject partical;
    public VomuleManager vomuleManager;

    [HeaderAttribute("Attributes")]
    public float damage = 5;
    public float currentHP;
    public float startSpeed = 2;

    [HeaderAttribute("Boss")]
    public bool isBoss = false;
    public GameObject enemyToSpawn;
    public int cooldownInt = 10;
    void Start()
    {
        vomuleManager = GameObject.FindWithTag("Audio").GetComponent<VomuleManager>();
        speed = startSpeed;
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Target");
        currentHP = maxHP;
        slider.maxValue = maxHP;
        slider.value = currentHP;
        if (camera == null)
        {
            camera = Camera.main;
        }
        //boss
        if (isBoss)
        {
            speed /= 2;
            maxHP *= 10;
            StartCoroutine(bossAbility());
        }
        agent.speed = speed;
        InvokeRepeating("attackBariccade", 0f, 0.5f);
        InvokeRepeating("speedInNav",0f,1f);
    }

    void Update()
    {
        bariccades = GameObject.FindGameObjectsWithTag("Bariccade");
        agent.destination = target.transform.position;
        slider.value = currentHP;
        slider.transform.LookAt(camera.transform);
    }

    public void speedInNav()
    {
        agent.speed = speed;
    }

    //boss
    private IEnumerator bossAbility()
    {
        while (true)
        {
            transform.gameObject.SetActive(true);
            yield return new WaitForSeconds(3);
            if (cooldownInt >= 10)
            {
                cooldown = true;
            }
            else
            {
                cooldown = false;
            }
            int random = UnityEngine.Random.Range(0, 10);
            Debug.Log(random);
            if (random == 5 && cooldown)
            {
                Vector3 newOne = new Vector3(transform.position.x, transform.position.y, transform.position.z);
                Instantiate(enemyToSpawn, newOne, gameObject.transform.rotation);
                Instantiate(enemyToSpawn, newOne, gameObject.transform.rotation);
                cooldownInt = 0;
                cooldown = false;
            }
            if (cooldownInt < 10)
            {
                cooldownInt++;
            }
        }
    }

    public void attackBariccade()
    {
        if (bariccades.Length == 0)
        {
            target = GameObject.FindGameObjectWithTag("Target");
        }
        else
        {
            GameObject[] baricadesss = GameObject.FindGameObjectsWithTag("Bariccade");
            float shortest = 999999;
            GameObject nearests = null;
            foreach (GameObject bar in baricadesss)
            {
                float directence = Vector3.Distance(transform.position, bar.transform.position);
                if (directence < shortest)
                {
                    shortest = directence;
                    nearests = bar;
                }
            }
            target = nearests;
        }
    }

    public void getDamage(float damage)
    {
        currentHP -= damage;
        slider.value = damage;
        if (currentHP <= 0)
        {//ded
            GameObject parti = Instantiate(partical, transform.position, transform.rotation);
            vomuleManager.playEffect(vomuleManager.enemyDed);
            Destroy(parti, 2f);
            Destroy(gameObject);
        }
    }

    public void slow(float percent)
    {
        speed = startSpeed * (1f - percent); 
        vomuleManager.playEffect(vomuleManager.hitFromFreeze);
        StartCoroutine(backSpeed());
    }

    public IEnumerator backSpeed()
    {
        yield return new WaitForSeconds(2);
        speed = startSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Target")|| other.gameObject.CompareTag("Bariccade"))
        {
            touchingBase = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        int dmg = Convert.ToInt32(damage);
        if ((other.gameObject.CompareTag("Target") || other.gameObject.CompareTag("Bariccade")) && touchingBase)
        {
            vomuleManager.playEffect(vomuleManager.enemyHitStructures);
            other.GetComponent<BaseHP>().damageBase(dmg);
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (!(other.gameObject.CompareTag("Target") || other.gameObject.CompareTag("Bariccade")))
        {
            touchingBase = false;
        }
    }
}