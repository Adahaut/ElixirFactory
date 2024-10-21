using UnityEngine;

public class BeltItem : MonoBehaviour
{
    public float moveSpeed = 2f;
    private Vector3 targetPosition;
    public bool isMoving = false;

    public Belt belt;

    // Called by conveyor belt to set the destination
    public void SetDestination(Vector3 newPosition)
    {
        targetPosition = newPosition;
        isMoving = true;
    }

    void Update()
    {

        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
            {
                isMoving = false;
                transform.position = targetPosition;
            }
        }
    }

}
