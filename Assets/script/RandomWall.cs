using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random; 

public class RandomWall : MonoBehaviour
{
  bool IsPlayerInRange = true;
     Vector3  currentPlayerPosition  ;
     Vector3  lastPlayerPOsition  ; 
  
void Update() 
{
    
       currentPlayerPosition = transform.position;
     
    if(Mathf.Round(currentPlayerPosition.y) != Mathf.Round(lastPlayerPOsition.y))
   { 
     
      
       SetWall();
   
   }

   lastPlayerPOsition = currentPlayerPosition;
    
}


void MakeWall( GameObject wall ,Vector3 position )
{
    Vector3 newPos = new Vector3 (Mathf.Round(position.x) ,Mathf.Round(position.y+1),0 );
        
       
     
       foreach (GameObject i in GameManager.exceptWall)
        {
          if(i.transform.position == newPos )
           {
             newPos.x+=1;
           }
       }
   if (  IsPlayerInRange && (Mathf.Round(position.y+1) < 7 )&& Enemy.enemyMove )
        { 
         
         GameObject instantiate = Instantiate(wall,newPos, Quaternion.identity);
            
         GameManager.wall.Add(instantiate);
              
        }
}
GameObject choiceWall ()
{
  GameObject gameManager = GameObject.Find("GameManager");
  BoardManager  BoardManager =gameManager.GetComponent<BoardManager>();
  GameObject tileChoice = BoardManager.wallTiles[Random.Range (0, BoardManager.wallTiles.Length)];
 return tileChoice;
}
 
void ComparePosition(Vector3 position)
{
 
  Vector3 position1;
  IsPlayerInRange = true;
 
  for(int j = 0 ; j <GameManager.wall.Count ; j++)
  {  
  
    
    for (int i=0 ; i<=8 ;i++)
   {
      
       position1= new Vector3(i , Mathf.Round(position.y)+1f );
       
       float dist = Vector3.Distance(position1,GameManager.wall[j].transform.position);
  
       if (Mathf.Round(dist) <= 0)
       {
         IsPlayerInRange = false;
       return;
         
       } 
   }

  }
 }
 

 void SetWall ()
 {
        
 ComparePosition(currentPlayerPosition);
        GameObject wall = choiceWall();
        MakeWall( wall ,currentPlayerPosition);
 } 
}
