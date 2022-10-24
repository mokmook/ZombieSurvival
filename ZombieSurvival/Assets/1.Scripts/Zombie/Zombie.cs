using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : LivingEntity
{
    [SerializeField] LayerMask whatIsTarget;

    private LivingEntity targetEntity;
    private NavMeshAgent navMeshAgent;

    [SerializeField] ParticleSystem hitEffect;
    [SerializeField] AudioClip deathSound;
    [SerializeField] AudioClip hitSound;

    private Animator zombieanim;
    private AudioSource zombieAuidoPlayer;
    private Renderer zombieRenderer;

    [SerializeField] float damage;
    [SerializeField] float timeBetAttack;
    private float lastAttackTime;

    private bool hasTarget 
    { get
        {
            if(targetEntity != null && !targetEntity.dead)
            {
                return true;
            }
            return false;
        } 
    }
    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        zombieanim = GetComponent<Animator>();
        zombieAuidoPlayer = GetComponent<AudioSource>();
        zombieRenderer = GetComponentInChildren<Renderer>();

    }
    public void Setup(ZombieData zombieData)
    {
        startingHealth = zombieData.health;
        health = startingHealth;

        damage = zombieData.damage;

        navMeshAgent.speed = zombieData.speed;

        zombieRenderer.material.color = zombieData.skinColor;
    }
    private void Start()
    {
        StartCoroutine("UpdatePath");
    }
    private void Update()
    {
        zombieanim.SetBool("HasTarget", hasTarget);
        
    }
    private IEnumerator UpdatePath()
    {
        while (!dead)
        {
            if (hasTarget)
            {
                navMeshAgent.isStopped = false;
                navMeshAgent.SetDestination(targetEntity.transform.position);
            }
            else
            {
                navMeshAgent.isStopped = true;
                Collider[] colliders = Physics.OverlapSphere(transform.position, 20f, whatIsTarget);

                for (int i = 0; i < colliders.Length; i++)
                {
                    LivingEntity livingEntity = colliders[i].GetComponent<LivingEntity>();
                    if (livingEntity != null && !livingEntity.dead)
                    {
                        targetEntity = livingEntity;

                        break;
                    }
                }
            }
            yield return new WaitForSeconds(.25f);
        }
    }
    public override void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        if (!dead)
        {
            hitEffect.transform.position = hitPoint;
            hitEffect.transform.rotation = Quaternion.LookRotation(hitNormal);
            hitEffect.Play();

            zombieAuidoPlayer.PlayOneShot(hitSound);
        }
        base.OnDamage(damage, hitPoint, hitNormal);
     
    }
    protected override void Die()
    {
        base.Die();

        Collider[] zombieColliders = GetComponents<Collider>();
        for (int i = 0; i < zombieColliders.Length; i++)
        {
            zombieColliders[i].enabled = false;
        }
        navMeshAgent.isStopped = true;
        navMeshAgent.enabled = false;

        zombieanim.SetTrigger("Die");

        zombieAuidoPlayer.PlayOneShot(deathSound);
    }
    protected void OnTriggerStay(Collider other)
    {
        if (!dead&&Time.time>=lastAttackTime+timeBetAttack)
        {

            LivingEntity attackTarget = other.GetComponent<LivingEntity>();
            if (attackTarget != null && attackTarget == targetEntity)
            {
                lastAttackTime = Time.time;

                Vector3 hitPoint = other.ClosestPoint(transform.position);
                Vector3 hitNormal=transform.position-other.transform.position;

                attackTarget.OnDamage(damage, hitPoint, hitNormal);
            }
        }
    }
}
