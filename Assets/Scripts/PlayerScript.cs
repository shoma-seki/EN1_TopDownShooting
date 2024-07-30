using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private Vector3 position;
    private Vector3 direction;
    private Vector3 velocity;
    private float speed = 4f;

    private Vector3 rotation;

    private Vector3 aimPosition;
    public Vector3 aimDirection;

    public bool isGrabGun = false;
    private GameObject gun;

    // Start is called before the first frame update
    void Start()
    {
        position = transform.position;
        rotation = transform.rotation.eulerAngles;

        gun = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        direction.z = Input.GetAxis("Vertical");
        direction.x = Input.GetAxis("Horizontal");
        velocity = direction * speed * Time.deltaTime;
        position += velocity;
        transform.position = position;

        aimDirection = aimPosition - transform.position;
        aimDirection.y = 0f;
        rotation = aimDirection.normalized;
        transform.rotation = Quaternion.LookRotation(rotation);

        if (isGrabGun)
        {
            gun.SetActive(true);
        }
    }

    public void SetAimPosition(Vector3 aimPosition)
    {
        this.aimPosition = aimPosition;
    }
}
