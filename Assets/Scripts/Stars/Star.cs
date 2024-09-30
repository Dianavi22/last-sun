using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
    private bool isCharging;
    [SerializeField] private float explosionRadius;
    [SerializeField] private GameObject _targetPlayer;
    [SerializeField] GameManager _gameManager;
    [SerializeField] private GameObject _sprite;
    private Camera _camera;

    [SerializeField] ParticleSystem _explosePart;
    [SerializeField] ParticleSystem _rotatePart;

    private bool isDie = false;
    void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _camera = FindObjectOfType<Camera>();

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !_gameManager.isCapsule)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject == gameObject)
                {
                    StartCoroutine(CountExplosion());
                    explosionRadius += 1;
                    _sprite.transform.localScale = new Vector3(explosionRadius*2, explosionRadius*2, explosionRadius*2);
                }
            }
        }

        try
        {
            gameObject.transform.LookAt(_targetPlayer.transform);

        }
        catch
        {
            return;
        }
    }
    private IEnumerator DestroyStar()
    {
        if (!isDie)
        {
            isDie = true;
            this._sprite.SetActive(false);
            this.GetComponent<MeshRenderer>().enabled = false;
            _rotatePart.Stop();
            _explosePart.Play();
            yield return new WaitForSeconds(3);
            Destroy(gameObject);
        }
      
    }


    private IEnumerator CountExplosion()
    {
        isCharging = true;
           yield return new WaitForSeconds(3);
        ExplodeStar();
        StartCoroutine(DestroyStar());

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }

    void ExplodeStar()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider collider in colliders)
        {
            if (collider.tag == "Player")
            {
                Vector3 direction = collider.transform.position - transform.position;

                direction = direction.normalized;

                Rigidbody playerRigidbody = collider.gameObject.GetComponent<Rigidbody>();
                playerRigidbody.AddForce(direction * 25, ForceMode.Impulse);
                collider.GetComponent<Player>().isMove = true;
                _camera.GetComponent<ShakyCame>().isShaking = true;

            }
        }
    }
}
