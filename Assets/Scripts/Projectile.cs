using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Rigidbody rb;
    public float speed = 10f;
    public int damage = 20;
    public GameObject explosion;
    // Start is called before the first frame update
    private void OnEnable()
    {
        rb.velocity = transform.forward * speed;
        StartCoroutine(kill());
    }


    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.gameObject.layer == 7)
        {
            other.transform.GetComponent<Enemy>().takeDamage(damage);
        }

        var expl = Instantiate(explosion, other.GetContact(0).point, Quaternion.Euler(0f, 0f, 0f));
        expl.transform.up = transform.position - other.transform.position;

        Destroy(transform.gameObject);
    }

    IEnumerator kill()
    {
        yield return new WaitForSeconds(5);
        Destroy(transform.gameObject);
    }
}
