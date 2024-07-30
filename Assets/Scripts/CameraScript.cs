using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    private Camera cam;
    private Vector3 hitPoint;

    private PlayerScript player;
    private LayerMask playerLayer;
    private GunScript gun;

    private Vector3 position;
    private Vector3 endPosition;

    private Vector3 positionPlus = new Vector3(0, 12f, -15.45f);
    [SerializeField] private float t;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;

        player = FindAnyObjectByType<PlayerScript>();
        playerLayer = LayerMask.GetMask("Player");
        gun = FindAnyObjectByType<GunScript>();

        position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        CameraMove();
        RayCasting();
    }

    private void CameraMove()
    {
        if (Vector3.Distance(player.transform.position, hitPoint) > 10f)
        {
            endPosition = Vector3.Lerp(player.transform.position,
               (hitPoint - player.transform.position).normalized * 10f, 0.5f);
        }
        else
        {
            endPosition = Vector3.Lerp(player.transform.position, hitPoint - player.transform.position, 0.5f);
        }
        position = Vector3.Lerp(position, endPosition, t);
        transform.position = position + positionPlus;
    }

    private void RayCasting()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            if (hit.collider.gameObject.tag == "Ground")
            {
                hitPoint = hit.point;
                player.SetAimPosition(hitPoint);
                gun.CanShot = true;
            }
        }

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, playerLayer))
        {
            if (hit.collider.gameObject.tag == "Player")
            {
                gun.CanShot = false;
            }
        }
    }
}
