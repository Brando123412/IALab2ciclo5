using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderBehavior : MonoBehaviour

{
    public float moveSpeed = 3f; // Velocidad de movimiento del objeto
    public float rotationSpeed = 100f; // Velocidad de rotación del objeto
    public float changeDirectionTime = 2f; // Tiempo entre cambios de dirección
    public float maxAngleChange = 45f; // Máximo cambio de ángulo permitido
    public float obstacleAvoidanceDistance = 2f; // Distancia para evitar obstáculos

    private float timer; // Temporizador para cambiar la dirección
    private Vector3 randomDirection; // Dirección aleatoria para moverse

    void Start()
    {
        timer = changeDirectionTime;
        GetNewRandomDirection();
    }

    void Update()
    {
        // Mueve el objeto en la dirección aleatoria
        transform.Translate(randomDirection * moveSpeed * Time.deltaTime);

        // Rotación suave hacia la dirección de movimiento
        Quaternion lookRotation = Quaternion.LookRotation(randomDirection);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);

        // Actualiza el temporizador
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            GetNewRandomDirection();
            timer = changeDirectionTime;
        }
        Debug.DrawRay(transform.position, randomDirection * obstacleAvoidanceDistance, Color.red);
        // Detección de obstáculos
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, obstacleAvoidanceDistance))
        {

            if (hit.collider.CompareTag("Obstacle"))
            {
                Vector3 newRandomDirection = Vector3.Reflect(randomDirection, hit.normal);
                randomDirection = Vector3.RotateTowards(randomDirection, newRandomDirection, maxAngleChange * Mathf.Deg2Rad, 0f);
            }
        }
    }

    // Genera una nueva dirección aleatoria para moverse
    void GetNewRandomDirection()
    {
        float angle = Random.Range(-maxAngleChange, maxAngleChange);
        randomDirection = Quaternion.Euler(0, angle, 0) * transform.forward;
    }
}
