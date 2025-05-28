using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*** Script, that makes Sprite on Background GameObject darker or brighter ***/
public class BackgroundChanger : MonoBehaviour
{
    SpriteRenderer sr;
    [SerializeField] float DarkerVal = 0.1f; // factor for darker or brighter

    private void Awake() => sr = GetComponent<SpriteRenderer>();

    void Update()
    {
        // Color = RGB_Transparency 
        if (Input.GetKeyDown(KeyCode.Alpha7)) sr.color -= new Color(DarkerVal, DarkerVal, DarkerVal, 0);
        if (Input.GetKeyDown(KeyCode.Alpha8)) sr.color += new Color(DarkerVal, DarkerVal, DarkerVal, 0);
    }
}
