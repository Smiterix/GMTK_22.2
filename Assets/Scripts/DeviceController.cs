using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceController : MonoBehaviour
{
    [Header("Settings")]
    public int pulsesLeft = 3;
    public float pulsesCooldown = 1f;
    public bool canPulse = true;
    public float rechargeDuration = 5f;
    public int maxCharges = 5;


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
        StartCoroutine(recharge());
    }

    void tryPulse()
    {
        if (pulsing || pulsesLeft == 0 || !canPulse) return;
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

        StartCoroutine(startCooldown());

        pulsesLeft--;

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

    IEnumerator startCooldown()
    {
        canPulse = false;
        yield return new WaitForSeconds(pulsesCooldown);
        canPulse = true;
    }

    IEnumerator recharge()
    {

        while (true)
        {
            if (pulsesLeft < maxCharges)
            {
                pulsesLeft++;
                yield return new WaitForSeconds(rechargeDuration);
            }
            yield return null;
        }
    }

}
