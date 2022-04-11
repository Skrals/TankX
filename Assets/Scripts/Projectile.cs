using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float _damage;
    [SerializeField] private float _speed;

    private void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out HealthContainer healthContainer))
        {
            healthContainer.TakeDamage(_damage);
            Destroy(gameObject);
        }
    }

    public float GetProjectileSpeed()
    {
        return _speed;
    }
}
