using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowRoute : MonoBehaviour
{
    public Transform[] waypoints; // Puntos de la ruta
    public float speed = 2f;      // Velocidad del coche
    public float rotationSpeed = 5f; // Velocidad de giro del coche

    private int currentWaypointIndex = 0;

    void Update()
    {
        if (waypoints.Length == 0)
            return;

        // Moverse hacia el waypoint actual
        Transform targetWaypoint = waypoints[currentWaypointIndex];
        Vector3 direction = (targetWaypoint.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;

        // Rotar hacia el waypoint
        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);
        }

        // Comprobar si llegó al waypoint
        if (Vector3.Distance(transform.position, targetWaypoint.position) < 0.1f)
        {
            // Pasar al siguiente waypoint (volver al inicio si es el último)
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        }
    }
}
