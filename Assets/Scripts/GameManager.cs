using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool isCapsule;
    public int nbCaps;

    [SerializeField] private TMP_Text tuto;
    [SerializeField] TypeSentence _typeSentence;
    [SerializeField] List<AudioClip> keyboardClips = new List<AudioClip>();
    [SerializeField] AudioSource audioSource;

    private bool _endParty = false;

    private void Start()
    {
        StartCoroutine(SetUp());
    }
    private void Update()
    {
        if(nbCaps >= 5 && !_endParty)
        {
            StartCoroutine(RestartAuto());
        }
    }

    private IEnumerator SetUp()
    {

        _typeSentence.WriteMachinEffect("Click on the stars to make them explode", tuto, 0.02f);
        audioSource.PlayOneShot(keyboardClips[1]);
        yield return new WaitForSeconds(0.5f);

        audioSource.PlayOneShot(keyboardClips[0]);

        yield return new WaitForSeconds(2);
        tuto.text = "";
        audioSource.PlayOneShot(keyboardClips[0]);
        _typeSentence.WriteMachinEffect("Press escape to start over", tuto, 0.02f);
        yield return new WaitForSeconds(3);

        tuto.gameObject.SetActive(false);
    }

    private IEnumerator RestartAuto()
    {
        _endParty = true;
        tuto.text = "";
        tuto.gameObject.SetActive(true);
        audioSource.PlayOneShot(keyboardClips[0]);

        _typeSentence.WriteMachinEffect("you found all the time capsules", tuto, 0.02f);
        yield return new WaitForSeconds(3);
        tuto.text = "";
        audioSource.PlayOneShot(keyboardClips[1]);

        _typeSentence.WriteMachinEffect("The game will start again in", tuto, 0.02f);
        yield return new WaitForSeconds(3);
        tuto.text = "";
        _typeSentence.WriteMachinEffect("3", tuto, 0.02f);
        yield return new WaitForSeconds(0.9f);
        tuto.text = "";

        _typeSentence.WriteMachinEffect("2", tuto, 0.02f);
        yield return new WaitForSeconds(0.9f);
        tuto.text = "";

        _typeSentence.WriteMachinEffect("1", tuto, 0.02f);
        yield return new WaitForSeconds(0.9f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }
}
