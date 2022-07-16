using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceController : MonoBehaviour
{
    public Transform VFX;
    public float pulseDuration = 1f;
    public Vector3 finalSize;
    public bool pulsing = true;
    public AnimationCurve sizeCurve;
    // Start is called before the first frame update
    void Start()
    {
        InputManager.inst.mi.Misc.DevicePulse.performed += ctx => tryPulse();
        VFX.localScale = Vector3.zero;
    }

    void tryPulse()
    {
        if (pulsing) return;
        StartCoroutine(performPulse());
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

            VFX.localScale = finalSize * sizeCurve.Evaluate(progress);
            yield return null;
        }

         pulsing = false;
        VFX.localScale = Vector3.zero;

    }

}
