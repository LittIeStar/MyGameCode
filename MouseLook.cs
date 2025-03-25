using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    // 鼠标灵敏度
    public float mouseSensitivity = 100f;

    // 玩家的Transform组件
    public Transform playerBody;

    // 垂直视角的旋转角度
    private float xRotation = 0f;

    void Start()
    {
        // 锁定鼠标光标
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // 获取鼠标在水平和垂直方向上的移动量
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // 计算垂直视角的旋转角度，并进行限制
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // 应用垂直视角的旋转
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // 应用水平视角的旋转
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
