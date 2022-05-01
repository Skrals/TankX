using System.Collections;
using UnityEngine;

public class FireCoolDown : MonoBehaviour
{
    [SerializeField] private bool _cd;
    [SerializeField] private float _time;

    private void Start()
    {
        _cd = false;
    }

    public bool GetCD()
    {
        return _cd;
    }

    public IEnumerator FireCD()
    {
        Debug.Log("Fire on CD");
        _cd = true;

        yield return new WaitForSeconds(_time);
        _cd = false;

        Debug.Log("Fire CD is down");

        StopCoroutine(FireCD());
    }
}
