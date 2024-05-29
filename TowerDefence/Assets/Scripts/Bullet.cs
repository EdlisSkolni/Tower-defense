using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform target;
    private float slowing = 0.5f;
    private EnemyMovement enemy;
    private bool slow = false;
    private VomuleManager vomuleManager;
    private bool boom = false;
    [Header("Attributes")]
    public float explosionRadius = 0f;
    public float speed = 70f;
    public float damage = 5;
    [Header("Particals")]
    public GameObject Normal;
    public GameObject Freeze;
    public GameObject MoreDamage;
    public GameObject Boom;

    public void Seek(Transform targetMed, int turretType)
    {
        target = targetMed;
        enemy = targetMed.GetComponent<EnemyMovement>();
        if(turretType == 3)
        {
            slow = true;
            damage /= 2;
        }
        if(turretType == 2)
        {
            damage *= 3;
        }
        if(turretType == 4)
        {
            boom = true;
            damage = 40;
            explosionRadius = 10f;
        }

    }

    private void Start()
    {
        vomuleManager = GameObject.FindWithTag("Audio").GetComponent<VomuleManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 direction = target.position - transform.position;
        float distance = speed * Time.deltaTime;

        if (direction.magnitude <= distance)
        {
            hitTarget();
            return;
        }
        transform.Translate(direction.normalized * distance, Space.World);
        transform.LookAt(target);
    }

    void hitTarget()
    {
        if (slow)
        {
            GameObject parti = (GameObject)Instantiate(Freeze, transform.position, transform.rotation);
            Destroy(parti, 2f);
            vomuleManager.playEffect(vomuleManager.hitFromFreeze);
            enemy.slow(slowing);
        }
        else if (!boom)
        {
            GameObject parti = (GameObject)Instantiate(Normal, transform.position, transform.rotation);
            Destroy(parti, 2f);
            vomuleManager.playEffect(vomuleManager.hitFromNormalAndDMG);
        }

        if(explosionRadius> 0)
        {
            Explosion();
        }
        else
        {
            enemy.getDamage(damage);
        }

        Destroy(gameObject);
    }

    private void Explosion()
    {
        vomuleManager.playEffect(vomuleManager.boom);
        GameObject parti = (GameObject)Instantiate(Boom, transform.position, transform.rotation);
        Destroy(parti, 2f);
        Collider[] cols = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach(Collider col in cols)
        {
            if(col.tag == "Enemy")
            {
                col.gameObject.GetComponent<EnemyMovement>().getDamage(damage);
            }
        }
    }
}
