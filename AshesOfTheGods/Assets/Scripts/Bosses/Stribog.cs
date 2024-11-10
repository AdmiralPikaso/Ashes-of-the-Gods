using System.Reflection;
using Unity.VisualScripting;
using System.Collections;
using UnityEngine;

public class Stribog : Enemy
{
    private bool phase1, phase2;
    private Rigidbody2D rigidB;
    public float MaxHp = 300;

    private Transform PlayerTransform;

    void Start()
    {
        StartCoroutine(Transit());
    }
    void Awake()
    {
        rigidB = GetComponent<Rigidbody2D>();
        PlayerTransform = GameObject.FindWithTag("Player").GetComponent<Transform>();
        phase1 = true;
        phase2 = false;
        hp = 300;
    }
    void Update()
    {
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

    private bool inTransit = false;
    private void transits()
    {
        transform.position = PlayerTransform.position + new Vector3(0, 5, 0);
        inTransit = true;
    }
    public float GetHp() => hp;

    private IEnumerator Transit()
    {
        while (true)
        {
            if (inTransit)
            {

            }
            yield return null;
        }
    }
}
