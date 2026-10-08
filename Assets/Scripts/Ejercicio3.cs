using UnityEngine;

public class Ejercicio3 : MonoBehaviour
{
    public Transform goal;
    public float speed = 5f;
    private Vector3 direction;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.transform.LookAt(goal.position);
    }

    // Update is called once per frame
    void Update()
    {
        direction = goal.position - this.transform.position;
        this.transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);
    }
}
