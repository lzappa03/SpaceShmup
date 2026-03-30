using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_0 : Enemy
{
    [Header("Inscribed")]
    
    public GameObject projectilePrefab;
    
    // Update is called once per frame
    void Update()
    {
        Move();

        if(Time.time >= nextFireTime){
            TempFire();
            nextFireTime = Time.time + fireRate;
        }
    }

    void TempFire()
    {
        GameObject projGO = Instantiate<GameObject>(projectilePrefab);
        projGO.transform.position = transform.position;
        Rigidbody rigidB = projGO.GetComponent<Rigidbody>();
        rigidB.velocity = Vector3.down * projectileSpeed;
        
    }
}
