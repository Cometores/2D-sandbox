using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/* * * A script that shows the walkthrough when you press Tab * * */
public class WalkthroughScript : MonoBehaviour
{
    [SerializeField] GameObject walkthrough;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
            walkthrough.SetActive(!walkthrough.active);
    }
}
