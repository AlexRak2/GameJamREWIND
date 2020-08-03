using Microsoft.Unity.VisualStudio.Editor;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Spells : MonoBehaviour
{
    [SerializeField] KeyCode fireBallKey;
    [SerializeField] KeyCode rewindKey;

    [SerializeField] UnityEngine.UI.Image spellImage;
    [SerializeField] UnityEngine.UI.Image spellImageMask;
    [SerializeField] TextMeshProUGUI spellText;

    [SerializeField] float fireBallChargeTime = 2f;
    [SerializeField] GameObject FireBallSpawn;
    [SerializeField] ParticleSystem FireBall;
    [SerializeField] ParticleSystem FireBallHit;

    [SerializeField] float rewindChargeTime = 2f;

    [SerializeField] List<PlantMovement> plants = new List<PlantMovement>();

    bool fireCasting = false;
    bool rewindCasting = false;

    float chargeTime = 0f;
    float maxChargeTime = 0f;
    float chargeValue = 0f;

    void Start()
    {
        plants.AddRange(FindObjectsOfType<PlantMovement>());
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
            fireCasting = true;
            spellText.text = "Fire";
            maxChargeTime = fireBallChargeTime;
            chargeTime += Time.deltaTime;
            chargeValue = chargeTime / maxChargeTime;
            spellImageMask.fillAmount = chargeValue;
        }

        if (Input.GetKeyUp(fireBallKey) && fireCasting)
        {
            // Shoot fireball away with force based on charge value (0f to 1f)
            var FB = Instantiate(FireBall, FireBallSpawn.transform.position,FireBallSpawn.transform.rotation * Quaternion.Euler(0f, -90, 0f));
            FB.startSize = chargeValue * 4;
            FB.GetComponentInChildren<ParticleSystem>().startSize = chargeValue * 4;
            //FB.startSpeed = chargeValue * 3;
            print("Fireball fired with value of: " + chargeValue + "!");
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
                foreach(PlantMovement plant in plants)
                {
                    plant.SetRewind(true);
                }
            }
            rewindCasting = true;
            spellText.text = "Rew.";
            maxChargeTime = rewindChargeTime;
            chargeTime += Time.deltaTime;
            chargeValue = chargeTime / maxChargeTime;
            spellImageMask.fillAmount = chargeValue;
        }



        if (Input.GetKeyUp(rewindKey) && rewindCasting)
        {
            foreach (PlantMovement plant in plants)
            {
                plant.SetRewind(false);
            }
            print("Rewinded for a value of: " + chargeValue + "!");
            spellText.text = "";
            rewindCasting = false;
            chargeTime = 0f;
            chargeValue = 0f;
            maxChargeTime = 0f;
            spellImageMask.fillAmount = 0f;
        }
    }

}
