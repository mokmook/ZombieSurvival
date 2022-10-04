using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    [SerializeField] Gun gun;
    [SerializeField] Transform gunPivot;
    [SerializeField] Transform leftHandMount;
    [SerializeField] Transform rightHandMount;

    private PlayerInput playerInput;
    private Animator anim;


    private void Start()
    {
        playerInput= GetComponent<PlayerInput>();
        anim= GetComponent<Animator>();
    }
    private void OnEnable()
    {
        gun.gameObject.SetActive(true);
    }
    private void OnDisable()
    {
        gun.gameObject.SetActive(false);
    }
    private void Update()
    {
        if (playerInput.fire)
            gun.Fire();
        else if(playerInput.reload)
        {
            if (gun.Reload())
            {
                anim.SetTrigger("Reload");
            }
        }
        UpdateUI();
    }
    private void UpdateUI()
    {
        if (gun != null && UImanager.instance != null)
        {
            UImanager.instance.UpdateAmmoText(gun.m_magAmmo, gun.m_magAmmo);
        }
    }
    private void OnAnimatorIK(int layerIndex)
    {
        gunPivot.position = anim.GetIKHintPosition(AvatarIKHint.RightElbow);

        anim.SetIKPositionWeight(AvatarIKGoal.LeftHand,1.0f);
        anim.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1.0f);

        anim.SetIKPosition(AvatarIKGoal.LeftHand, leftHandMount.position);
        anim.SetIKRotation(AvatarIKGoal.LeftHand, leftHandMount.rotation);

        anim.SetIKPositionWeight(AvatarIKGoal.RightHand, 1.0f);
        anim.SetIKRotationWeight(AvatarIKGoal.RightHand, 1.0f);

        anim.SetIKPosition(AvatarIKGoal.RightHand, rightHandMount.position);
        anim.SetIKRotation(AvatarIKGoal.RightHand, rightHandMount.rotation);
    }
}
