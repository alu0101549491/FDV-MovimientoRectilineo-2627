using UnityEngine;

public class Ejercicio2 : MonoBehaviour
{
    public GameObject cube;
    public float speed = 5f;
    private Vector3 goal;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        goal = cube.transform.position - this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(goal.normalized * speed * Time.deltaTime);
    }
}
