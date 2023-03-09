using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VfxPlayer : MonoBehaviour
{
    [SerializeField] ParticleSystem attack1ParticleSystem;
    [SerializeField] ParticleSystem attack2ParticleSystem;
    [SerializeField] ParticleSystem attack3ParticleSystem;
    [SerializeField] ParticleSystem attack4ParticleSystem;
    [SerializeField] ParticleSystem attack5ParticleSystem;

    public void PlayVfx(int attackAnimation)
    {
        switch (attackAnimation)
        {
            case 1:
                attack1ParticleSystem.Play();
                break;
            case 2:
                attack2ParticleSystem.Play();
                break ;
            case 3:
                attack3ParticleSystem.Play();
                break;
            case 4:
                attack4ParticleSystem.Play();
                break;
            case 5:
                attack5ParticleSystem.Play();
                break;
        }
        //attack1ParticleSystem.Play();
        //Debug.Log("Play vfx");
    }
}
