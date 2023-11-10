using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Level : MonoBehaviour
{
    public VerletSolver VerletSolver;
    public int MaxNumOfObjects = 8;
    public float SpawnCooldown = 1f;

    private float CurrentSpawnTime = 0.0f;
    private int NumOfObjects = 0;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CurrentSpawnTime += Time.fixedDeltaTime;

        if (CurrentSpawnTime > SpawnCooldown && NumOfObjects < MaxNumOfObjects)
        {
            CurrentSpawnTime = 0.0f;
            ++NumOfObjects;

            float radius = Random.Range(10, 50);
            VerletObject obj = VerletSolver.SpawnObject(new Vector3(Screen.width / 2, Screen.height / 2, 0), radius);

            float angle = 30.0f * Mathf.Sin(Time.time) + Mathf.PI / 2;
            obj.SetVelocity(new Vector3(Mathf.Cos(angle), Mathf.Sin(angle) * 100), Time.fixedDeltaTime);
        }

        VerletSolver.UpdateSolver(Time.fixedDeltaTime);
    }
}
