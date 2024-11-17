using System.Reflection;
using Unity.VisualScripting;
using System.Collections;
using UnityEngine;
using System.Runtime.CompilerServices;
using NUnit.Framework.Constraints;
using Unity.Mathematics;

public class Stribog : Enemy
{
    private bool phase1, phase2;
    private Rigidbody2D rigidB;
    public float MaxHp = 300;
    private Transform PlayerTransform;
    private float centerArenaX;
    private float groundLevelY;
    void Awake()
    {
        groundLevelY = GameObject.FindGameObjectWithTag("Ground").transform.position.y + GameObject.FindGameObjectWithTag("Ground").transform.localScale.y; 
        rigidB = GetComponent<Rigidbody2D>();
        PlayerTransform = GameObject.FindWithTag("Player").GetComponent<Transform>();
        phase1 = true;
        phase2 = false;
        hp = 300;
        centerArenaX = GameObject.Find("ArenaWalls").transform.position.x;
    }
    void Update()
    {
        if (!inTransit)
        {
            StartPos = transform.position;
        }
        if (phase1)
        {

        }
        if (phase2)
        {

        }
        if (hp <= 150)
        {
            phase1 = false;
            phase2 = true;
        }
    }
    void FixedUpdate()
    {
        if (inTransit)
            transits(StartPos);
    }

    public bool inTransit = false;
    private float flyuplength = 6;
    private float flySpeed = 20;
    private Vector2 StartPos;

    private bool nowFlyup = true;
    private bool nowFlyTocenter = false;
    private bool nowCirclingAboveCenter = false;
    private void transits(Vector2 StartPos)
    {

        void flyup(Vector2 StartPos)
        {
            if (StartPos.y + flyuplength > transform.position.y)
                rigidB.MovePosition(new Vector2(rigidB.position.x, rigidB.position.y + flySpeed * Time.fixedDeltaTime * 2));
            else
            {
                nowFlyup = false;
                nowFlyTocenter = true;
                rigidB.rotation = -30 * math.sign(centerArenaX - rigidB.position.x);
            }
        }
        void flytocenter()
        {

            if (math.distance(rigidB.position.x, centerArenaX) > 0.01f)
            {
                rigidB.MovePosition(new Vector2(rigidB.position.x + flySpeed * Time.fixedDeltaTime * math.sign(centerArenaX - rigidB.position.x), rigidB.position.y));
            }
            else
            {
                nowFlyTocenter = false;
                nowCirclingAboveCenter = true;
                rigidB.rotation = 0;

            }
        }
        void circlingAboveCenter()
        {

        }
        print($"{nowFlyup}, {nowFlyTocenter}, {nowCirclingAboveCenter}");
        print(StartPos);
        if (nowFlyup)
            flyup(StartPos);
        else if (nowFlyTocenter)
            flytocenter();
        else if (nowCirclingAboveCenter)
            circlingAboveCenter();
    }
    public float GetHp() => hp;

    /* private IEnumerator Transit()
     {
         while (true)
         {
             if (inTransit)
             {

             }
             yield return null;
         }
     }*/
}
