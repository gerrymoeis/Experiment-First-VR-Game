using UnityEngine;

[ExecuteAlways]
[RequireComponent(typeof(LineRenderer))]
public class NavigationLine : MonoBehaviour
{
    private LineRenderer line;

    private void OnEnable()
    {
        line = GetComponent<LineRenderer>();
        DrawLine();
    }

    private void Update()
    {
#if UNITY_EDITOR
        if (!Application.isPlaying)
        {
            DrawLine();
        }
#endif
    }

    public void DrawLine()
    {
        if (line == null)
            line = GetComponent<LineRenderer>();

        int pointCount = transform.childCount;

        line.positionCount = pointCount;

        for (int i = 0; i < pointCount; i++)
        {
            line.SetPosition(i, transform.GetChild(i).position);
        }
    }
}
