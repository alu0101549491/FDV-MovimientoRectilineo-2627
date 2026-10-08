using UnityEngine;

public class Ejercicio7_1 : MonoBehaviour
{
    public Transform goal;
    public float speed = 5f;
    public float accuracy = 0.01f;
    private Vector3 direction;

    // Update is called once per frame
    void Update()
    {
        direction = goal.position - this.transform.position;
        if (direction.magnitude > accuracy)
        {
            this.transform.LookAt(goal.position);
            this.transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
    }
}
