using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleAvoidanceBehavior : MonoBehaviour
{
    public float moveSpeed = 5f; // Velocidad de movimiento del agente
    public float avoidanceForce = 5f; // Fuerza de evitación de obstáculos
    public float raycastDistance = 5f; // Distancia máxima de los raycasts para detectar obstáculos
    public float fieldOfViewAngle = 90f; // Ángulo de visión periférica del agente
    public LayerMask obstacleLayer; // Capa que contiene los obstáculos

    void Update()
    {
        // Calcula la dirección de movimiento deseada (inicialmente hacia adelante)
        Vector3 desiredDirection = transform.forward;

        // Realiza varios raycasts en diferentes direcciones dentro del campo de visión periférica
        RaycastHit hit;
        for (float angle = -fieldOfViewAngle / 2f; angle <= fieldOfViewAngle / 2f; angle += 10f)
        {
            Vector3 raycastDirection = Quaternion.Euler(0f, angle, 0f) * transform.forward;
            if (Physics.Raycast(transform.position, raycastDirection, out hit, raycastDistance, obstacleLayer))
            {
                // Calcula una dirección de evitación perpendicular al obstáculo
                Vector3 avoidanceDirection = Vector3.Cross(hit.normal, Vector3.up).normalized;
                desiredDirection += avoidanceDirection * avoidanceForce;
            }
        }

        // Normaliza la dirección deseada
        desiredDirection.Normalize();

        // Aplica movimiento al agente en la dirección deseada
        transform.position += desiredDirection * moveSpeed * Time.deltaTime;

        // Rotación suave hacia la dirección de movimiento deseada
        Quaternion targetRotation = Quaternion.LookRotation(desiredDirection, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime);
    }
}
