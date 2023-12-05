using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseGun : MonoBehaviour
{
    [Header("Gun Settings")]
    public float FireRate = 0.5f;
    public float BulletSpeed = 10f;
    public float Damage = 10f;
    public bool CanShoot = true;

    protected float fireTimer = 0f;
    
    /// <summary>
    /// The direction the gun is aiming. 
    /// This should be set in the base class to determine the direction the gun firing
    /// </summary>
    public Vector3 AimDirection {get; set;}

    [Header("Gun References")]
    public Bullet BulletPrefab;
    public Transform BulletSpawnPoint;
    public BaseCharacter Owner;

    void Awake()
    {
        if(Owner == null)
        {
            Owner = GetComponentInParent<BaseCharacter>();
        }
    }
    
    void Update()
    {
        // Subtract the fire timer by dt. Clamp it to 0 so it doesn't go negative
        fireTimer = Mathf.Clamp(fireTimer - Time.deltaTime, 0f, FireRate);
    }

    public virtual void Shoot()
    {
        Debug.Log("Base Gun Shoot");
        if(CanShoot && fireTimer <= 0f)
        {
            fireTimer = FireRate;

            // Create a bullet
            Bullet bullet = Instantiate(BulletPrefab, BulletSpawnPoint.position, Quaternion.identity);

            // Be sure bullet is on same team
            bullet.SetTeam(Owner.Team);

            bullet.SetDirection(AimDirection);
            bullet.SetSpeed(BulletSpeed);
            bullet.SetDamage(Damage);
        }
    }
}