using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float _damage;
    [SerializeField] private float _speed;

    private void Start()
    {
        StartCoroutine(DestroySelf());
    }

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
        else if (collision.gameObject.TryGetComponent(out Border border))
        {
            Destroy(gameObject);
        }
    }

    public float GetProjectileSpeed()
    {
        return _speed;
    }

    private IEnumerator DestroySelf()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }


}
