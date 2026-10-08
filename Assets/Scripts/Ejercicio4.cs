using UnityEngine;

public class Ejercicio4 : MonoBehaviour
{
    public Transform goal;
    public float speed = 5f;
    private Vector3 direction;
    private LineRenderer lineRenderer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.transform.LookAt(goal.position);

        lineRenderer = GetComponent<LineRenderer>();
        if (lineRenderer == null)
        {
            lineRenderer = gameObject.AddComponent<LineRenderer>();
        }

        lineRenderer.positionCount = 2;
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = Color.red;
        lineRenderer.endColor = Color.red;
    }

    // Update is called once per frame
    void Update()
    {
        direction = goal.position - this.transform.position;
        Vector3 lineEnd = this.transform.position + this.transform.forward * 10f;
        lineRenderer.SetPosition(0, this.transform.position);
        lineRenderer.SetPosition(1, lineEnd);
        this.transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);
    }
}
