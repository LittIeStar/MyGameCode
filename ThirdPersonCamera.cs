using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform target; // ��ҵ�Transform���
    public float distance = 5.0f; // ���������ҵľ���
    public float height = 2.0f; // ������ĸ߶�
    public float smoothSpeed = 0.125f; // ����������ƽ����

    private Vector3 offset; // ���������ҵ�ƫ����

    void Start()
    {
        // �����ʼƫ����
        offset = new Vector3(0f, height, -distance);
    }

    void LateUpdate()
    {
        // ����Ŀ��λ��
        Vector3 desiredPosition = target.position + target.TransformDirection(offset);

        // ʹ��ƽ����ֵ���ƶ��������Ŀ��λ��
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        // �������ʼ�տ������
        transform.LookAt(target);
    }
}