using UnityEngine;

public class Playermovmentcc : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float speed = 7f;
    public float jmp = 4f;

    public float gravity = -9.81f;

    private CharacterController cc;
    private Vector3 velocity;
    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 move = transform.right * Input.GetAxis("Horizontal") + transform.forward * Input.GetAxis("Vertical");
        cc.Move(move * speed * Time.deltaTime);

        if (cc.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        if (Input.GetButtonDown("Jump") && cc.isGrounded)
        {
            velocity.y = Mathf.Sqrt(jmp * -2f * gravity);

        }
        velocity.y += gravity * Time.deltaTime;
        cc.Move(velocity * Time.deltaTime);
    }
}

