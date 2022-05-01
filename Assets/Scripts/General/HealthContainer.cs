using UnityEngine;
using UnityEngine.Events;

public class HealthContainer : MonoBehaviour
{
   [SerializeField] private float _health;
   [SerializeField] private float _maxHealth;

    public event UnityAction<float> OnHealthChanged;

    private void Start() => _health = _maxHealth;

    public void TakeDamage(float damage)
    {
        _health-= damage;
        OnHealthChanged?.Invoke(_health);
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
