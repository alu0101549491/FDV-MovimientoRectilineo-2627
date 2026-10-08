using UnityEngine;

public class Ejercicio5_1 : MonoBehaviour
{
    public Transform goal;
    public float speed = 5f;
    private Vector3 direction;

    // Update is called once per frame
    void Update()
    {
        this.transform.LookAt(goal.position);
        direction = goal.position - this.transform.position;
        this.transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);
    }
}
