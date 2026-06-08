using UnityEngine;
using System.Collections;

public class WaypointPatrol : MonoBehaviour
{
    public Transform pathParent;

    public float moveSpeed = 2f;
    public float idleTime = 2f;

    private Transform[] points;

    private int currentIndex = 0;
    private int direction = 1;

    private bool isWaiting = false;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();

        points = new Transform[pathParent.childCount];

        for (int i = 0; i < pathParent.childCount; i++)
        {
            points[i] = pathParent.GetChild(i);
        }

        transform.position = points[0].position;

        StartCoroutine(IdleRoutine());
    }

    private void Update()
    {
        if (isWaiting)
            return;

        MoveToPoint();
    }

    private void MoveToPoint()
    {
        Transform target = points[currentIndex];

        transform.position = Vector3.MoveTowards(
            transform.position,
            target.position,
            moveSpeed * Time.deltaTime
        );

        Vector3 dir = target.position - transform.position;
        dir.y = 0;

        if (dir != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(dir);
        }

        if (Vector3.Distance(transform.position, target.position) < 0.05f)
        {
            StartCoroutine(ReachedPoint());
        }
    }

    private IEnumerator ReachedPoint()
    {
        isWaiting = true;

        animator.SetBool("IsWalking", false);

        yield return new WaitForSeconds(idleTime);

        if (currentIndex == points.Length - 1)
        {
            direction = -1;
        }
        else if (currentIndex == 0)
        {
            direction = 1;
        }

        currentIndex += direction;

        animator.SetBool("IsWalking", true);

        isWaiting = false;
    }

    private IEnumerator IdleRoutine()
    {
        isWaiting = true;

        animator.SetBool("IsWalking", false);

        yield return new WaitForSeconds(idleTime);

        currentIndex = 1;

        animator.SetBool("IsWalking", true);

        isWaiting = false;
    }

    private void OnDrawGizmos()
    {
        if (pathParent == null)
            return;

        Gizmos.color = Color.green;

        for (int i = 0; i < pathParent.childCount - 1; i++)
        {
            Transform a = pathParent.GetChild(i);
            Transform b = pathParent.GetChild(i + 1);

            Gizmos.DrawLine(a.position, b.position);
        }
    }
}
