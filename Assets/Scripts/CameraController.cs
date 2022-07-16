using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform cameraAnchor;
    public float rotationAroundY = 0f;
    public float mouseSensivity = 1f;
    public Transform player;

    void Start()
    {
        InputManager.inst.mi.Mouse.delta.performed += ctx => handleDelta(ctx.ReadValue<Vector2>());
    }

    void handleDelta(Vector2 delta)
    {
        if (!InputManager.inst.rightClickDown) return;
        rotationAroundY += delta.x * mouseSensivity * Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        cameraAnchor.rotation = Quaternion.Euler(0, rotationAroundY, 0);
    }
    void LateUpdate()
    {
        transform.position = player.position;

    }
}
