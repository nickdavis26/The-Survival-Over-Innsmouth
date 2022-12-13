using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    GameObject player;
    UnityEngine.AI.NavMeshAgent nav;
    Animator anim;
    bool playerInRange = false;
    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;
    float timer = 0f;

    int attackDamage = 34;
    public float timeBetweenAttacks = 2f;
    Animation animate;

    // Start is called before the first frame update
    void Start()
    {
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        anim = GetComponent<Animator>();
        animate = GetComponent<Animation>();
        enemyHealth = GetComponent<EnemyHealth>();
        playerHealth = GameObject.FindWithTag("Player").GetComponent<PlayerHealth>();
        player = GameObject.FindWithTag("Player");
        animate.Play("Run");
        anim.SetBool("isWalking", true);
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyHealth.isMoving) // for some reason it never goes into this if statement
            nav.SetDestination(player.transform.position);

        timer += Time.deltaTime;

        if (timer >= timeBetweenAttacks && playerInRange && enemyHealth.currentHealth > 0)
        {
            timer = 0f;
            //if (playerHealth.currentHealth > 0)
                playerHealth.TakeDamage(attackDamage);
            //anim.SetTrigger("Walk");
        }

        

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && enemyHealth.isMoving)
        {
            playerInRange = true;
            animate.Play("Attack1");
        } else if(other.gameObject.tag != "Player")
        {
            animate.Play("Run");
        }
    }


    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player" && enemyHealth.isMoving)
        {
            playerInRange = false;
            animate.PlayQueued("Run", QueueMode.CompleteOthers);


        }
    }

    IEnumerator WaitToWalk(float time)
    {
        yield return new WaitForSeconds(time);
        animate.Play("Walk");
    }
}
