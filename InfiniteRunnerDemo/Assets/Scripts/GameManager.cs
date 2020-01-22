using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public bool isGameOver;

    public TextMeshProUGUI restartText;

    // Start is called before the first frame update
    void Start()
    {
        // Finds the gameObject for the restartText
        restartText = GameObject.Find("RestartText").GetComponent<TextMeshProUGUI>();
        // Makes restartText "invisible"
        restartText.gameObject.SetActive(false);
    }

    // GameOver: Called once the player has been killed
    public void GameOver()
    {
        isGameOver = true;
        // Makes the restart text reappear
        restartText.gameObject.SetActive(true);     
    }

    // RestartGame: Reloads the scene
    public void RestartGame()
    {
        isGameOver = false;
        // Reloads scene at sceneIndex 0. 
        // Since we only have 1 scene, we will reload the current scene
        SceneManager.LoadScene(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameOver && Input.GetKeyDown(KeyCode.R))
        {
            // If game is over and R is pressed, restart the game
            RestartGame();
        }
    }
}
