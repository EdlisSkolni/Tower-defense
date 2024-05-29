using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    private Transform target;
    private VomuleManager volume;
    private float fireCountdown = 0f;

    [HeaderAttribute("shooting")]
    public string tag = "Enemy";
    public float range = 15f;
    public float fireRate = 1f;
    public GameObject bulletPrefab;
    public Transform firePoint;

    [HeaderAttribute("class")]
    public bool splash = false;
    public bool freeze = false;
    public bool dmg = false;

    public int type()
    {
        int help = 1;
        if (freeze)
        {
            help = 3;
        } 
        else if(splash)
        {
            fireRate = 0.2f;
            help = 4;
        }
        else if(dmg)
        {
            help = 2;
        }
        return help;
    }


    void Start()
    {
        InvokeRepeating("updateTarget", 0f, 0.5f);
        volume = GameObject.FindWithTag("Audio").GetComponent<VomuleManager>();
    }

    void Update()
    {
        if(target == null)
        {
            return;
        }

        if (fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime;
    }

    void Shoot()
    {
        GameObject bulletGo = (GameObject)Instantiate(bulletPrefab, firePoint.position,firePoint.rotation);
        Bullet bullet = bulletGo.GetComponent<Bullet>();
        if (bullet != null)
        {
            bullet.Seek(target, type());
        }
        volume.playEffect(volume.turretHit);
    }

    void updateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(tag);
        float shortest = 999999;
        GameObject nearests = null;
        foreach (GameObject enemy in enemies)
        {
            float directence = Vector3.Distance(transform.position, enemy.transform.position);
            if (directence < shortest)
            {
                shortest = directence;
                nearests = enemy;
            }
        }

        if (nearests != null && shortest<=range)
        {
            target = nearests.transform;
        }
        else
        {
            target = null;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
