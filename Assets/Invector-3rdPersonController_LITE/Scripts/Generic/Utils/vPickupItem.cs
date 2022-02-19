using UnityEngine;
using System.Collections;

public class vPickupItem : MonoBehaviour
{
    AudioSource _audioSource;
    public AudioClip _audioClip;
    public GameObject _particle;

    public int point;

    public bool Enter = false;

    private Transform player;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();

    }

    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            other.GetComponent<CollisionManager>().ChangePoint(point);

            if (!Enter)
            {
                _audioSource.PlayOneShot(_audioClip);
                Enter = true;
            }


        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Enter = false;
            other.GetComponent<CollisionManager>().ChangePoint(-1);
            //_audioSource.PlayOneShot(_audioClip);

        }
    }
}