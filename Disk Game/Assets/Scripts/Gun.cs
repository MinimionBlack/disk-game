using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public float range = 20f;
    public float verticalRange = 20f;
    public float gunShotRadius = 20F;

    public float fireRate; = 1f;
    public float bigDamage = 2f;
    public float smallDamage = 1f;


    private float nextTimeToFire;
    private BoxCollider gunTrigger;


    public LayerMask raycastLayerMask;
    public EnemyManager enemyManager;
    void Start()
    {
        gunTrigger = GetComponent<BoxCollider>();
        gunTrigger.size = new Vector3(x: 1, y: verticalRange, z:range);
        gunTrigger.center = new Vector3(x: 0, y: 0, z: range * 0.5f);
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0)&& Time.time > nextTimeToFire) 
        {
            Fire(); 
        }
    }


    void Fire()
    {
        //simulate gun shot radius

        //alert any enemy in earshot
        
        //play test audio
        GetComponent<AudioSource>().Stop();
        GetComponent<AudioSource>().Play();


        // damage enemies
        foreach (var enemy in enemyManager.enemiesInTrigger)
        {
            // get direction to enemy
            var dir = enemy.transform.position - transform.position;
            
            RaycastHit hit;
            if(Physics.Raycast(transform.position, dir, out hit, range * 1.5f, raycastLayerMask))
            {
                if(hit.transform == enemy.transform)
                {
                    //range check
                    float dist = Vector3.Distance(enemy.transform.position, transform.position);

                    if(dist > range * 0.5f)
                    {
                        //damage enemy small
                        enemy.TakeDamage(smallDamage);

                    }
                    else
                    {
                        //damage enemy 
                        enemy.TakeDamage(bigDamage);
                    }
                }
            }
        }



        //reset timer
        nextTimeToFire = Time.time * fireRate;
    }
    private void OnTriggerEnter(Collider other)
    {
        // add potential enemy to shoot
        Enemy enemy = other.transform.GetComponent<Enemy>();

        if (enemy)
        {
            enemyManager.AddEnemy(enemy);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        // remove potential enemy to shoot
        Enemy enemy = other.transform.GetComponent<Enemy>();

        if (enemy)
        {
            enemyManager.RemoveEnemy(enemy);
        }
    }
}
