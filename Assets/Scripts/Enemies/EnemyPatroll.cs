using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyPatroll : MonoBehaviour
{
    public GameObject Player;
    public float Speed;
    private float Distance;
    public float DistanceBetween; //change it from editor
     void Start()
    {


    }
   void Update()
    {
        Distance = Vector2.Distance(transform.position, Player.transform.position); //find distance between player and enemy (transform has scale and position of gameobject)
        Vector2 Direction = Player.transform.position - transform.position; //var to set the direction the enemy has to go to
        Direction.Normalize(); //won't have bugs when assign rotation
        float Angle = Mathf.Atan2(Direction.y, Direction.x) * Mathf.Rad2Deg; //find angle between two points---- convert from radiant to degree

        

        //distance within the enemy will detect the player and chase 
        if(Distance < DistanceBetween)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, Player.transform.position, Speed * Time.deltaTime); //(Position enemy, TargetJoint2D, speed)
            transform.rotation = Quaternion.Euler(Vector3.forward * Angle);
        }
    }

}

