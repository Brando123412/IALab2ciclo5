using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PursuitBehavior : MonoBehaviour
{
    public Transform target; // Referencia al transform del objetivo
    public float moveSpeed = 5f; // Velocidad de movimiento del perseguidor
    public float rotationSpeed = 5f; // Velocidad de rotaci�n del perseguidor
    public float predictionMultiplier = 1f; // Multiplicador de predicci�n de movimiento
    public float stopDistance = 1.5f; // Distancia a la que el perseguidor se detiene

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (target != null)
        {
            // Calcula la direcci�n hacia el objetivo
            Vector3 direction = target.position - transform.position;
            float distance = direction.magnitude;

            // Predice la posici�n futura del objetivo
            Vector3 futurePosition = target.position + target.GetComponent<Rigidbody>().velocity * predictionMultiplier;

            // Calcula la direcci�n hacia la posici�n futura del objetivo
            Vector3 futureDirection = futurePosition - transform.position;
            futureDirection.Normalize();

            // Calcula la rotaci�n hacia la posici�n futura del objetivo
            Quaternion lookRotation = Quaternion.LookRotation(futureDirection);

            // Rotaci�n suave hacia la posici�n futura del objetivo
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);

            // Si la distancia al objetivo es mayor que la distancia de detenci�n, mueve al perseguidor
            if (distance > stopDistance)
            {
                // Mueve al perseguidor en la direcci�n calculada
                rb.velocity = transform.forward * moveSpeed;
            }
            else
            {
                // Det�n al perseguidor si est� lo suficientemente cerca del objetivo
                rb.velocity = Vector3.zero;
            }
        }
    }
}
