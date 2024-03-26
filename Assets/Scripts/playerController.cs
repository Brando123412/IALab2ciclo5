using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public float speed = 5f; // Velocidad de movimiento del personaje

    void Update()
    {
        // Obtener la entrada del teclado en los ejes horizontal y vertical
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Calcular el vector de movimiento basado en las entradas del teclado
        Vector3 movement = new Vector3(moveHorizontal, 0f, moveVertical) * speed * Time.deltaTime;

        // Aplicar el movimiento al transform del personaje
        transform.Translate(movement);
    }
}
