using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class HurtboxVisualizer : MonoBehaviour
{
    public Color gizmoColor = Color.red; // Красный контур
    [Range(1, 5)] public float lineThickness = 2f;

    private Collider2D _col;

    private void Awake()
    {
        _col = GetComponent<Collider2D>();
    }

    private void OnDrawGizmos()
    {
        if (_col is null)
        {
            _col = GetComponent<Collider2D>();
            if (_col is null) return;
        }

        Gizmos.color = gizmoColor;

        // Сохраняем оригинальную матрицу
        var originalMatrix = Gizmos.matrix;

        // Устанавливаем матрицу трансформации объекта
        Gizmos.matrix = Matrix4x4.TRS(
            transform.position,
            transform.rotation,
            transform.lossyScale
        );

        // Отрисовка в зависимости от типа коллайдера
        switch (_col)
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
        var halfSize = size * 0.5f;
        Vector3 p1 = offset + new Vector2(-halfSize.x, -halfSize.y);
        Vector3 p2 = offset + new Vector2(halfSize.x, -halfSize.y);
        Vector3 p3 = offset + new Vector2(halfSize.x, halfSize.y);
        Vector3 p4 = offset + new Vector2(-halfSize.x, halfSize.y);

        // Утолщенные линии через многократное рисование
        for (var i = 0; i < lineThickness; i++)
        {
            var offsetAmount = i * 0.01f;
            Gizmos.DrawLine(p1 + Vector3.one * offsetAmount, p2 + Vector3.one * offsetAmount);
            Gizmos.DrawLine(p2 + Vector3.one * offsetAmount, p3 + Vector3.one * offsetAmount);
            Gizmos.DrawLine(p3 + Vector3.one * offsetAmount, p4 + Vector3.one * offsetAmount);
            Gizmos.DrawLine(p4 + Vector3.one * offsetAmount, p1 + Vector3.one * offsetAmount);
        }
    }

    private void DrawWireCircle(Vector2 offset, float radius)
    {
        var segments = 20;
        Vector3 lastPoint = offset + Vector2.right * radius;

        for (var i = 1; i <= segments; i++)
        {
            var angle = i * Mathf.PI * 2 / segments;
            Vector3 nextPoint = offset + new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * radius;

            // Утолщенные линии
            for (var j = 0; j < lineThickness; j++)
            {
                var offsetAmount = j * 0.01f;
                Gizmos.DrawLine(
                    lastPoint + Vector3.one * offsetAmount,
                    nextPoint + Vector3.one * offsetAmount
                );
            }

            lastPoint = nextPoint;
        }
    }
}