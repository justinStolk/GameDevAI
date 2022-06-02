using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeBomb : MonoBehaviour
{

    [SerializeField] private GameObject smokeScreen;
    [SerializeField] private float smokeTime = 2.5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject smoke = Instantiate(smokeScreen, transform.position, Quaternion.identity);
        Destroy(smoke, smokeTime);
        Destroy(this.gameObject);
    }

}
