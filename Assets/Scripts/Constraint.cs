using UnityEngine;

public abstract class Constraint
{
    public abstract void ApplyConstraint(VerletObject obj);
}

public class CircleRect : Constraint
{
    public Vector3 Position;
    public float Radius;

    public override void ApplyConstraint(VerletObject obj)
    {
        Vector3 toObj = Position - obj.PositionCurrent;
        float dist = toObj.magnitude;
        if (dist > Radius - obj.Radius)
        {
            Vector3 n = toObj / dist;
            obj.PositionCurrent = Position - n * (Radius - obj.Radius);
        }
    }
}

public class RectConstraint : Constraint
{
    public Vector3 Position;
    public Vector3 Bounds;

    public RectConstraint()
    {
        Bounds = new Vector3(Screen.width * 0.5f, Screen.height * 0.5f);
        Position = Bounds;
    }

    public override void ApplyConstraint(VerletObject obj)
    {
        Vector3 min = Position - Bounds;
        Vector3 max = Position + Bounds;

        Vector3 objPos = obj.PositionCurrent;
        Vector3 newPos = obj.PositionCurrent;
        float objRad = obj.Radius;

        if (min.x > objPos.x - objRad)
        {
            float dist = objPos.x - min.x;
            obj.PositionCurrent.x += objRad - dist;
        }
        else if (max.x < objPos.x + objRad)
        {
            float dist = max.x - objPos.x;
            obj.PositionCurrent.x -= objRad - dist;
        }
        if (min.y > objPos.y - objRad)
        {
            float dist = objPos.y - min.y;
            obj.PositionCurrent.y += objRad - dist;
        }
        else if (max.y < objPos.y + objRad)
        {
            float dist = max.y - objPos.y;
            obj.PositionCurrent.y -= objRad - dist;
        }
    }
}
