using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snow : MonoBehaviour {
    public int number2Destroy { set; get; }
    public bool next2Destory { set; get; }
    public bool isDestory { set; get; }
    public GameObject nextSnow { set; get; }
    public int size { set; get; }
    public bool isCoin { set; get; }
    public GameObject text;
    // Use this for initialization

    private void Update()
    {
        if (StartGame.game.gameOver)
        {
            Destroy(gameObject);
        }
    }


    private void OnMouseDown()
    {
        //TODO check if the object should be click

        if (isCoin)
        {
            
        }
        if (!StartGame.game.gameOver)
        {
            Debug.Log(number2Destroy);
            if (nextSnow != null)
            {
                nextSnow.GetComponent<Snow>().next2Destory = true;
            }
            StartGame.game.setNextDestroy(gameObject);
            //TODO destroy animation
            Destroy(gameObject);
        }
    }
}
