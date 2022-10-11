using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : LivingEntity
{
    [SerializeField] Slider healthSlider;

    [SerializeField] AudioClip deathClip;
    [SerializeField] AudioClip hitClip;
    [SerializeField] AudioClip itemPuckupClip;

    private AudioSource playerAudioPlayer;
    private Animator anim;

    private PlayerMovement playerMovement;
    private PlayerShooter playerShooter;

    private void Awake()
    {
        playerAudioPlayer = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();

        playerMovement = GetComponent<PlayerMovement>();
        playerShooter = GetComponent<PlayerShooter>();
    }
    protected override void onEnable()
    {
        base.onEnable();

        healthSlider.gameObject.SetActive(true);
        healthSlider.maxValue = _startingHealth;
        healthSlider.value = health;

        playerMovement.enabled = true;
        playerShooter.enabled = true;
    }
    protected override void RestoreHealth(float newHealth)
    {
        base.RestoreHealth(newHealth);
        healthSlider.value = health;
    }
    public override void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        if (!dead)
            playerAudioPlayer.PlayOneShot(hitClip);

        base.OnDamage(damage, hitPoint, hitNormal);

        healthSlider.value = health;
    }
    protected override void Die()
    {
        base.Die();
        healthSlider.gameObject.SetActive(false);

        playerAudioPlayer.PlayOneShot(deathClip);
        anim.SetTrigger("Die");

        playerMovement.enabled = false;
        playerShooter.enabled = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!dead)
        {
            IItem item = other.GetComponent<IItem>();
            if(item != null)
            {
                item.Use(gameObject);

                playerAudioPlayer.PlayOneShot(itemPuckupClip);
            }

        }
    }
}
