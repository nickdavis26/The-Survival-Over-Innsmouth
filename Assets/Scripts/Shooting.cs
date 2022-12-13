using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    int damage = 10;
    public float range = 100f;
    public float impactForce = 30f;
    public float fireRate = 6f;

    public Camera fpsCam;
    public ParticleSystem bulletTravel;
    public ParticleSystem muzzleFlash;
    public ParticleSystem bloodSplatter;
    public AudioSource gunShot;
    public AudioSource zombieShot;

    private float nextShot = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextShot)
        {
            nextShot = Time.time + 1f / fireRate;
            Shoot();
        }
    }

    void Shoot()
    {
        RaycastHit hit;
        bulletTravel.Play();
        muzzleFlash.Play();
        gunShot.Play();
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            EnemyHealth enemyHealth = hit.transform.GetComponent<EnemyHealth>();
            bloodSplatter = hit.transform.gameObject.GetComponentInChildren(typeof(ParticleSystem), true) as ParticleSystem;
            zombieShot = hit.transform.GetComponent<AudioSource>();


            if (enemyHealth != null)
            {
                bloodSplatter.transform.position = hit.point;
                bloodSplatter.Play();
                zombieShot.Play();
                enemyHealth.TakeDamage(damage);
            }
            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }
        }
    }
}
