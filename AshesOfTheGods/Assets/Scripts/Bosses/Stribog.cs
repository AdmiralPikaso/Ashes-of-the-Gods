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
            transitToPhase2(StartPos);
    }

    public bool inTransit = false;
    private float flyuplength = 6;
    private float flySpeed = 20;
    private Vector2 StartPos;

    private bool nowFlyup = true;
    private bool nowFlyTocenter = false;
    private bool nowFlyUp2 =false;
    private float realSpeed = 1;
    private void transitToPhase2(Vector2 StartPos)
    {
        void flyup(Vector2 StartPos)
        {
            if (realSpeed < flySpeed)
                realSpeed = realSpeed * 1.5f;
            if (StartPos.y + flyuplength > transform.position.y)
                rigidB.MovePosition(new Vector2(rigidB.position.x, rigidB.position.y + realSpeed * Time.fixedDeltaTime * 2));
            else
            {
                nowFlyup = false;
                nowFlyTocenter = true;
                rigidB.rotation = -30 * math.sign(centerArenaX - rigidB.position.x);
                realSpeed = 2;
            }
        }
        void flytocenter()
        {
            print(realSpeed);
            if (realSpeed < flySpeed)
                realSpeed = realSpeed + 1;
            if (math.distance(rigidB.position.x, centerArenaX) > 0.5f)
            {
                rigidB.MovePosition(new Vector2(rigidB.position.x + realSpeed * Time.fixedDeltaTime * math.sign(centerArenaX - rigidB.position.x), rigidB.position.y));
            }
            else
            {
                nowFlyTocenter = false;
                nowFlyUp2 = true;
                rigidB.rotation = 0;
                StartPos = transform.position;
            }
        }
        void flyup2(Vector2 StartPos)
        {
            //here we need start animation to phase2 sprite
            if (transform.position.y < StartPos.y + 5)
                rigidB.MovePosition(new Vector2(rigidB.transform.position.x, rigidB.transform.position.y + Time.fixedDeltaTime));

            else
            {
                phase1 = false;
                phase2 = true;
            }
        }
        print($"{nowFlyup}, {nowFlyTocenter}, {nowFlyUp2}");
        if (nowFlyup)
            flyup(StartPos);
        else if (nowFlyTocenter)
        {
            flytocenter();
        }
        else if (nowFlyUp2)
            flyup2(StartPos);
    }
    public float GetHp() => hp;
}

