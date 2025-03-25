using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // ����ƶ��ٶ�
    public float speed = 5f;
    // �����Ծ����
    public float jumpForce = 5f;
    // ���ڼ���������߳���
    public float groundCheckDistance = 0.1f;
    // ����㣬�������߼���ж��Ƿ��ڵ���
    public LayerMask groundLayer;

    private Rigidbody rb;
    private bool isGrounded;

    void Start()
    {
        // ��ȡ��Ҷ���� Rigidbody ���
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody component not found on the player object.");
            enabled = false;
            return;
        }
        // ����������ת����ֹ������ƶ�������������ת
        rb.freezeRotation = false;
    }

    void FixedUpdate()
    {
        float deltaTime = Time.fixedDeltaTime;
        // �������Ƿ��ڵ�����
        CheckGround();
        // ������ҵ��ƶ�
        MovePlayer(deltaTime);
        // ������ҵ���Ծ
        HandleJump();
    }

    void CheckGround()
    {
        // ����Ҷ���ĵײ����·���һ������
        Vector3 rayOrigin = transform.position - new Vector3(0, GetComponent<Collider>().bounds.extents.y, 0);
        RaycastHit hit;
        isGrounded = Physics.Raycast(rayOrigin, Vector3.down, out hit, groundCheckDistance, groundLayer);
    }

    void MovePlayer(float deltaTime)
    {
        // ��ȡˮƽ�ʹ�ֱ���������
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        // �����ƶ���������������������������ϵ��
        Vector3 movement = transform.forward * verticalInput + transform.right * horizontalInput;

        // �����µ��ƶ��ٶ�����
        Vector3 targetVelocity = movement * speed;
        targetVelocity.y = rb.velocity.y;

        // ʹ�� Rigidbody �� MovePosition �����������λ��
        rb.MovePosition(rb.position + targetVelocity * deltaTime);
    }

    void HandleJump()
    {
        // ������ڵ������Ұ�����Ծ����Ĭ���ǿո����ʱִ����Ծ����
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            // ������ʩ��һ�����ϵĳ���ʵ����Ծ
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            Debug.Log("Jump");
        }
    }
}