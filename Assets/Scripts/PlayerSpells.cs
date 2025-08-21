using UnityEngine;

public class PlayerSpells : MonoBehaviour
{
    [SerializeField] private KeyCode projectileKey, buffKey, aoeKey, ultimateKey;
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private float projectileLifetime = 1f;

    [SerializeField] private GameObject buffPrefab, aoePrefab, ultimatePrefab;
    [SerializeField] private float projectileSpeed = 10f;
    [SerializeField] private bool ultimateIsProjectile, ultimateIsBuff, ultimateIsAOE, ultimateIsTargeted;

    void Update()
    {
        if (Input.GetKeyDown(projectileKey))
        {
            CastProjectile();
        }
        if (Input.GetKeyDown(buffKey))
        {
            CastBuff();
        }
        if (Input.GetKeyDown(aoeKey))
        {
            CastAOE();
        }
        if (Input.GetKeyDown(ultimateKey))
        {
            CastUltimate();
        }
    }

    public void CastProjectile()
    {
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hitInfo))
        {
            Vector3 direction = (hitInfo.point - transform.position).normalized;
            direction.y = 0;
            transform.forward = direction;
        }

        var projectile = Instantiate(projectilePrefab, transform.position + transform.forward, transform.rotation);
        projectile.Initialize(transform.forward, projectileSpeed);
        Destroy(projectile.gameObject, projectileLifetime);
    }

    public void CastBuff()
    {
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hitInfo))
        {
            if (hitInfo.collider.TryGetComponent<Ally>(out var ally))
            {
                Instantiate(buffPrefab, ally.transform);
            }
        }
    }

    public void CastAOE()
    {
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hitInfo))
        {
            Vector3 hitPoint = hitInfo.point;
            hitPoint.y = 0;
            Instantiate(aoePrefab, hitPoint, Quaternion.identity);
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
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hitInfo))
            {
                Vector3 hitPoint = hitInfo.point;
                hitPoint.y = 0;
                Instantiate(ultimatePrefab, hitPoint, Quaternion.identity);
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
