using UnityEngine;

public class HealthContainer : MonoBehaviour
{
   [SerializeField] private float _health;
   [SerializeField] private float _maxHealth;

    private void Start()
    {
        _health = _maxHealth;
    }
}
