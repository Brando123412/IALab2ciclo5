using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderBehavior : MonoBehaviour

{
    public float moveSpeed = 3f; // Velocidad de movimiento del objeto
    public float rotationSpeed = 100f; // Velocidad de rotaci�n del objeto
    public float changeDirectionTime = 2f; // Tiempo entre cambios de direcci�n
    public float maxAngleChange = 45f; // M�ximo cambio de �ngulo permitido
    public float obstacleAvoidanceDistance = 2f; // Distancia para evitar obst�culos

    private float timer; // Temporizador para cambiar la direcci�n
    private Vector3 randomDirection; // Direcci�n aleatoria para moverse

    void Start()
    {
        timer = changeDirectionTime;
        GetNewRandomDirection();
    }

    void Update()
    {
        // Mueve el objeto en la direcci�n aleatoria
        transform.Translate(randomDirection * moveSpeed * Time.deltaTime);

        // Rotaci�n suave hacia la direcci�n de movimiento
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
        // Detecci�n de obst�culos
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

    // Genera una nueva direcci�n aleatoria para moverse
    void GetNewRandomDirection()
    {
        float angle = Random.Range(-maxAngleChange, maxAngleChange);
        randomDirection = Quaternion.Euler(0, angle, 0) * transform.forward;
    }
}
