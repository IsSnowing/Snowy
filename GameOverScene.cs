using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameOverScene : MonoBehaviour {
    public GameObject highest_score;
    public GameObject scores;

    private void Start()
    {
        GameController.Load();
        highest_score.GetComponent<Text>().text = GameController.instance.highest_scores.ToString();
        scores.GetComponent<Text>().text = GameController.instance.scores.ToString();
    }

    public void restart()
    {
        //StartGame.game.gameOver = false;
        SceneManager.LoadScene(1);
        
    }

    public void home()
    {
        
        SceneManager.LoadScene(0);
        
    }
}
