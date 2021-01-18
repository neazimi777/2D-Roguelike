using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;	

public class GameManager : MonoBehaviour
{
	public float levelStartDelay = 2f; 
	public float turnDelay = 0.1f; 
	public int playerFoodPoints = 100;
    public static GameManager instance = null;
	[HideInInspector] public bool playersTurn = true;

	private Text levelText; 
	private Text timer; 
	private GameObject levelImage; 
	private BoardManager boardScript; 
	private int level = 1; 
	private List<Enemy> enemies; 
	private bool enemiesMoving; 
	private float currentTime ; 
    static public  List<GameObject> wall = new List<GameObject>();
    static public  List<GameObject> exceptWall  = new List<GameObject>();
                  
	private bool doingSetup = true;



	void Awake()
	{
		
		if (instance == null)

			
			instance = this;

		
		else if (instance != this)

			Destroy(gameObject);

		
		DontDestroyOnLoad(gameObject);

	
		enemies = new List<Enemy>();

		
		boardScript = GetComponent<BoardManager>();
		
		
	}

	 [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
	static public void CallbackInitialization()
	{
       
		SceneManager.sceneLoaded += OnSceneLoaded;
	    
	    instance.InitGame();
	}
	
	 static private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
        { 
	       
            instance.level++;
             wall.Clear();
             exceptWall.Clear();
    
            instance.InitGame();
           

        }


	
	void InitGame()
	{
        
		doingSetup = true;
	
		levelImage = GameObject.Find("LevelImage");

		
		levelText = GameObject.Find("LevelText").GetComponent<Text>();

		
		levelText.text = "Day " + level;

		
		levelImage.SetActive(true);
  
	
		Invoke("HideLevelImage", levelStartDelay);

	  
		enemies.Clear();

		
		boardScript.SetupScene(level);

      	currentTime  = SetTime(level);
      wall.AddRange(GameObject.FindGameObjectsWithTag("wall"));
     exceptWall.AddRange(GameObject.FindGameObjectsWithTag("Food"));
     exceptWall.AddRange(GameObject.FindGameObjectsWithTag("Soda")); 
      exceptWall.AddRange(GameObject.FindGameObjectsWithTag("Enemy")); 
		
	}


	
	void HideLevelImage()
	{
      
		levelImage.SetActive(false);

		
		doingSetup = false;
	}


	void Update()
	{ 
     

     StartCoroutine( DisplayTime());
         

		if (playersTurn || enemiesMoving || doingSetup)

			
			return;

		
		StartCoroutine(MoveEnemies());

    

	}

	public void AddEnemyToList(Enemy script)
	{
		
		enemies.Add(script);
	}


	
	public void GameOver()
	{
		levelText.text = "After " + level + " days, you starved.";
		levelImage.SetActive(true);
         
		
	}

	IEnumerator MoveEnemies()
	{
	
		enemiesMoving = true;

	
		yield return new WaitForSeconds(turnDelay);

		if (enemies.Count == 0)
		{
			
			yield return new WaitForSeconds(turnDelay);
		}

		for (int i = 0; i < enemies.Count; i++)
		{
			
			enemies[i].MoveEnemy();

			
			yield return new WaitForSeconds(enemies[i].moveTime);
		}

		
		playersTurn = true;

	
		enemiesMoving = false;
	}

 int SetTime (int level)
{
   int time = 0 ;
 
    if(level <= 5)
      {  
        time = level * 10;
       }
    else 
   {
      time = 60 ; 
   }

 return time;
}
IEnumerator DisplayTime()
{
 yield return new WaitForSeconds(levelStartDelay);

  currentTime -=  Time.deltaTime;
 
    if(Mathf.Round(currentTime) == 0)
          { 
             
             GameOver();
          }

      if ( currentTime<=3)
       {
           timer.color = Color.red;
            
        }
       
      timer = GameObject.Find("Timer").GetComponent<Text>();
      timer.text = Mathf.Round(currentTime).ToString();
       
 
}

 
}
 
     
	