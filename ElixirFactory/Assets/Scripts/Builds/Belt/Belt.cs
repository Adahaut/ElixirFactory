using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Belt : MonoBehaviour
{
    float moveSpeed = 2.5f;
    [SerializeField] bool moveRight = true;
    public Vector2 direction = Vector2.right;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Vector3 movement = (moveRight ? Vector3.right : Vector3.left) * moveSpeed * Time.deltaTime;
        collision.transform.Translate(direction.normalized * moveSpeed * Time.deltaTime);
    }
}
