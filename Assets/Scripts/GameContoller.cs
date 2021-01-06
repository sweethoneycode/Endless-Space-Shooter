using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameContoller : MonoBehaviour
{
    public GameObject scoreText;
    public GameObject timerText;
    private float timer;

    public float playerScore = 0;

    private void Awake()
    {
        timerText.GetComponent<TMP_Text>().text = "Timer: " + timerText;
        scoreText.GetComponent<TMP_Text>().text = "Score: " + playerScore;
    }

    public void UpdateScore(float kill)
    {
        playerScore += kill;

        scoreText.GetComponent<TMP_Text>().text = "Score: " + playerScore;
    }

    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        timer++;
        timerText.GetComponent<TMP_Text>().text = "Timer: " + timer;
    }
}
