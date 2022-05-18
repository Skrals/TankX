using System.Collections;
using UnityEngine;

public class FireCoolDown : MonoBehaviour
{
    [SerializeField] private bool _cdFlag;
    [SerializeField] private float _cdTime;

    private void Start()
    {
        _cdFlag = false;
    }

    public bool GetCD()
    {
        return _cdFlag;
    }

    public IEnumerator FireCD()
    {
        Debug.Log("Fire on CD");
        _cdFlag = true;

        yield return new WaitForSeconds(_cdTime);
        _cdFlag = false;

        Debug.Log("Fire CD is down");

        StopCoroutine(FireCD());
    }
}
