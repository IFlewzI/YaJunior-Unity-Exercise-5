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

        _signalisationInJob = StartCoroutine(SwitchSignalisationState(_signalisationMaxVolume));
        _isSignalisationInJob = true;
    }

    public void EndSignalisation()
    {
        if (_signalisationInJob != null)
            StopCoroutine(_signalisationInJob);

        _signalisationInJob = StartCoroutine(SwitchSignalisationState(_signalisationMinVolume));
        _isSignalisationInJob = false;
    }

    private IEnumerator SwitchSignalisationState(float targetVolume)
    {
        if (!_isSignalisationInJob)
        {
            _audioSource.volume = 0;
            _audioSource.Play();
        }

        while (_audioSource.volume != targetVolume)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, targetVolume, _signalisationVolumeStep * Time.deltaTime);

            yield return null;
        }

        if (_isSignalisationInJob)
            _audioSource.Stop();
    }
}
