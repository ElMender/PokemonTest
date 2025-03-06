using UnityEngine;
using System.Collections;

public class PokeballProjectile : MonoBehaviour
{
    Rigidbody rb;
    public float force;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.rotation * Vector3.forward * force, ForceMode.Impulse);
        StartCoroutine(DestroyBall());
    }

    IEnumerator DestroyBall()
    {
        yield return new WaitForSeconds(5);
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name != "Pokemon")
        {
            Destroy(this.gameObject);
        }
    }
}
