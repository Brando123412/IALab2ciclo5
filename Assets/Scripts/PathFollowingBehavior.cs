using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFollowingBehavior : MonoBehaviour
{
    public List<Transform> waypoints = new List<Transform>(); // Lista de puntos de la ruta
    public float moveSpeed = 5f; // Velocidad de movimiento del agente
    public float rotationSpeed = 5f; // Velocidad de rotación del agente
    public float waypointRadius = 0.5f; // Radio de proximidad al waypoint para considerarlo alcanzado
    public float minWaypointDistance = 1f; // Distancia mínima para pasar al siguiente waypoint
    public bool loopPath = false; // Indica si se debe seguir la ruta en bucle

    private int currentWaypointIndex = 0; // Índice del waypoint actual

    void Start()
    {
        if (waypoints.Count == 0)
        {
            Debug.LogError("No se han asignado waypoints.");
            enabled = false;
            return;
        }
    }

    void Update()
    {
        Vector3 direction = waypoints[currentWaypointIndex].position - transform.position;

        Quaternion lookRotation = Quaternion.LookRotation(direction, transform.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);

        if (direction.magnitude < waypointRadius)
        {
            if (loopPath || currentWaypointIndex < waypoints.Count - 1)
            {
                currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Count;
            }
            else
            {
                return;
            }
        }
        transform.Translate(transform.forward * moveSpeed * Time.deltaTime, Space.World);
    }
    void OnDrawGizmos()
    {
        if (waypoints.Count < 2)
            return;

        Gizmos.color = Color.blue;

        for (int i = 0; i < waypoints.Count; i++)
        {
            Gizmos.DrawSphere(waypoints[i].position, waypointRadius);

            if (i < waypoints.Count - 1)
            {
                Gizmos.DrawLine(waypoints[i].position, waypoints[i + 1].position);
            }
            else if (loopPath)
            {
                Gizmos.DrawLine(waypoints[i].position, waypoints[0].position);
            }
        }
    }
}
