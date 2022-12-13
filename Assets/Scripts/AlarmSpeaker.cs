using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AlarmSpeaker : MonoBehaviour
{
    [SerializeField] private float _signalisationMinVolume;
    [SerializeField] private float _signalisationMaxVolume;
    [SerializeField] private float _signalisationVolumeStep;

    private AudioSource _audioSource;
    private Coroutine _signalisationInJob;
    private bool _isSignalisationInJob;

    private void Start()
    {
        _audioSource = gameObject.GetComponent<AudioSource>();
    }

    public void StartSignalisaton()
    {
        if (_signalisationInJob != null)
            StopCoroutine(_signalisationInJob);

        _signalisationInJob = StartCoroutine(TurnSignalisationOn());
    }

    public void EndSignalisation()
    {
        if (_signalisationInJob != null)
            StopCoroutine(_signalisationInJob);

        _signalisationInJob = StartCoroutine(TurnSignalisationOff());
    }

    private IEnumerator TurnSignalisationOn()
    {
        _audioSource.volume = _signalisationMinVolume;
        _audioSource.Play();

        while (_audioSource.volume < _signalisationMaxVolume)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, _signalisationMaxVolume, _signalisationVolumeStep * Time.deltaTime);

            yield return null;
        }
    }

    private IEnumerator TurnSignalisationOff()
    {
        _audioSource.volume = _signalisationMaxVolume;
        
        while (_audioSource.volume > _signalisationMinVolume)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, _signalisationMinVolume, _signalisationVolumeStep * Time.deltaTime);

            yield return null;
        }

        _audioSource.Stop();
    }
}
