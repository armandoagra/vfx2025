using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private GameObject explosionFX;
    private float speed;

    void Update()
    {
        transform.Translate(transform.forward * Time.deltaTime * speed, Space.World);
    }

    public void Initialize(Vector3 direction, float speed)
    {
        transform.forward = direction.normalized;
        this.speed = speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Enemy enemy))
        {
            Instantiate(explosionFX, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
