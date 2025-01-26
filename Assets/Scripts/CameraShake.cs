// ʹ��Cinemachine��
using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ����һ��CameraShake�࣬����ʵ���������Ч��
public class CameraShake : MonoBehaviour
{
    // ����ģʽ�������������ű���ֱ�ӷ���
    public static CameraShake Instance { get; private set; }

    // Cinemachine����������
    private CinemachineVirtualCamera cinemachineVirtualCamera;

    // ��������ʱ���ʱ��
    private float shakeTimer;

    // ��Awake�׶Σ���ʼ�������ͻ�ȡCinemachine����������
    private void Awake()
    {
        Instance = this;
        cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    // ��Update�и��¶���Ч�����߼�
    private void Update()
    { }
    

    // �ṩһ�����������������������Ч������Ҫָ������ǿ�Ⱥͳ���ʱ��
    public void shakeCamera(float intensity, float time)
    {
        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;
    }

    // ��һ�����������������������������ʱָ��Ƶ�ʺ�ǿ���Լ�ʱ��
    public void shakeCameraWithFrequency(float intensity, float frequency)
    {
        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;
        cinemachineBasicMultiChannelPerlin.m_FrequencyGain = frequency;
    }
}
