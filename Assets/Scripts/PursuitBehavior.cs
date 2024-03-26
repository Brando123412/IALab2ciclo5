using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PursuitBehavior : MonoBehaviour
{
    public Transform target; 
    public float speed = 5f; 

    void Update()
    {
        Vector3 targetPosition = target.position + target.GetComponent<Rigidbody>().velocity;
        Vector3 direction = targetPosition - transform.position;
        direction.Normalize();

        // Calcula la distancia que el agente debería moverse en este frame
        float distanceToMove = speed * Time.deltaTime;

        // Calcula el desplazamiento necesario en esta dirección
        Vector3 displacement = direction * distanceToMove;

        // Mueve al agente hacia la posición futura del objetivo
        transform.position += displacement;
    }
}
