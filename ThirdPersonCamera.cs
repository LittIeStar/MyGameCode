using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform target; // 玩家的Transform组件
    public float distance = 5.0f; // 摄像机与玩家的距离
    public float height = 2.0f; // 摄像机的高度
    public float smoothSpeed = 0.125f; // 摄像机跟随的平滑度

    private Vector3 offset; // 摄像机与玩家的偏移量

    void Start()
    {
        // 计算初始偏移量
        offset = new Vector3(0f, height, -distance);
    }

    void LateUpdate()
    {
        // 计算目标位置
        Vector3 desiredPosition = target.position + target.TransformDirection(offset);

        // 使用平滑插值来移动摄像机到目标位置
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        // 让摄像机始终看向玩家
        transform.LookAt(target);
    }
}