using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // 玩家移动速度
    public float speed = 5f;
    // 玩家跳跃力量
    public float jumpForce = 5f;
    // 用于检测地面的射线长度
    public float groundCheckDistance = 0.1f;
    // 地面层，用于射线检测判断是否在地面
    public LayerMask groundLayer;

    private Rigidbody rb;
    private bool isGrounded;

    void Start()
    {
        // 获取玩家对象的 Rigidbody 组件
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody component not found on the player object.");
            enabled = false;
            return;
        }
        // 冻结刚体的旋转，防止玩家在移动过程中意外旋转
        rb.freezeRotation = false;
    }

    void FixedUpdate()
    {
        float deltaTime = Time.fixedDeltaTime;
        // 检查玩家是否在地面上
        CheckGround();
        // 处理玩家的移动
        MovePlayer(deltaTime);
        // 处理玩家的跳跃
        HandleJump();
    }

    void CheckGround()
    {
        // 从玩家对象的底部向下发射一条射线
        Vector3 rayOrigin = transform.position - new Vector3(0, GetComponent<Collider>().bounds.extents.y, 0);
        RaycastHit hit;
        isGrounded = Physics.Raycast(rayOrigin, Vector3.down, out hit, groundCheckDistance, groundLayer);
    }

    void MovePlayer(float deltaTime)
    {
        // 获取水平和垂直方向的输入
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        // 计算移动方向向量（相对于玩家自身坐标系）
        Vector3 movement = transform.forward * verticalInput + transform.right * horizontalInput;

        // 计算新的移动速度向量
        Vector3 targetVelocity = movement * speed;
        targetVelocity.y = rb.velocity.y;

        // 使用 Rigidbody 的 MovePosition 方法更新玩家位置
        rb.MovePosition(rb.position + targetVelocity * deltaTime);
    }

    void HandleJump()
    {
        // 当玩家在地面上且按下跳跃键（默认是空格键）时执行跳跃操作
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            // 给刚体施加一个向上的冲量实现跳跃
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            Debug.Log("Jump");
        }
    }
}
