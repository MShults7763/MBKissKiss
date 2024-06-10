using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MeleeEnemyFollow : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 1f;
    public float gridSize = 1.0f; // Size of the grid
    public float sightRange = 10f;
    public LayerMask solidObjectsLayer;

    private Vector2 nextPosition;
    private bool isMoving;
    private bool CanMove;

    void Start()
    {
        nextPosition = transform.position;
        isMoving = false;
        CanMove = true;
    }

    void Update()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        if (!isMoving && Vector2.Distance(transform.position, player.position) > gridSize)
        {
            if (Vector2.Distance(transform.position, player.position) < sightRange)
            {
                MoveInGridPattern();
            }
        }
    }

    void MoveInGridPattern()
    {
        // Calculate the direction in grid terms
        Vector2 direction = ((Vector2)player.position - (Vector2)transform.position).normalized;

        // Determine the primary axis of movement based on the greater difference
        float maxDiff = Mathf.Max(Mathf.Abs(direction.x), Mathf.Abs(direction.y));

        Vector2 gridDirection;
        if (Mathf.Abs(direction.x) == maxDiff)
        {
            gridDirection = new Vector2(Mathf.Sign(direction.x), 0);
        }
        else
        {
            gridDirection = new Vector2(0, Mathf.Sign(direction.y));
        }

        // Calculate the next position on the grid
        nextPosition += gridDirection * gridSize;
        nextPosition = new Vector2(
            Mathf.Round(nextPosition.x / gridSize) * gridSize,
            Mathf.Round(nextPosition.y / gridSize) * gridSize
        );

        if (IsWalkable(nextPosition) && CanMove)
            // Start moving towards the next grid position
            StartCoroutine(MoveToPosition(transform, nextPosition, moveSpeed));
    }

    System.Collections.IEnumerator MoveToPosition(Transform transform, Vector2 position, float speed)
    {
        isMoving = true;
        Vector2 startPosition = transform.position;
        float timeToMove = gridSize / speed;
        float t = 0;

        while (t < 1f)
        {
            t += Time.deltaTime / timeToMove;
            transform.position = Vector2.Lerp(startPosition, position, t);
            yield return null;
        }

        isMoving = false;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            CanMove = false;
        }
        else
        {
            CanMove = true;
        }
    }
    private bool IsWalkable(Vector2 nextPosition)
    {
        if (Physics2D.OverlapCircle(nextPosition, 0.3f, solidObjectsLayer) != null)
        {
            return false;
        }
        return true;
    }
}

