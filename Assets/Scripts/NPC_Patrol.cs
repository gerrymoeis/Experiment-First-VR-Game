using UnityEngine;
using System.Collections;

public class NPCPatrol : MonoBehaviour
{
    [Header("Points")]
    public Transform pointA;
    public Transform pointB;

    [Header("Movement")]
    public float moveSpeed = 2f;

    [Header("Idle")]
    public float idleTime = 3f;

    private Transform targetPoint;
    private Animator animator;

    private bool isWaiting = false;

    private void Start()
    {
        animator = GetComponent<Animator>();

        transform.position = pointA.position;

        targetPoint = pointB;

        StartCoroutine(IdleThenMove());
    }

    private void Update()
    {
        if (isWaiting)
            return;

        MoveToTarget();
    }

    private void MoveToTarget()
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPoint.position,
            moveSpeed * Time.deltaTime
        );

        Vector3 direction =
            targetPoint.position - transform.position;

        direction.y = 0;

        if (direction != Vector3.zero)
        {
            transform.rotation =
                Quaternion.LookRotation(direction);
        }

        float distance =
            Vector3.Distance(
                transform.position,
                targetPoint.position
            );

        if (distance < 0.05f)
        {
            StartCoroutine(ArrivedAtPoint());
        }
    }

    private IEnumerator ArrivedAtPoint()
    {
        isWaiting = true;

        animator.SetBool("IsWalking", false);

        yield return new WaitForSeconds(idleTime);

        if (targetPoint == pointA)
            targetPoint = pointB;
        else
            targetPoint = pointA;

        animator.SetBool("IsWalking", true);

        isWaiting = false;
    }

    private IEnumerator IdleThenMove()
    {
        isWaiting = true;

        animator.SetBool("IsWalking", false);

        yield return new WaitForSeconds(idleTime);

        animator.SetBool("IsWalking", true);

        isWaiting = false;
    }
}