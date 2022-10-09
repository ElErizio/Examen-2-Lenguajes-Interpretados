using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;

public class MarioController : MonoBehaviour
{
    // Variables que vamos nacesitar para el movimiento de Mario
    private float _movimiento = 8;
    private float _fuerzaSalto = 16;
    private float _horizontalInput = default;
    private bool _isGrounded = default;


    private Rigidbody2D _rigidboy2D = default;
    private AudioSource _audioSource = default;

    [SerializeField] private float _counter = 120;
    [SerializeField] private TextMeshProUGUI _counterText = default;
    [SerializeField] private AudioClip[] _clips = default;
    [SerializeField] private LayerMask _groundLayer = default;
    [SerializeField] private Transform _feetPos = default;

    private void Awake()
    {
        _rigidboy2D = GetComponent<Rigidbody2D>();
        _audioSource = GetComponent<AudioSource>();
    }

    // Codigo para el control de Mario
    private void FixedUpdate()
    {
        _horizontalInput = Input.GetAxisRaw("Horizontal");
        _rigidboy2D.velocity = new Vector2(_horizontalInput * _movimiento, _rigidboy2D.velocity.y);
        _isGrounded = Physics2D.OverlapCircle(_feetPos.position, 0.1f, _groundLayer);
    }
    private void Update()
    {
        Salto();
        Giro();

        if (_counter > 0)
        {
            _counter -= Time.deltaTime;
            _counterText.text = _counter.ToString("f0");
        }
        else
        {
            _counter = 120;
            MuerteMario();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy") && collision.contacts[0].normal.y <= 0.4f || collision.collider.CompareTag("Shell"))
        {
            Debug.Log(collision.contacts[0].normal);
            MuerteMario();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Finish"))
        {
            SceneManager.LoadScene("WinUI");
        }

        if (collision.CompareTag("Fall"))
        {
            MuerteMario();
        }
    }

    private void Salto()
    {
        if (Input.GetButtonDown("Jump") && _isGrounded)
        {
            _audioSource.clip = _clips[0];
            _audioSource.Play();
            _rigidboy2D.velocity = Vector2.up * _fuerzaSalto;
        }
    }

    private void Giro()
    {
        Vector3 localScale = transform.localScale;

        if (_horizontalInput != 0)
        {
            localScale.x = _horizontalInput > 0 ? 5f : -5f;
        }

        transform.localScale = localScale;
    }
    
    public void MuerteMario()
    {
        _movimiento = 0f;
        _fuerzaSalto = 0f;
        _audioSource.clip = _clips[1];
        _audioSource.Play();
        StartCoroutine(WaitForDie());
    }

    private IEnumerator WaitForDie()
    {
        yield return new WaitForSeconds(2.7f);
        SceneManager.LoadScene("GameOver");
    }

}
