using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    Animation anim;
    UnityEngine.AI.NavMeshAgent nav;
    Shooting Damage;
    public bool isMoving = true;
    

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animation>();
        currentHealth = startingHealth;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            isMoving = false;
            anim.Play("Death");
            StartCoroutine(Despawn(1.5f));
        }
    }

    IEnumerator Despawn(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(this.gameObject);
    }
}
