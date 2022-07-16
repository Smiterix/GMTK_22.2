using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceController : MonoBehaviour
{
    public float pulseDuration = 1f;
    public Vector3 finalSize;
    public bool pulsing = true;
    public AnimationCurve sizeCurve;
    [Range(0, 1)]
    public float propability = 0f;
    // Start is called before the first frame update
    void Start()
    {
        InputManager.inst.mi.Misc.DevicePulse.performed += ctx => tryPulse();
        transform.localScale = Vector3.zero;
    }

    void tryPulse()
    {
        if (pulsing) return;
        StartCoroutine(performPulse());
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.gameObject.layer != 7) return;
        if (Random.Range(0f, 1f) <= propability)
        {
            var enemy = other.GetComponent<Enemy>();
            enemy.dematerialize();
        };
    }

    IEnumerator performPulse()
    {
        pulsing = true;

        float timeElasped = 0f;
        float progress = 0f;

        while (timeElasped < pulseDuration)
        {
            timeElasped += Time.deltaTime;
            progress = timeElasped / pulseDuration;

            transform.localScale = finalSize * sizeCurve.Evaluate(progress);
            yield return null;
        }

        pulsing = false;
        transform.localScale = Vector3.zero;
    }

}
