using UnityEngine;

public class Ejercicio8 : MonoBehaviour
{
    public Transform goal;
    public float speed = 5f;
    public float rotationSpeed = 2f;
    private Vector3 direction;

    void Update()
    {
        direction = goal.position - this.transform.position;
        this.transform.rotation = Quaternion.Slerp(
            this.transform.rotation, 
            Quaternion.LookRotation(direction), 
            rotationSpeed * Time.deltaTime
        );
        this.transform.position = Vector3.MoveTowards(this.transform.position, goal.position, speed * Time.deltaTime);
    }
}
