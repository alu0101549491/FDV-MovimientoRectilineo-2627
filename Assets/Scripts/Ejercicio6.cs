using UnityEngine;

public class Ejercicio6 : MonoBehaviour
{
    public Transform goal;
    public float speed = 5f;

    // Update is called once per frame
    void Update()
    {
        this.transform.LookAt(goal.position);
        this.transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}
