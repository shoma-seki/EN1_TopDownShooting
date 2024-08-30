using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClearEnshutu : MonoBehaviour
{
    ClearScript clear;

    Vector3 scale = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        clear = FindAnyObjectByType<ClearScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (clear.isClear)
        {
            if (scale.x < 1.0f)
            {
                scale += new Vector3(2f, 2f, 0.1f) * Time.deltaTime;
            }
        }

        transform.localScale = scale;
    }
}
