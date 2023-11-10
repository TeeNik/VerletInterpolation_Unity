using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerletObject : MonoBehaviour
{

    public float Radius = 50.0f;
    public Vector3 PositionCurrent;

    protected Vector3 PositionOld;
    protected Vector3 Acceleration;

    public void Init(Vector3 initialPos, float radius)
    {
        PositionCurrent = initialPos;
        PositionOld = initialPos;
        Radius = radius;
    }

    public void UpdatePosition(float deltaTime)
    {
        Vector3 Velocity = PositionCurrent - PositionOld;
        PositionOld = PositionCurrent;
        PositionCurrent = PositionCurrent + Velocity + Acceleration * deltaTime * deltaTime;
        Acceleration = Vector3.zero;

        //TODO move to upper level
        transform.position = PositionCurrent;
    }

    public void Accelerate(Vector3 acc)
    {
        Acceleration += acc;
    }

    public Vector3 GetVelocity(float deltaTime)
    {
        return (PositionCurrent - PositionOld) / deltaTime;
    }

    public void SetVelocity(Vector3 Velocity, float deltaTime)
    {
        PositionOld = PositionCurrent - (Velocity * deltaTime);
    }

}
