using UnityEngine;

public class HealthContainer : MonoBehaviour
{
   [SerializeField] private float _health;
   [SerializeField] private float _maxHealth;

    private void Start()
    {
        _health = _maxHealth;
    }

    public void TakeDamage(float damage)
    {
        _health-= damage;
        Destroyed();
    }

    private void Destroyed()
    {
        if (_health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
