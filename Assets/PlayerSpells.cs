using UnityEngine;

public class PlayerSpells : MonoBehaviour
{
    [SerializeField] private KeyCode projectileKey, buffKey, aoeKey, ultimateKey;
    [SerializeField] private GameObject projectilePrefab, buffPrefab, aoePrefab, ultimatePrefab;
    [SerializeField] private float projectileSpeed = 10f;
    [SerializeField] private bool ultimateIsProjectile, ultimateIsBuff, ultimateIsAOE, ultimateIsTargeted;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(projectileKey))
        {
            CastProjectile();
        }
        if (Input.GetKeyDown(buffKey))
        {
            Instantiate(buffPrefab, transform.position, transform.rotation);
        }
        if (Input.GetKeyDown(aoeKey))
        {
            Instantiate(aoePrefab, transform.position, transform.rotation);
        }
        if (Input.GetKeyDown(ultimateKey))
        {
            Instantiate(ultimatePrefab, transform.position + transform.forward, transform.rotation);
        }
    }

    public void CastProjectile()
    {
        Instantiate(projectilePrefab, transform.position + transform.forward, transform.rotation);
    }

    public void CastBuff()
    {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hitInfo, 5f))
        {
            if (hitInfo.collider.TryGetComponent<Ally>(out var ally))
            {
                Instantiate(buffPrefab, ally.transform);
            }
        }
    }

    public void CastAOE()
    {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hitInfo, 10f))
        {
            if (hitInfo.collider.CompareTag("Ground"))
            {
                Instantiate(aoePrefab, hitInfo.point, Quaternion.identity);
            }
        }
    }

    public void CastUltimate()
    {
        if (ultimateIsProjectile)
        {
            GameObject projectile = Instantiate(ultimatePrefab, transform.position + transform.forward, transform.rotation);
            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.linearVelocity = transform.forward * projectileSpeed;
            }
        }
        else if (ultimateIsBuff)
        {
            if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hitInfo, 5f))
            {
                if (hitInfo.collider.TryGetComponent<Ally>(out var ally))
                {
                    Instantiate(buffPrefab, ally.transform);
                }
            }
        }
        else if (ultimateIsAOE)
        {
            if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hitInfo, 10f))
            {
                if (hitInfo.collider.CompareTag("Ground"))
                {
                    Instantiate(ultimatePrefab, hitInfo.point, Quaternion.identity);
                }
            }
        }
        else if (ultimateIsTargeted)
        {
            if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hitInfo, 5f))
            {
                if (hitInfo.collider.TryGetComponent<Enemy>(out var enemy))
                {
                    Instantiate(ultimatePrefab, enemy.transform);
                }
            }
        }
    }
}
