using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random; 

public class RandomWall : MonoBehaviour
{
  bool IsPlayerInRange = true;
  Vector3  m_playerPosition;
 
void Update() 
{
 

 m_playerPosition = transform.position;
  ComparePosition(m_playerPosition, GameManager.currentObj);
 GameObject wall = choiceWall();
 
MakeWallFromCompareTime( wall ,m_playerPosition);
 
}


void MakeWallFromCompareTime( GameObject wall ,Vector3 position )
{

   if (Enemy.skipMove && IsPlayerInRange )
        { 
           
          Instantiate(wall,  new Vector3 (position.x +=1 ,position.y,position.z)  , Quaternion.identity);
            
        }
  }
GameObject choiceWall ()
{
  GameObject gameManager = GameObject.Find("GameManager");
  BoardManager  BoardManager =gameManager.GetComponent<BoardManager>();
  GameObject tileChoice = BoardManager.wallTiles[Random.Range (0, BoardManager.wallTiles.Length)];
 return tileChoice;
}
 
void ComparePosition(Vector3 position, GameObject[] currentObj )
{
Vector3 position1 = new Vector3 (position.x +=1 ,position.y,position.z);
Vector3 position2;
float dist1;
float dist2=1;


  foreach(GameObject go in currentObj)
  {
     dist1 = Vector3.Distance(position1 , go.transform.position);

    if ( go.CompareTag("wall"))
    {
       for(int i = 1 ; i<=8 ;i++ )
       { 
         position2 = new Vector3 (position.x +=i ,position.y+=1,position.z);
         dist2 = Vector3.Distance(position2 , go.transform.position);
     // Debug.Log("dist2" + dist2);
       
   if ((dist1 <= 0) && (dist2 <= 0))
   {
      print ("ifff==");
    IsPlayerInRange = false;
    return ;
}
}
   

  }
  }

 }
} 
