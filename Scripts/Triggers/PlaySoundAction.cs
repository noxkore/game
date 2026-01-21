using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlaySoundAction : ActionBase
{
    [Header("Sound Settings")]
    [SerializeField] private AudioClip clip;
    [SerializeField] private bool playOneShot = true;
    [SerializeField] private float volume = 1f;

    [Header("Runtime")]
    [SerializeField, Range(0f, 2f)]
    private float localIntensity = 1f;

    private AudioSource audioSource;

    protected override void Awake()
    {
        base.Awake();
        audioSource = GetComponent<AudioSource>();
    }

    public void SetLocalIntensity(float value)
    {
        localIntensity = Mathf.Max(0f, value);
    }

    public override void Execute(IContext context)
    {
        if (clip == null)
            return;

        float finalVolume = volume * localIntensity;

        if (finalVolume <= 0f)
            return;

        if (playOneShot)
        {
            audioSource.PlayOneShot(clip, finalVolume);
        }
        else
        {
            audioSource.clip = clip;
            audioSource.volume = finalVolume;
            audioSource.Play();
        }
    }
}