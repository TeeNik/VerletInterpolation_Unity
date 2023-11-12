using System.Collections.Generic;
using UnityEngine;

public class VerletSolver : MonoBehaviour
{
    public VerletObject ObjectPrefab;
    public Constraint Constraint = new RectConstraint();
    public Transform Parent;
    public Vector3 Gravity = new Vector3(0, 1000, 0);

    protected List<VerletObject> Objects = new List<VerletObject>();
    protected List<Connection> Connections = new List<Connection>();


    public VerletObject CreateObject(Vector3 position, float radius)
    {
        VerletObject Object = Instantiate(ObjectPrefab, position, Quaternion.identity, Parent) as VerletObject;
        Object.Init(position, radius);
        Objects.Add(Object);
        return Object;
    }

    public void CreateConnection(VerletObject o1, VerletObject o2)
    {
        var connection = new Connection(o1, o2, (o2.PositionCurrent - o1.PositionCurrent).magnitude);
        Connections.Add(connection);
    }

    public void UpdateSolver(float deltaTime)
    {
        const int subSteps = 8;
        float subDeltaTime = deltaTime / subSteps;

        for (int i = 0; i < subSteps; ++i)
        {
            ApplyGravity();
            SolveCollisions();
            ApplyConstraints();
            UpdateObjects(subDeltaTime);
            UpdateConnections(subDeltaTime);
        }
    }

    protected void ApplyGravity()
    {
        foreach (var obj in Objects)
        {
            obj.Accelerate(Gravity);
        }
    }

    protected void ApplyConstraints()
    {
        foreach (var obj in Objects)
        {
            Constraint.ApplyConstraint(obj);
        }
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

    protected void UpdateConnections(float deltaTime)
    {
        foreach (var connection in Connections)
        {
            Vector3 delta = connection.O1.PositionCurrent - connection.O2.PositionCurrent;
            float current = delta.magnitude;
            float f = (current - connection.Length) / current;
            connection.O1.PositionCurrent -= f * 0.5f * delta;
            connection.O2.PositionCurrent += f * 0.5f * delta;
        }
    }


}
