using UnityEngine;

public class Player : MonoBehaviour
{
    public int pid;

    private Rigidbody rb;
    private Vector3 initialPosition;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        initialPosition = transform.position;
    }

    public void Lose()
    {
        rb.AddForce(new Vector3(2, 2));
    }

    public void ResetPlayer()
    {
        transform.position = initialPosition;
        transform.eulerAngles = new Vector3(0, 0, 0);
    }
}