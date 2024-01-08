using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using UnityEngine;
using HarmonyLib;
using System.Collections.Generic;
using LethalLib.Modules;
using Unity.Netcode;

namespace CreatureRadar;

public class CreatureRadarItem : GrabbableObject
{
    public AudioSource itemAudioSource;
    public AudioClip radarBeep;
    public AudioClip[] batteryDischargedSounds;
    public RoundManager roundManager;

    public bool isBeeping = false;
    private float beepInterval = 0.5f; // Should increase as the closest enemy gets closer


    public void Awake()
    { 
        itemAudioSource = GetComponent<AudioSource>();
        roundManager = FindObjectOfType<RoundManager>();

        grabbable = true;
        grabbableToEnemies = true;
        insertedBattery = new Battery(false, 1);
        mainObjectRenderer = GetComponent<MeshRenderer>();
    }

    public override void Update()
    {
        base.Update();

        if (this.isHeld)
        {
            if (this.insertedBattery.empty)
            {
                // TODO: Play a sound to indicate that the battery is empty

            }
            else
            {
                // TODO: Add the actual radar functionality
            }
        }
    }

    private void startTracking() // TODO: Add the actual radar functionality
    {
        if (!isBeeping)
        {
            isBeeping = true;
            // For performance reasons, we only want to check for the closest enemy every 0.75 seconds
        }
    }

    private void beepPitchDown(float decrementValue = 0.033f)
    {
        itemAudioSource.pitch -= decrementValue;
    }

    private void beepPitchUp(float incrementValue = 0.033f)
    {
        itemAudioSource.pitch += incrementValue;
    }

    public override void UseUpBatteries()
    {
        base.UseUpBatteries();

    }

}
 