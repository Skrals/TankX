using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePoint : MonoBehaviour
{
    [SerializeField] private Projectile _projectileTemplate;
    [SerializeField] private GameObject _lookPoint;

    public void Shot()
    {
        var projectile = Instantiate(_projectileTemplate, transform.position, transform.rotation);
        var projectileRigitbody = projectile.GetComponent<Rigidbody2D>();

        Vector2 startPos = _projectileTemplate.transform.position;
        Vector2 endPos = _lookPoint.transform.position;

        projectileRigitbody.AddForce(Vector2.MoveTowards(startPos, endPos, 50) * _projectileTemplate.GetProjectileSpeed() *Time.deltaTime, ForceMode2D.Impulse);

    }

}
