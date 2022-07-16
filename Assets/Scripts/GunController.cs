using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public GameObject projectile;
    public Transform barrel;
    public int rpm = 500;
    public float shotWaitTime = 0f;
    public bool canShoot = true;
    public AudioSource audioSource;
    public ParticleSystem ps_shoot;

    // Start is called before the first frame update
    void Start()
    {
        shotWaitTime = 1f / (rpm / 60f);
    }

    // Update is called once per frame
    void Update()
    {
        if (InputManager.inst.leftClickDown)
        {
            if (canShoot)
            {   
                Instantiate(projectile, barrel.position, barrel.rotation);
                audioSource.PlayOneShot(audioSource.clip);
                canShoot = false;
                ps_shoot.Play();
                StartCoroutine(shotCooldown());
            }
        }
    }

    IEnumerator shotCooldown()
    {
        yield return new WaitForSeconds(shotWaitTime);
        canShoot = true;
    }
}
