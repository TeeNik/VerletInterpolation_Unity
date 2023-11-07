using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.Video;

public class VerletSolver : MonoBehaviour
{
    public VerletObject ObjectPrefab;


    protected List<VerletObject> Objects;

    protected int NumOfObjects = 8;
    protected float Time = 0;

    public VerletObject SpawnObject(Vector3 position, float radius)
    {
        VerletObject Object = Instantiate(ObjectPrefab, position, Quaternion.identity) as VerletObject;
        Objects.Add(Object);
        return Object;
    }

    public void UpdateSolver(float deltaTime)
    {
        Time += deltaTime;
        const int subSteps = 8;
        float subDeltaTime = deltaTime / subSteps;

        for (int i = 0; i < subSteps; ++i)
        {
            ApplyGravity();
            SolveCollisions();
            ApplyConstraints();
            UpdateObjects(subDeltaTime);
            UpdateLinks(subDeltaTime);
        }
    }

    protected void ApplyGravity()
    {
        Vector3 gravity = new Vector3(0, 1000, 0);
        foreach (var obj in Objects)
        {
            obj.Accelerate(gravity);
        }
    }

    protected void ApplyConstraints()
    {

    }

    protected void SolveCollisions()
    {
        const float responseCoef = 0.75f;

        for (int i = 0; i < Objects.Count; ++i)
        {
            VerletObject co1 = Objects[i];
            for (int j = i + 1; j < Objects.Count; ++j)
            {
                VerletObject co2 = Objects[j];

                Vector3 diff = co1.PositionCurrent - co2.PositionCurrent;
                float dist = diff.magnitude;
                float sumRad = co1.Radius + co2.Radius;
                if (dist < sumRad)
                {
                    Vector3 n = diff / dist;
                    float delta = 0.5f * responseCoef * (dist - sumRad);

                    float massRatio1 = co1.Radius / (co1.Radius + co2.Radius);
                    float massRatio2 = co2.Radius / (co1.Radius + co2.Radius);

                    co1.PositionCurrent -= n * massRatio2 * delta;
                    co2.PositionCurrent += n * massRatio1 * delta;
                }
            }
        }
    }

    protected void UpdateObjects(float deltaTime)
    {
        foreach (var obj in Objects)
        {
            obj.UpdatePosition(deltaTime);
        }
    }

    protected void UpdateLinks(float deltaTime)
    {

    }


}
