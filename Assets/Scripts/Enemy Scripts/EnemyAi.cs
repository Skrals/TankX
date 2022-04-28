using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAi : MonoBehaviour
{
    [SerializeField] private PlayerTank _playerTank;
    // Start is called before the first frame update

    void Start()
    {
        _playerTank = FindObjectOfType<PlayerTank>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void Patrol()
    {
        
    }

    private void Search()
    {

    }

    private void Attack()
    {

    }
}
