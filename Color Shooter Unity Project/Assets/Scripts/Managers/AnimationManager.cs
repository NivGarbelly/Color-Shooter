using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
   public List<Animation> Walls = new List<Animation>();
   public List<Animation> Trophies = new List<Animation>();
   public List<Animation> Enemies = new List<Animation>();
   public List<Animation> Players= new List<Animation>();

   private void Awake() 
   {
        foreach (var trophyObj in GameObject.FindGameObjectsWithTag("Trophies"))
        {
            Trophies.Add(trophyObj.GetComponentInChildren<Animation>());
        }

        foreach (var enemyObj in GameObject.FindGameObjectsWithTag("Enemies"))
        {
             Enemies.Add(enemyObj.GetComponentInChildren<Animation>());
        }
        
         foreach (var wallsObj in GameObject.FindGameObjectsWithTag("Walls"))
        {
             Walls.Add(wallsObj.GetComponentInChildren<Animation>());
        }
        
         foreach (var playerObj in GameObject.FindGameObjectsWithTag("Player"))
        {
             Players.Add(playerObj.GetComponentInChildren<Animation>());
        }    
   }
   public void AnimateWalls()
   {
     foreach (var wallsAnim in Walls)
     {
          wallsAnim.CrossFade("CreateWall");
     }
   }
   public void AnimteEnemiesAndTrophies()
   {
     foreach (var enemiesAnim in Enemies)
     {
          //enemiesAnim.CrossFade("CreateEnemy");
     }
     foreach (var trophiesAnim in Trophies)
     {
          trophiesAnim.CrossFade("CreateTrophy");
     }
   }
   public void AnimatePlayer()
   {
     foreach (var playerAnim in Players)
     {
          playerAnim.CrossFade("CreatePlayer");
     }
   }
}
