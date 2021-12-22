using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StartMenu : MonoBehaviour
{
    float playerScore;
    [SerializeField] TMP_Text currentPlayerScore;

    // Start is called before the first frame update
    void Start()
    {
        playerScore = SaveManager.instance.activeSave.highScore;
        currentPlayerScore.text = "Player High Score: " + playerScore.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
