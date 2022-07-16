using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedDestruction : MonoBehaviour
{
    public float time = 2f;
    float timeElasped = 0f;

    // Update is called once per frame
    void Update()
    {
        timeElasped += Time.deltaTime;
        if(timeElasped >= time) Destroy(transform.gameObject);
    }
}
