using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invader : MonoBehaviour
{
    private GameManager gameManager;

    public Sprite[] animationSprites; //array of sprites
    public float animationTime = 1.0f; //how often does it cicle to the next sprite

    public System.Action killed;

    private SpriteRenderer _spriteRenderer;
    private int _spriteIndex;


    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating(nameof(AnimateSprite), this.animationTime, this.animationTime);
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    private void AnimateSprite()
    {
        _spriteIndex++;
        if (_spriteIndex >= this.animationSprites.Length)
        {
            _spriteIndex = 0;
        }
        _spriteRenderer.sprite = animationSprites[_spriteIndex];
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Lazer"))
        {
            gameManager.UpdateScore(100);
            this.killed.Invoke();
            this.gameObject.SetActive(false);
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Bunker"))
        {
            gameManager.KillPlayer();
        }
    }
}
