using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BeltItem : MonoBehaviour
{
    public float moveSpeed = 2f; // Vitesse de d�placement sur le convoyeur
    private Vector3 targetPosition; // Position cible sur le convoyeur
    private bool isMoving = false;

    public Belt belt;

    // Appel� par le convoyeur pour d�placer l'item
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

            // Arr�te le mouvement une fois la position cible atteinte
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
            // Le convoyeur r�cup�re cet item
            belt.carriedItem = this;

            // Le convoyeur demande � l'item de se d�placer � sa position
            MoveToPosition(belt.transform.position);
            Debug.Log("Item " + gameObject.name + " plac� sur le convoyeur " + belt.name);
        }
    }

}
