using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    // ���������
    public float mouseSensitivity = 100f;

    // ��ҵ�Transform���
    public Transform playerBody;

    // ��ֱ�ӽǵ���ת�Ƕ�
    private float xRotation = 0f;

    void Start()
    {
        // ���������
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // ��ȡ�����ˮƽ�ʹ�ֱ�����ϵ��ƶ���
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // ���㴹ֱ�ӽǵ���ת�Ƕȣ�����������
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // Ӧ�ô�ֱ�ӽǵ���ת
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Ӧ��ˮƽ�ӽǵ���ת
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
