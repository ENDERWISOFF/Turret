using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 1;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Движение на W A S D

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 move = new Vector3(horizontal, vertical);
        if (move.magnitude > 1)
            move.Normalize();

        transform.position += move * speed * Time.deltaTime;
    }
}
