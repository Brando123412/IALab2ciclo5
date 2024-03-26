using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekBehavior : MonoBehaviour
{
    public Transform target; // El objeto al que queremos llegar
    public float maxSpeed = 5f; // Velocidad máxima del objeto
    public float maxSpeedRotation = 5f; // Velocidad máxima del objeto
    void Update()
    {
        Vector3 targetDirection = target.position - transform.position;

        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(targetDirection.normalized), Time.deltaTime * maxSpeedRotation);

        transform.position += transform.forward * Time.deltaTime * maxSpeed;

    }
}
