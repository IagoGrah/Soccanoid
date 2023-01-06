using System;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] GameObject breakVFX;
    [SerializeField] private AudioClip breakSFX;
    [SerializeField][Range(0f, 1f)]private float breakSFXVolume = 1f;
    [SerializeField] Sprite[] damageSprites;

    SpriteRenderer spriteRenderer;
    Level level;

    int currentHP;

    private const string breakableTag = "Breakable";

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        level = FindObjectOfType<Level>();
        if (CompareTag(breakableTag))
        {
            currentHP = damageSprites.Length;
            level.AddBreakableBlock();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (CompareTag(breakableTag))
        {
            TakeDamage();
        }
    }

    private void TakeDamage()
    {
        currentHP--;
        if (currentHP <= 0)
        {
            level.RemoveBreakableBlock();
            TriggerBreakFX();
            Destroy(gameObject);
        }
        else
        {
            UpdateDamageSprite();
        }
    }

    private void UpdateDamageSprite()
    {
        var spriteIndex = currentHP - 1;
        if (damageSprites[spriteIndex] != null)
        {
            spriteRenderer.sprite = damageSprites[spriteIndex]; 
        }
    }

    private void TriggerBreakFX()
    {
        AudioSource.PlayClipAtPoint(breakSFX, Camera.main.transform.position, breakSFXVolume);
        var particles = Instantiate(breakVFX, transform.position, transform.rotation);
        var particleSystem = particles.GetComponent<ParticleSystem>().main;
        particleSystem.startColor = GetComponent<SpriteRenderer>().color;
    }
}
