using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour {
    public static StartGame game;


    private int nextAmountofSnow2Create;
    public Sprite[] sprites;

    private GameObject prevSnow;
    private int points;

    public GameObject snowPrefab;
    public GameObject score;
    public GameObject screen;
    public GameObject coinPrefab;
    public int coins;

    private Animator point_animator;
    private Animator screen_animator;

    public bool gameOver;

    

    private void Awake()
    {
        if (game == null)
            game = this;
       else if (game != this)
            Destroy(game);
      //  DontDestroyOnLoad(game);
    }
    
    // Use this for initialization
    void Start () {
        GameController.Load();
        coins = GameController.instance.coins;
        gameOver = false;
        points = 0;
        nextAmountofSnow2Create = 2;
        prevSnow = Instantiate(snowPrefab, new Vector3(0, 7, 0), Quaternion.identity);
        prevSnow.GetComponent<Snow>().next2Destory = true;
        point_animator = score.GetComponent<Animator>();
        screen_animator = screen.GetComponent<Animator>();;
    }
	
	// Update is called once per frame
	void Update () {
        if (!gameOver)
        {
            StartCoroutine(waitOnCreate());
            score.GetComponent<Text>().text = points.ToString();
        }
        

    }


    private bool createSnow = true;
    IEnumerator waitOnCreate()
    {
        if (createSnow)
        {
            createSnow = false;
            yield return new WaitForSeconds(5f);
            StartCoroutine(createSnows());

            createSnow = true;
        }
    }

    private float randScale;
    private int randSize;
    private int randColor;
   // private Sprite randSprite;
    IEnumerator createSnows()
    {
        randColor = Random.Range(0, 2);

        
        //randSprite = sprites[Random.Range(0, sprites.Length)];
        for (int i = 0; i < nextAmountofSnow2Create; i++)
        {
            randScale = Random.Range(0.2f, 0.3f);
            randSize = Random.Range(1, 200);

            GameObject snow = Instantiate(snowPrefab, new Vector3(0,7,0), Quaternion.identity);
            snow.transform.localScale = new Vector3(randScale, randScale, 0);
            //snow.GetComponent<SpriteRenderer>().sprite = randSprite;

            if (randColor == 0)
            {
                //Debug.Log("white");
                snow.GetComponent<SpriteRenderer>().color = Color.white;
            }

            if (prevSnow != null)
            {
                snow.GetComponent<Snow>().text.GetComponent<Text>().text = (prevSnow.GetComponent<Snow>().size + randSize).ToString();
                snow.GetComponent<Snow>().size = prevSnow.GetComponent<Snow>().size + randSize;
                prevSnow.GetComponent<Snow>().nextSnow = snow;
            }
            else
            {
                snow.GetComponent<Snow>().size = randSize;
                snow.GetComponent<Snow>().next2Destory = true;
                snow.GetComponent<Snow>().text.GetComponent<Text>().text = randSize.ToString();
            }
            prevSnow = snow;
            yield return new WaitForSeconds(.5f);

        }
        nextAmountofSnow2Create++;
    }

    public void setNextDestroy(GameObject snow)
    {
        Snow s = snow.GetComponent<Snow>();
        if(!s.next2Destory)
        {
            gameOver = true;
            Debug.Log("game end");
            if (GameController.instance.highest_scores < points)
            {
                GameController.instance.highest_scores = points;
            }
            GameController.instance.scores = points;
            GameController.Save();

            SceneManager.LoadScene(2, LoadSceneMode.Additive);
            return;
        }
        points++;
        point_animator.SetTrigger("add");
        screen_animator.SetTrigger("shake");
    }

    
}
