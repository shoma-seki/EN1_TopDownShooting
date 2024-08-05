using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundGunScript : MonoBehaviour
{
    private PlayerScript player;

    private Vector3 rotate;

    // Start is called before the first frame update
    void Start()
    {
        player = FindAnyObjectByType<PlayerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        rotate.y += 45.0f * Time.deltaTime;
        transform.rotation = Quaternion.Euler(rotate);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            player.isGrabGun = true;
            Destroy(gameObject);
        }
    }
}
