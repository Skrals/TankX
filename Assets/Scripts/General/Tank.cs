using UnityEngine;

public abstract class Tank : MonoBehaviour
{
    [SerializeField] private HealthContainer _healthContainer;

    private void Awake() => _healthContainer = GetComponent<HealthContainer>();

    private void OnEnable() => _healthContainer.OnHealthChanged += OnHealthUpdate;

    private void OnDisable() => _healthContainer.OnHealthChanged -= OnHealthUpdate;

    protected abstract void OnHealthUpdate(float hp);

}
