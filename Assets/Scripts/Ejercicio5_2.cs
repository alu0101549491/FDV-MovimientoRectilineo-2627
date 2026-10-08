using UnityEngine;

public class Ejercicio5_2 : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float boostMultiplier = 2f;

    private void Update()
    {
        float horizontalMovement = Input.GetAxis("Horizontal");
        float verticalMovement = Input.GetAxis("Vertical");
        float currentSpeed = Input.GetKey(KeyCode.Space) ? speed * boostMultiplier : speed;

        Vector3 movement = new Vector3(
            horizontalMovement,
            0f,
            verticalMovement
        );

        transform.Translate(movement * currentSpeed * Time.deltaTime);
    }
}
