using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RulesManager : MonoBehaviour
{
    int roundNum = 0;
    double spawnNum;
    public GameObject zombie;
    public Transform[] spawnPoints;
    public AudioSource music1;
    public GameObject roundNumText;
    public TMP_Text roundText;
    public TMP_Text middleText;
    bool inRound = false;
    public Image gameOverImage;
    bool gameOver = false;
    public AudioSource bellSound;
    public GameObject player;


    // Start is called before the first frame update
    void Start()
    {
        spawnNum = 5.0;
        roundText.text = "Round " + roundNum.ToString();
        middleText.text = "Round " + roundNum.ToString();
        gameOverImage = GameObject.FindWithTag("Image").GetComponent<Image>();
        music1.Play();
        StartCoroutine(Round());
    }

    void Update()
    {
        if (!gameOver)
        {
            roundText.text = "Round " + roundNum.ToString();
            middleText.text = "Round " + roundNum.ToString();
        }
    }

    // Update is called once per frame
    IEnumerator Round()
    {
        if (!inRound)
        {
            inRound = true;
            ++roundNum;
            StartCoroutine(RoundTransition());
            yield return new WaitForSeconds(1);
            if (roundNum > 1) spawnNum = spawnNum * (1.5);
            for (int i = 0; i < spawnNum; ++i)
            {
                yield return new WaitForSeconds(2);
                int spawnPointIndex = UnityEngine.Random.Range(0, spawnPoints.Length);
                GameObject newZomb = Instantiate(zombie, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
            }
        }

        if(GameObject.FindWithTag("Enemy") == null)
        {
            inRound = false;
        }
        yield return new WaitForSeconds(1);
        StartCoroutine(Round());
    }

    IEnumerator RoundTransition()
    {
        //show round num in middle of screen
        roundText.enabled = false;
        middleText.enabled = true;
        bellSound.Play();
        yield return new WaitForSeconds(3);
        
        roundText.enabled = true;
        middleText.enabled = false;
        //roundText.text = "Round " + roundNum.ToString();
    }

    public void GameOver()
    {
        GameObject.FindWithTag("Crosshair").SetActive(false);
        gameOver = true;
        roundText.enabled = false;
        StartCoroutine(WaitandExit());
        //gameOverImage.enabled = (true);
        middleText.text = "Game Over";
        middleText.enabled = true;
    }

    IEnumerator WaitandExit()
    {
        UnityEngine.Debug.Log("In Coroutine");
        player.GetComponentInChildren<Shooting>().enabled = false;
        player.GetComponent<PlayerMovement>().enabled = false;
        player.GetComponentInChildren<CameraRotation>().enabled = false;
        Animation idle = zombie.GetComponent<Animation>();
        idle.Play("Idle");
        yield return new WaitForSeconds(2);
        gameOverImage.enabled = (true);
    }

}
