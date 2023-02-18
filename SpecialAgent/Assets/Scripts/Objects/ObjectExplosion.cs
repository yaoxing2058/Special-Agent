using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectExplosion : MonoBehaviour
{
    private AudioSource explosion;
    private AudioClip explosionSound;
    public Transform explosionPrefab;

    // Start is called before the first frame update
    void Start()
    {
        explosion = gameObject.GetComponent<AudioSource>();
        explosionSound = explosion.clip;
    }

    void OnTriggerEnter(Collider other) {
        Instantiate(explosionPrefab, transform.position, transform.rotation);
        explosion.Play();
        Destroy(gameObject, 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
