using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Connection : MonoBehaviour
{
    public VerletObject O1;
    public VerletObject O2;
    public float Length;

    public Connection(VerletObject o1, VerletObject o2, float length)
    {
        O1 = o1;
        O2 = o2;
        Length = length;
    }
}
