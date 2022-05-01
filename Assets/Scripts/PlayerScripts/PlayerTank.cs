using UnityEngine;

public class PlayerTank : Tank
{
    protected override void OnHealthUpdate(float hp)
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
