using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Meanu : MonoBehaviour {


    public GameObject highest_score;
	// Use this for initialization
	void Start () {
        GameController.Load();
        highest_score.GetComponent<Text>().text = GameController.instance.highest_scores.ToString();
	}
	
    public void loadGame()
    {
        SceneManager.LoadScene(1);
       // StartGame.game.gameOver = false;
    }
}
