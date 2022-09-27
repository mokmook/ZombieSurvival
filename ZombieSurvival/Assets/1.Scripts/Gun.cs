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
      
    }
    private void Shot()
    {

    }

    private IEnumerator ShotEffect(Vector3 hitPos)
    {
        bulletLineRenderer.enabled = true;
        yield return new WaitForSeconds(0.03f);
        bulletLineRenderer.enabled = false;
    }
    public bool Reload()
    {
        return false;
    }
    private IEnumerator ReloadRoutine()
    {
        state = State.Reloading;
        yield return new WaitForSeconds(gunData.reloadTime);
        state = State.Ready;
    }
    
}
