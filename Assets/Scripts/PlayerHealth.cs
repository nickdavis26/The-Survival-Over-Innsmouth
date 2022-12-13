using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    Animator anim;
    public RulesManager rules;
    public AudioSource hurt;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int amount)
    {
        hurt.Play();
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            hurt.enabled = false;
            rules.GameOver();
        }
    }
}
