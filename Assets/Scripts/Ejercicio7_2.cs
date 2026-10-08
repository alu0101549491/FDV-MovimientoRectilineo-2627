using UnityEngine;

public class Ejercicio7_2 : MonoBehaviour
{
    public Transform goal;
    public float speed = 5f;

    // Update is called once per frame
    void Update()
    {
        this.transform.LookAt(goal.position);
        this.transform.position = Vector3.MoveTowards(this.transform.position, goal.position, speed * Time.deltaTime);
    }
}
