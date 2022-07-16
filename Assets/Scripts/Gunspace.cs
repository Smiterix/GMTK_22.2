using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gunspace : MonoBehaviour
{
    public Transform gunspaceposition;
    public Transform player;
    public float amplitude = 1f;
    public float lerp = 0f;
    public float lerpSpeed = 1f;
    Vector3 initialSpinePosition = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (PlayerController.inst.movementVector != Vector3.zero)
            lerp = Mathf.MoveTowards(lerp, 1, lerpSpeed * Time.deltaTime);
        else
            lerp = Mathf.MoveTowards(lerp, 0, lerpSpeed * Time.deltaTime);

        transform.rotation = player.rotation;
        transform.position = new Vector3(gunspaceposition.position.x, gunspaceposition.position.y, gunspaceposition.position.z);


        transform.position += transform.forward * amplitude * lerp;
    }
}
