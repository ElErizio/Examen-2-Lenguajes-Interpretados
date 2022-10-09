using UnityEngine;
using TMPro;
using System.Collections;

public class CoinController : MonoBehaviour
{
    private CoinManager _coinManager;
    private AudioSource _audioSource;
    [SerializeField] private TextMeshProUGUI _text;

    private void Awake()
    {
        _coinManager = CoinManager.Instance;
        _audioSource = GetComponent<AudioSource>(); 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _text.text = _coinManager.OnCollectCoin().ToString();
            _audioSource.Play();
            StartCoroutine(WaitToDestroy());
        }
    }

    private IEnumerator WaitToDestroy()
    {
        yield return new WaitForSeconds(0.4f);
        Destroy(gameObject);
    }

}
