using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMoving : MonoBehaviour
{
    public bool playerOnPlatform; // Nouvelle variable pour suivre si le joueur est sur la plateforme
    public bool moving; // Nouvelle variable pour suivre si la plateforme est en mouvement

    [SerializeField] float speed;
    [SerializeField] public Transform[] points;

    public int currentPointIndex;
    public bool movingForward = true;

    void Start()
    {
        transform.position = points[0].position;
        currentPointIndex = 0;
    }

    void Update()
    {
        if (playerOnPlatform && !moving) // Si le joueur est sur la plateforme et la plateforme n'est pas en mouvement
        {
            moving = true; // Activer le mouvement
        }

        if (moving)
        {
            Vector3 targetPosition = points[currentPointIndex].position;

            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

            if (transform.position == targetPosition) // Si la plateforme atteint sa destination
            {
                if (movingForward)
                {
                    currentPointIndex++;
                    if (currentPointIndex >= points.Length)
                    {
                        currentPointIndex = points.Length - 2;
                        movingForward = false;
                    }
                }
                else
                {
                    currentPointIndex--;
                    if (currentPointIndex < 0)
                    {
                        currentPointIndex = 1;
                        movingForward = true;
                    }
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerOnPlatform = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerOnPlatform = false;
            moving = false; // Arrêter le mouvement quand le joueur quitte la plateforme
        }
    }
}
