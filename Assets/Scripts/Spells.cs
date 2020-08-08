using Microsoft.Unity.VisualStudio.Editor;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class Spells : MonoBehaviour
{
    [SerializeField] PlayerStats playerStats;
    [SerializeField] KeyCode fireBallKey;
    [SerializeField] KeyCode rewindKey;

    [SerializeField] UnityEngine.UI.Image spellImage;
    [SerializeField] UnityEngine.UI.Image spellImageMask;
    [SerializeField] TextMeshProUGUI spellText;

    [SerializeField] float fireBallChargeTime = 2f;
    [SerializeField] GameObject FireBallSpawn;
    [SerializeField] ParticleSystem FireBall;

    [SerializeField] float rewindChargeTime = 2f;

    [SerializeField] List<PlantScaler> plants = new List<PlantScaler>();
    [SerializeField] List<Key> keys = new List<Key>();

    [SerializeField] AudioSource spellAudioSource;
    [SerializeField] AudioClip fireChannelSFX;
    [SerializeField] AudioClip fireCastSFX;
    [SerializeField] AudioClip fireLargeCastSFX;
    [SerializeField] AudioClip rewindChannelSFX;


    bool fireCasting = false;
    public bool rewindCasting = false;

    float chargeTime = 0f;
    float maxChargeTime = 0f;
    float chargeValue = 0f;
    [SerializeField] Volume postProcessVolume;
    ColorAdjustments colorAdjust;

    void Start()
    {
        plants.AddRange(FindObjectsOfType<PlantScaler>());
        keys.AddRange(FindObjectsOfType<Key>());
        postProcessVolume.profile.TryGet(out colorAdjust);

    }

    void Update()
    {
        FireBallCastLogic();

        RewindCastLogig();
    }

    private void FireBallCastLogic()
    {
        if (Input.GetKey(fireBallKey) && !rewindCasting)
        {
            //Perhaps make it so the mana bar shows how much itll consume
            if (!fireCasting)
            {
                spellAudioSource.Stop();
                spellAudioSource.loop = true;
                spellAudioSource.clip = fireChannelSFX;
                spellAudioSource.Play();
            }
            fireCasting = true;
            spellText.text = "Fire";
            maxChargeTime = fireBallChargeTime;
            chargeTime += Time.deltaTime;
            chargeValue = chargeTime / maxChargeTime;
            spellImageMask.fillAmount = chargeValue;


        }

        if (Input.GetKeyUp(fireBallKey) && fireCasting && playerStats.currentMana >0)
        {
            if (chargeValue < 0.15f) 
            {
                chargeValue = 0.15f;
            }
                if (chargeValue > 1f)
                {
                    chargeValue = 1f;
                }

            //to fix, get it linked up with how much the charge value is. and balance;
            float manaUsed = chargeValue * 3;
            playerStats.UseMana(manaUsed);
            // Shoot fireball away with force based on charge value (0f to 1f)
            ParticleSystem fireBallParticle = Instantiate(FireBall, FireBallSpawn.transform.position, FireBallSpawn.transform.rotation * Quaternion.Euler(0f, -90, 0f));
                fireBallParticle.startSize = chargeValue * 3;
                ParticleSystem[] fireBallChilds = fireBallParticle.GetComponentsInChildren<ParticleSystem>();
                //FB.startSpeed = chargeValue * 3;
                foreach (ParticleSystem pfbc in fireBallChilds)
                {
                    pfbc.startSize = chargeValue * 2;
                }

            //print("Fireball fired with value of: " + chargeValue + "!");

            spellAudioSource.loop = false;
            spellAudioSource.Stop();
            spellAudioSource.clip = fireCastSFX;
            spellAudioSource.Play();

            spellText.text = "";
            fireCasting = false;
            chargeTime = 0f;
            chargeValue = 0f;
            maxChargeTime = 0f;
            spellImageMask.fillAmount = 0f;
        }
    }

    private void RewindCastLogig()
    {
        if (Input.GetKey(rewindKey) && !fireCasting)
        {
            // Start rewind effect, slowly rewind the environment
            if (!rewindCasting)
            {
                foreach(PlantScaler plant in plants)
                {
                    plant.SetRewind(true);

                }
                foreach (Key key in keys)
                {
                    key.Flip();
                }
                spellAudioSource.Stop();
                spellAudioSource.loop = true;
                spellAudioSource.clip = rewindChannelSFX;
                spellAudioSource.Play();
                GetComponent<Player>().Freeze(true);
            }
            colorAdjust.saturation.Override(Mathf.Lerp(0f, -100f, chargeTime));
            rewindCasting = true;
            spellText.text = "Rew.";
            maxChargeTime = rewindChargeTime;
            chargeTime += Time.deltaTime;
            chargeValue = chargeTime / maxChargeTime;
            spellImageMask.fillAmount = chargeValue;
        }

        if (Input.GetKeyUp(rewindKey) && rewindCasting)
        {
            foreach (PlantScaler plant in plants)
            {
                plant.SetRewind(false);
            }
            foreach (Key key in keys)
            {
                key.Flip();
            }

            GetComponent<Player>().Freeze(false);

            spellAudioSource.loop = false;
            spellAudioSource.Stop();

            colorAdjust.saturation.Override(0f);
            //print("Rewinded for a value of: " + chargeValue + "!");
            spellText.text = "";
            rewindCasting = false;
            chargeTime = 0f;
            chargeValue = 0f;
            maxChargeTime = 0f;
            spellImageMask.fillAmount = 0f;
        }
    }

}
