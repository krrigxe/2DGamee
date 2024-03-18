using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthPickup : MonoBehaviour
{
    public int healthRestore = 20;
    public Vector3 spinRotationSpeed= new Vector3(0,100,0);

    AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        damagable Damagable=collision.GetComponent<damagable>();

        if (Damagable)
        {
           bool maxHealed= Damagable.Heal(healthRestore);

            if (maxHealed)
            {
                if (audioSource)
                {
                    AudioSource.PlayClipAtPoint(audioSource.clip, gameObject.transform.position, audioSource.volume);

                }

                Destroy(gameObject);
            }
              
        }
            
    }
    private void Update()
    {
        transform.eulerAngles += spinRotationSpeed * Time.deltaTime;
    }

}
