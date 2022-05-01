using UnityEngine;

[RequireComponent(typeof(EnemyAi))]
public class EnemyTank : Tank
{
    [SerializeField] private Win _winCondition;

    private void Start()
    {
        _winCondition = FindObjectOfType<Win>();
    }

    protected override void OnHealthUpdate(float hp)
    {
        if(hp <= 0)
        {
            _winCondition.RemoveEnemy(gameObject.GetComponent<EnemyTank>());
        }
    }
}
