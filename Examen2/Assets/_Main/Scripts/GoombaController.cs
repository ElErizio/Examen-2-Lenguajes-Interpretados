using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoombaController : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D = default;
    private AudioSource _audioSource = default;
    [SerializeField] private float _movimientoGoomba = 6f;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        MoveGoomba();
    }

    private void MoveGoomba()
    {
        _rigidbody2D.velocity = new Vector2( _movimientoGoomba, _rigidbody2D.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Obstacles"))
        {
            _movimientoGoomba *= -1;
        }

        if (collision.collider.CompareTag("Player") && collision.contacts[0].normal.y <= -0.5f || collision.collider.CompareTag("Shell"))
        {
            GoombaDie();
        }
        else if(collision.collider.CompareTag("Player"))
        {
            _movimientoGoomba = 0f;
        }
    }
    // Funcion para que el goomba muera y se destruya el objeto
    private void GoombaDie()
    {
        _audioSource.Play();
        StartCoroutine(WaitToDie());
    }

    private IEnumerator WaitToDie()
    {
        yield return new WaitForSeconds(0.2f);
        Destroy(gameObject);
    }
}

