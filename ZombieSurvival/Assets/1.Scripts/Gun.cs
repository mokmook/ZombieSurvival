using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public enum State
    {
        Ready,
        Empty,
        Reloading
    }
    public State state { get; private set; }

    [SerializeField] Transform fireTransform;

    [SerializeField] ParticleSystem muzzleFlashEffect;
    [SerializeField] ParticleSystem shellEjectEffect;

    private LineRenderer bulletLineRenderer;

    private AudioSource gunAuidoPlayer;

    [SerializeField] GunData gunData;

    private float fireDistance = 50f;

    [SerializeField] int ammoRemain = 100;
    [SerializeField] int magAmmo;

    private float lastFireTime;
    void Awake()
    {
        bulletLineRenderer = GetComponent<LineRenderer>();
        gunAuidoPlayer = GetComponent<AudioSource>();

        bulletLineRenderer.positionCount = 2;
        bulletLineRenderer.enabled = false;
    }
    private void OnEnable()
    {
        ammoRemain = gunData.startAmmoRemain;
        magAmmo = gunData.magCapacity;

        state = State.Ready;
        lastFireTime = 0;
    }  
    public void Fire()
    {
        if (state==State.Ready&&Time.time>=lastFireTime+gunData.timeBetFire)
        {
            lastFireTime = Time.time;
            Shot();
        }
    }
    private void Shot()
    {
        Debug.DrawRay(fireTransform.position, fireTransform.forward, color: Color.red);

        RaycastHit hit;

        Vector3 hitPos;
        if (Physics.Raycast(fireTransform.position, fireTransform.forward, out hit,fireDistance))
        {
            IDamageable target = hit.collider.GetComponent<IDamageable>();
            
            if (target != null)
                target.OnDamage(gunData.damage, hit.point, hit.normal);

            hitPos = hit.point;
        }
        
        else       
            hitPos = fireTransform.position + fireTransform.forward * fireDistance;

        StartCoroutine("ShotEffect", hitPos);
        

        magAmmo--;

        if (magAmmo <= 0)
            state = State.Empty;
    }

    private IEnumerator ShotEffect(Vector3 hitPos)
    {
        muzzleFlashEffect.Play();
        shellEjectEffect.Play();

        gunAuidoPlayer.PlayOneShot(gunData.shotClip);

        bulletLineRenderer.SetPosition(0, fireTransform.position);
        bulletLineRenderer.SetPosition(1, hitPos);
        bulletLineRenderer.enabled = true;

        yield return new WaitForSeconds(0.03f);

        bulletLineRenderer.enabled = false; 
    }
    
    public bool Reload()
    {      
        if(state==State.Reloading||ammoRemain<=0||magAmmo>=gunData.magCapacity)
        return false;

        StartCoroutine("ReloadRoutine");
        return true;
    }
    private IEnumerator ReloadRoutine()
    {
        state = State.Reloading;
        gunAuidoPlayer.PlayOneShot(gunData.reloadClip);

        yield return new WaitForSeconds(gunData.reloadTime);

        int ammoToFill = gunData.magCapacity - magAmmo;

        if (ammoRemain < ammoToFill)
            ammoToFill = ammoRemain;

        magAmmo += ammoToFill;
        ammoRemain -= ammoToFill;

        state = State.Ready;
    }
    
}
