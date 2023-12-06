using UnityEngine;

public class EnemyGun : BaseGun
{
    protected PlayerCharacter targetPlayer;
    protected float ShootTimer = 0f;

    public float ShootIntervalMin = 0.5f;
    public float ShootIntervalMax = 1.5f;

    override protected void Awake()
    {
        base.Awake();

        targetPlayer = FindObjectOfType<PlayerCharacter>();

    }
    
    void Start()
    {
        ResetShootTimer();
    }
    

    protected override void Update()
    {
        base.Update();
        
        if(ShootTimer > 0f)
        {
            ShootTimer -= Time.deltaTime;
        }
        else
        {
            ResetShootTimer();
            Shoot();
        }
    }

    protected void LateUpdate()
    {
        AimDirection = (targetPlayer.transform.position - transform.position).normalized;
    }

    protected void ResetShootTimer()
    {
        ShootTimer = Random.Range(ShootIntervalMin, ShootIntervalMax);
    }
}