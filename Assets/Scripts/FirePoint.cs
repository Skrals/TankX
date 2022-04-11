using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePoint : MonoBehaviour
{
    [SerializeField] private Projectile _projectileTemplate;

    public void Shot()
    {
        var projectile = Instantiate(_projectileTemplate, transform.position, transform.rotation);
        var projectileRigitbody = projectile.GetComponent<Rigidbody2D>();

        projectileRigitbody.AddForce(Vector2.up * _projectileTemplate.GetProjectileSpeed(),ForceMode2D.Impulse);//ToDo выстрел в любую сторону
    }

}
