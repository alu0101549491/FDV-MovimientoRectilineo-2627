using UnityEngine;

public class Ejercicio9 : MonoBehaviour
{
    public float speed = 5f;
    public float rotationSpeed = 3f;
    private Vector3 direction;
    private GameObject[] waypoints;
    private GameObject nextWaypoint;

    void Start()
    {
        waypoints = GameObject.FindGameObjectsWithTag("Waypoint");
        System.Array.Reverse(waypoints);
        nextWaypoint = waypoints.Length > 0 ? waypoints[0] : null;
    }

    void Update()
    {
        MoveToNextWaypoint(nextWaypoint.transform);

        if (Vector3.Distance(this.transform.position, nextWaypoint.transform.position) < 0.1f)
        {
            int currentIndex = System.Array.IndexOf(waypoints, nextWaypoint);
            int nextIndex = (currentIndex + 1) % waypoints.Length;
            nextWaypoint = waypoints[nextIndex];
        }
    }

    void MoveToNextWaypoint(Transform goal)
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
