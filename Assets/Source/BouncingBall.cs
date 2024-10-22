using UnityEngine;

public class BouncingBall : MonoBehaviour
{
    public float jumpForce = 5f;
    public float forwardForce = 3f;
    public float returnSpeed = 2f;
    public Vector3 startPosition;
    private Rigidbody2D rb;
    private bool isJumping = false;
    private bool isControlEnabled = true; // Контроль включен по умолчанию

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
    }

    void Update()
    {
        if (isControlEnabled && (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)))
        {
            Jump();
        }

        if (isJumping)
        {
            transform.position = Vector3.Lerp(transform.position, startPosition, returnSpeed * Time.deltaTime);
        }
    }

    void Jump()
    {
        if (!isJumping)
        {
            isJumping = true;
            rb.velocity = new Vector3(forwardForce, jumpForce, 0);
            Invoke("StopJump", 0.5f);
        }
    }

    void StopJump()
    {
        isJumping = false;
        rb.velocity = Vector3.zero;
    }

    public void EnableControl(bool enable)
    {
        isControlEnabled = enable; // Управление можно включить или отключить
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            transform.position = new Vector3(startPosition.x, startPosition.y, startPosition.z);
            rb.velocity = Vector3.zero;
        }
    }
}
