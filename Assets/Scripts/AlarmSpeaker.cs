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
        _isSignalisationInJob = true;
        RunSwitchSignalisationStateCoroutine(ref _signalisationInJob, _signalisationMaxVolume);
    }

    public void EndSignalisation()
    {
        _isSignalisationInJob = false;
        RunSwitchSignalisationStateCoroutine(ref _signalisationInJob, _signalisationMinVolume);
    }

    private void RunSwitchSignalisationStateCoroutine(ref Coroutine coroutineInJob, float targetVolume)
    {
        if (coroutineInJob != null)
            StopCoroutine(coroutineInJob);

        coroutineInJob = StartCoroutine(SwitchSignalisationState(targetVolume));
    }

    private IEnumerator SwitchSignalisationState(float targetVolume)
    {
        if (_isSignalisationInJob)
        {
            _audioSource.volume = 0;
            _audioSource.Play();
        }

        while (_audioSource.volume != targetVolume)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, targetVolume, _signalisationVolumeStep * Time.deltaTime);

            yield return null;
        }

        if (!_isSignalisationInJob)
            _audioSource.Stop();
    }
}
