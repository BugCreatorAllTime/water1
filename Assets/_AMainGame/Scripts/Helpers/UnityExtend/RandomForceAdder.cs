using FMod;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class RandomForceAdder : MonoBehaviour
{
    [SerializeField]
    private RandomFloat randomForceMagnitude;
    [SerializeField]
    private RandomFloat randomTorque;

    public void AddRandomForce()
    {
        GetComponent<Rigidbody2D>().AddForce(Random.insideUnitCircle * randomForceMagnitude, ForceMode2D.Impulse);
    }

    public void AddRandomTorque()
    {
        GetComponent<Rigidbody2D>().AddTorque(randomTorque, ForceMode2D.Impulse);
    }
}
