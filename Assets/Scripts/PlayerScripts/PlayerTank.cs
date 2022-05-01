using UnityEngine;

public class PlayerTank : MonoBehaviour
{
    [SerializeField] private HealthContainer _healthContainer;

    private void Awake() => _healthContainer = GetComponent<HealthContainer>();

    private void OnEnable() => _healthContainer.OnHealthChanged += OnHealthUpdate;

    private void OnDisable() => _healthContainer.OnHealthChanged -= OnHealthUpdate;

    private void OnHealthUpdate(float hp)
    {
        if(hp <= 0)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        Debug.Log("Game OVER");
    }

}
