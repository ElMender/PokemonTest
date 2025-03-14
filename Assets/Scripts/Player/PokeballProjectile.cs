using UnityEngine;
using System.Collections;

public class PokeballProjectile : MonoBehaviour
{
    Rigidbody rb;
    public float force;
    public GameObject caughtFX;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.rotation * Vector3.forward * force, ForceMode.Impulse);
        StartCoroutine(DestroyBall());
    }

    IEnumerator DestroyBall()
    {
        yield return new WaitForSeconds(3);
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Pokemon")
        {
            GameManager.Instance.AddPokemonToList(collision.gameObject.name);
            SoundManager.SoundInstance.PlayCaughtSFX();
            Instantiate(caughtFX, collision.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
            Destroy(collision.gameObject);
        }
    }
}
