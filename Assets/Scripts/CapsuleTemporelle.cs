using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CapsuleTemporelle : MonoBehaviour
{
    [SerializeField] private float capsuleRadius;
    [SerializeField] CameraFollow _cam;
    [SerializeField] private GameObject player;
    [SerializeField] private bool _isActive = false;
    [SerializeField]  private TMP_Text _capsTxt;
    private int nbCaps = 0;
    [SerializeField] private TMP_Text _uiCaps;
    [SerializeField] GameManager gameManager;

    [SerializeField] TypeSentence _typeSentence;

    [SerializeField] List<AudioClip> capsSounds = new List<AudioClip>();
    [SerializeField] AudioSource capsAudioSource;
    void Start()
    {
        _cam = FindObjectOfType<Camera>().GetComponent<CameraFollow>();
        capsAudioSource = FindAnyObjectByType<AudioSource>();
    }

    void Update()
    {
        if (!_isActive)
        {
            StartCoroutine(ShowCapsule());
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, capsuleRadius);
    }
    private IEnumerator ShowCapsule()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, capsuleRadius);
        foreach (Collider collider in colliders)
        {
            if (collider.tag == "Player")
            {
                _cam.player = this.gameObject;
                _capsTxt.gameObject.SetActive(true);
                _typeSentence.WriteMachinEffect("You found Me", _capsTxt, 0.03f);
                _isActive = true;
                gameManager.nbCaps++;
                _uiCaps.text = gameManager.nbCaps.ToString();
                capsAudioSource.PlayOneShot(capsSounds[Random.Range(0,capsSounds.Count)], 0.6f);
                gameManager.isCapsule = true;
                yield return new WaitForSeconds(1.5f);
                _cam.player = player;
                gameManager.isCapsule = false;

            }
        }

       
    }
}
