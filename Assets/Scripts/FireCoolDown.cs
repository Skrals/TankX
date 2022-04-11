using System.Collections;
using UnityEngine;

public class FireCoolDown : MonoBehaviour
{
    [SerializeField] private bool _CD;

    private void Start()
    {
        _CD = false;
    }

    public bool GetCD()
    {
        return _CD;
    }

    public IEnumerator FireCD()
    {
        Debug.Log("Fire on CD");
        _CD = true;

        yield return new WaitForSeconds(1f);
        _CD = false;

        Debug.Log("Fire CD is down");

        StopCoroutine(FireCD());
    }
}
