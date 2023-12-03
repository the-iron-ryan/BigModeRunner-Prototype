using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseGun : MonoBehaviour
{
    [Header("Gun Settings")]
    public float FireRate = 0.5f;
    public float BulletSpeed = 10f;
    public float Damage = 10f;

    /// <summary>
    /// The direction the gun is aiming. 
    /// This should be set in the base class to determine the direction the gun firing
    /// </summary>
    protected Vector3 aimDirection;

    [Header("Gun References")]
    public GameObject BulletPrefab;
    public BaseCharacter Owner;

    // Start is called before the first frame update
    void Start()
    {
        
    }


    protected virtual void Shoot()
    {
        // Create a bullet
        GameObject bullet = Instantiate(BulletPrefab, transform.position, Quaternion.identity);

        // Be sure bullet is on same team
        bullet.GetComponent<Bullet>().SetTeam(Owner.Team);

        // Set the bullet's direction
        bullet.GetComponent<Bullet>().SetDirection(aimDirection);

        // Set the bullet's speed
        bullet.GetComponent<Bullet>().SetSpeed(BulletSpeed);

        // Set the bullet's damage
        bullet.GetComponent<Bullet>().SetDamage(Damage);
    }
}