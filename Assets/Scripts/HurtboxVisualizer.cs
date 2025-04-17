using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class HurtboxVisualizer : MonoBehaviour
{
    public Color gizmoColor = Color.red; // Красный контур
    [Range(1, 5)] public float lineThickness = 2f;

    private Collider2D col;

    private void Awake()
    {
        col = GetComponent<Collider2D>();
    }

    private void OnDrawGizmos()
    {
        if (col == null)
        {
            col = GetComponent<Collider2D>();
            if (col == null) return;
        }

        Gizmos.color = gizmoColor;

        // Сохраняем оригинальную матрицу
        Matrix4x4 originalMatrix = Gizmos.matrix;

        // Устанавливаем матрицу трансформации объекта
        Gizmos.matrix = Matrix4x4.TRS(
            transform.position,
            transform.rotation,
            transform.lossyScale
        );

        // Отрисовка в зависимости от типа коллайдера
        switch (col)
        {
            case BoxCollider2D box:
                DrawWireBox(box.offset, box.size);
                break;

            case CircleCollider2D circle:
                DrawWireCircle(circle.offset, circle.radius);
                break;
        }

        // Восстанавливаем оригинальную матрицу
        Gizmos.matrix = originalMatrix;
    }

    private void DrawWireBox(Vector2 offset, Vector2 size)
    {
        Vector2 halfSize = size * 0.5f;
        Vector3 p1 = offset + new Vector2(-halfSize.x, -halfSize.y);
        Vector3 p2 = offset + new Vector2(halfSize.x, -halfSize.y);
        Vector3 p3 = offset + new Vector2(halfSize.x, halfSize.y);
        Vector3 p4 = offset + new Vector2(-halfSize.x, halfSize.y);

        // Утолщенные линии через многократное рисование
        for (int i = 0; i < lineThickness; i++)
        {
            float offsetAmount = i * 0.01f;
            Gizmos.DrawLine(p1 + Vector3.one * offsetAmount, p2 + Vector3.one * offsetAmount);
            Gizmos.DrawLine(p2 + Vector3.one * offsetAmount, p3 + Vector3.one * offsetAmount);
            Gizmos.DrawLine(p3 + Vector3.one * offsetAmount, p4 + Vector3.one * offsetAmount);
            Gizmos.DrawLine(p4 + Vector3.one * offsetAmount, p1 + Vector3.one * offsetAmount);
        }
    }

    private void DrawWireCircle(Vector2 offset, float radius)
    {
        int segments = 20;
        Vector3 lastPoint = offset + Vector2.right * radius;

        for (int i = 1; i <= segments; i++)
        {
            float angle = i * Mathf.PI * 2 / segments;
            Vector3 nextPoint = offset + new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * radius;

            // Утолщенные линии
            for (int j = 0; j < lineThickness; j++)
            {
                float offsetAmount = j * 0.01f;
                Gizmos.DrawLine(
                    lastPoint + Vector3.one * offsetAmount,
                    nextPoint + Vector3.one * offsetAmount
                );
            }

            lastPoint = nextPoint;
        }
    }
}