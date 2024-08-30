using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class ClearScript : MonoBehaviour
{
    private PlayerScript player;

    public bool isClear;

    // Start is called before the first frame update
    void Start()
    {
        player = FindAnyObjectByType<PlayerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.z > 110f)
        {
            isClear = true;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("GameScene");
        }
    }
}
