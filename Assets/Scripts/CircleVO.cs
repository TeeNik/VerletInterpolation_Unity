using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class CircleVO : VerletObject
{
    public override void Init(Vector3 initialPos, float radius)
    {
        base.Init(initialPos, radius);

        transform.position = initialPos;
        RectTransform rect = GetComponent<RectTransform>();
        rect.sizeDelta = new Vector2(radius, radius) * 2.0f;
    }

    public override void UpdatePosition(float deltaTime)
    {
        base.UpdatePosition(deltaTime);
        transform.position = PositionCurrent;
    }
}
