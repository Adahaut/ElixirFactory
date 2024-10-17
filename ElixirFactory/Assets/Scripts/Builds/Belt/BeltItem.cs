using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BeltItem : MonoBehaviour
{
    public float moveSpeed = 2f; // Vitesse de déplacement sur le convoyeur
    private Vector3 targetPosition; // Position cible sur le convoyeur
    private bool isMoving = false;

    public Belt belt;

    // Appelé par le convoyeur pour déplacer l'item
    public void MoveToPosition(Vector3 newPosition)
    {
        targetPosition = newPosition;
        isMoving = true;
    }

    void Update()
    {
        // Si l'item est en mouvement, interpole vers la position cible
        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            // Arrête le mouvement une fois la position cible atteinte
            if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
            {
                isMoving = false;
                transform.position = targetPosition;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Belt belt = other.GetComponent<Belt>();
        if (belt != null && belt.carriedItem == null)
        {
            // Le convoyeur récupère cet item
            belt.carriedItem = this;

            // Le convoyeur demande à l'item de se déplacer à sa position
            MoveToPosition(belt.transform.position);
            Debug.Log("Item " + gameObject.name + " placé sur le convoyeur " + belt.name);
        }
    }

}
