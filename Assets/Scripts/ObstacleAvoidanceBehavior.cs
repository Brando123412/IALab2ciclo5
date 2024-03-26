using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleAvoidanceBehavior : MonoBehaviour
{
    public Transform target; 
    public float movementSpeed = 5f;
    public float obstacleDetectionDistance = 2f;

    private void Update()
    {
        if (target != null)
        {
            MoveTowardsTarget();
        }
    }

    private void MoveTowardsTarget()
    {
        Vector3 directionToTarget = (target.position - transform.position).normalized;
        
        transform.position += directionToTarget * movementSpeed * Time.deltaTime;
        
        transform.rotation = Quaternion.LookRotation(directionToTarget);
        Debug.DrawRay(transform.position, directionToTarget * obstacleDetectionDistance, Color.red);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, directionToTarget, out hit, obstacleDetectionDistance))
        {
            if (hit.collider.CompareTag("Obstacle"))
            {
                print("Holaaa");
                Vector3 avoidanceDirection = Vector3.Cross(hit.normal, Vector3.up).normalized;
                transform.forward = avoidanceDirection;
               
            }
        }
    }
}
