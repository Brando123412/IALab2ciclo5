using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleAvoidanceBehavior : MonoBehaviour
{
    public float moveSpeed = 5f; // Velocidad de movimiento del agente
    public float avoidanceForce = 5f; // Fuerza de evitaci�n de obst�culos
    public float raycastDistance = 5f; // Distancia m�xima de los raycasts para detectar obst�culos
    public float fieldOfViewAngle = 90f; // �ngulo de visi�n perif�rica del agente
    public LayerMask obstacleLayer; // Capa que contiene los obst�culos

    void Update()
    {
        // Calcula la direcci�n de movimiento deseada (inicialmente hacia adelante)
        Vector3 desiredDirection = transform.forward;

        // Realiza varios raycasts en diferentes direcciones dentro del campo de visi�n perif�rica
        RaycastHit hit;
        for (float angle = -fieldOfViewAngle / 2f; angle <= fieldOfViewAngle / 2f; angle += 10f)
        {
            Vector3 raycastDirection = Quaternion.Euler(0f, angle, 0f) * transform.forward;
            if (Physics.Raycast(transform.position, raycastDirection, out hit, raycastDistance, obstacleLayer))
            {
                // Calcula una direcci�n de evitaci�n perpendicular al obst�culo
                Vector3 avoidanceDirection = Vector3.Cross(hit.normal, Vector3.up).normalized;
                desiredDirection += avoidanceDirection * avoidanceForce;
            }
        }

        // Normaliza la direcci�n deseada
        desiredDirection.Normalize();

        // Aplica movimiento al agente en la direcci�n deseada
        transform.position += desiredDirection * moveSpeed * Time.deltaTime;

        // Rotaci�n suave hacia la direcci�n de movimiento deseada
        Quaternion targetRotation = Quaternion.LookRotation(desiredDirection, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime);
    }
}
