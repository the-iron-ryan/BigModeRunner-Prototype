using System;
using Palmmedia.ReportGenerator.Core.Reporting.Builders;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private CharacterTeam team;
    [SerializeField] private float damage;
    [SerializeField] private Vector3 aimDirection;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float bulletLifeTime = 5f;

    void Update()
    {
        transform.position += aimDirection * bulletSpeed * Time.deltaTime;
        
        // Destroy the bullet after the lifetime is up
        bulletLifeTime -= Time.deltaTime;
        if (bulletLifeTime <= 0)
        {
            Destroy(gameObject);
        }
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        // Check if the object we hit is an enemy
        if (hit.gameObject.GetComponent<IDamageable>() != null && hit.gameObject.GetComponent<BaseCharacter>().Team != team)
        {
            // If it is, damage it
            hit.gameObject.GetComponent<IDamageable>().TakeDamage(damage);
        }

        // Destroy the bullet
        Destroy(gameObject);
    }

    public void SetTeam(CharacterTeam team)
    {
        this.team = team;
    }

    public void SetDamage(float damage)
    {
        this.damage = damage;
    }

    public void SetDirection(Vector3 aimDirection)
    {
        this.aimDirection = aimDirection;
    }

    public void SetSpeed(float bulletSpeed)
    {
        this.bulletSpeed = bulletSpeed;
    }
}