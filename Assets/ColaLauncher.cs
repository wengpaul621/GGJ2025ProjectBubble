using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class ColaLauncher : MonoBehaviour
{
    [SerializeField] private Transform launchPoint;
    [SerializeField] private Transform launchDirHelper;
    [SerializeField] private ParticleSystem launchEffect;
    [SerializeField] private List<launchObjectInfo> launchObjInfos;

    [Title("Param")]
    public float launchInterval = 0.1f;
    public float launchDiffusAngle = 10.0f;
    public float launchPressure = 10.0f;
    [MinMaxSlider(0.0f, 1.0f)] public Vector2 launchPressurePercRange;

    [Title("Debug")]
    [ReadOnly] [SerializeField] private float launchIntervalTimer;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Launch();
        }

        if (Input.GetKey(KeyCode.A))
        {
            if (launchIntervalTimer < launchInterval)
            {
                launchIntervalTimer += Time.deltaTime;
            }
            else
            {
                launchIntervalTimer = 0.0f;
                Launch();
            }
        }

        if (Input.GetKeyUp(KeyCode.A))
        {
            launchIntervalTimer = 0.0f;
        }
    }

    [Button]
    public void Launch()
    {
        launchEffect.Play();

        for (int i=0;i< launchObjInfos.Count; i++)
        {
            int launchCount = Random.Range(launchObjInfos[i].objCount.x, launchObjInfos[i].objCount.y);
            for (int k = 0; k < launchCount; k++)
            {
                GameObject newBubble = Instantiate(launchObjInfos[i].objPrefab, launchPoint.transform.position, launchPoint.transform.rotation);
                newBubble.transform.rotation = Quaternion.Euler(0.0f, 0.0f, Random.value * 360.0f);
                Vector2 launchForce = (launchDirHelper.position - launchPoint.position).normalized;
                launchForce = Quaternion.Euler(0.0f, 0.0f, Random.Range(-launchDiffusAngle, launchDiffusAngle)) * launchForce;
                launchForce *= launchPressure;
                launchForce *= Random.Range(launchPressurePercRange.x, launchPressurePercRange.y);
                newBubble.GetComponent<Rigidbody2D>().AddForce(launchForce);
            }
        }
    }

    [System.Serializable]
    private class launchObjectInfo
    {
        [HorizontalGroup("Group")] [HideLabel] public GameObject objPrefab;
        [HorizontalGroup("Group")] [HideLabel] public Vector2Int objCount;
    }
}
