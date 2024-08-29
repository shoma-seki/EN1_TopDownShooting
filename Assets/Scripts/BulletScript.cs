using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    private Vector3 position;
    private Vector3 direction;
    private Vector3 velocity;
    private float speed = 50;

    private Vector3 rotation;

    private bool isEnd;

    private LineRenderer lineRenderer;
    private Vector3 startP;
    private Vector3 endP;

    PlayerScript player;
    GunScript gun;

    // Start is called before the first frame update
    void Start()
    {
        player = FindAnyObjectByType<PlayerScript>();
        gun = FindAnyObjectByType<GunScript>();
        position = transform.position;
        direction = player.aimDirection + (player.transform.position - gun.transform.position);

        lineRenderer = GetComponent<LineRenderer>();
        endP = gun.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        velocity = direction.normalized * speed * Time.deltaTime;
        position += velocity;
        transform.position = position;

        if (Vector3.Distance(player.transform.position, transform.position) >= 5f)
        {
            startP = transform.position;
            endP = transform.position - direction.normalized * 5f;
        }
        else
        {
            startP = transform.position;
        }

        lineRenderer.SetPosition(0, startP);
        lineRenderer.SetPosition(1, endP);

        rotation = direction.normalized;
        transform.rotation = Quaternion.LookRotation(rotation);

        DestroySelf();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Wall" || other.gameObject.tag == "Switch" ||
            other.gameObject.tag == "MoveWall" || other.gameObject.tag == "Enemy")
        {
            isEnd = true;
        }
    }

    private void DestroySelf()
    {
        if (isEnd)
        {
            direction = Vector3.zero;
            speed = 0;
            startP = Vector3.Lerp(startP, endP, 0.2f);
            if (Vector3.Distance(startP, endP) < 0.05f)
            {
                Destroy(gameObject);
            }
        }
    }
}
