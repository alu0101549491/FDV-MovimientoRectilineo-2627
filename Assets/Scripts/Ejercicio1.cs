using UnityEngine;

public class Ejercicio1 : MonoBehaviour
{
    public GameObject cube;
    public float speed = 5f;
    private Vector3 direction;
    private Vector3 goal;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        direction = cube.transform.position - this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(this.transform.position, cube.transform.position) > 0.1f)
        {
            goal = direction.normalized * speed * Time.deltaTime;
            this.transform.Translate(goal);
        }
    }
}
