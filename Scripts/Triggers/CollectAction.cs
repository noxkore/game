using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(AudioSource))]
public class CollectOnEnter : BaseCollectable
{
    [Header("Destroy")]
    [SerializeField] private float destroyDelay = 3f;

    private bool collected;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (collected)
            return;

        if (!other.TryGetComponent<BaseInventory>(out BaseInventory inventory))
            return;

        if (!other.CompareTag("Player"))
        {
            return;
        }

        Collect(inventory);
    }

    protected override void OnCollected()
    {
        collected = true;

        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        if (sprite != null)
            sprite.enabled = false;

        Collider2D collider = GetComponent<Collider2D>();
        if (collider != null)
            collider.enabled = false;

        AudioSource audio = GetComponent<AudioSource>();
        if (audio != null)
            audio.Play();

        Destroy(gameObject, destroyDelay);
    }
}