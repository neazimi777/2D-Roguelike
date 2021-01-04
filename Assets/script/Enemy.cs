using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MovingObject
{
    public int playerDamage; 						
    public AudioClip attackSound1;						
    public AudioClip attackSound2;						
		
		
    private Animator animator;							
    private Transform target;						
    public static bool skipMove;							
		
		
		
    protected override void Start ()
    {
			
        GameManager.instance.AddEnemyToList (this);
			
			
        animator = GetComponent<Animator> ();
			
			
        target = GameObject.FindGameObjectWithTag ("Player").transform;
			
			
        base.Start ();
    }
		
		
		
    protected override void AttemptMove <T> (int xDir, int yDir)
    {
			
        if(skipMove)
        { 
          
            skipMove = false;
          // Debug.Log("skipMove"+skipMove);
            return;
				
        }
			
		
        base.AttemptMove <T> (xDir, yDir);	
        skipMove = true;
   // Debug.Log("skipMove"+skipMove);
    }
		
		
		
    public void MoveEnemy ()
    {
       
			
        int xDir = 0;
        int yDir = 0;
			
			
        if(Mathf.Abs (target.position.x - transform.position.x) < float.Epsilon)
				
		
            yDir = target.position.y > transform.position.y ? 1 : -1;
			
		
        else
				
            xDir = target.position.x > transform.position.x ? 1 : -1;
			
			
        AttemptMove <Player> (xDir, yDir);
    }
		
		
		
    protected override void OnCantMove <T> (T component)
    {
		
        Player hitPlayer = component as Player;
			
			
        hitPlayer.LoseFood (playerDamage);
			
			
        animator.SetTrigger ("enemyAttack");
			
			
        SoundManager.instance.RandomizeSfx (attackSound1, attackSound2);
    }
}