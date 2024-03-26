using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TarjetAgent : MonoBehaviour
{
    public Transform cursor3D;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("Terrain"))
                {
                    
                    cursor3D.position = hit.point;
                }
            }
        }
    }
}

