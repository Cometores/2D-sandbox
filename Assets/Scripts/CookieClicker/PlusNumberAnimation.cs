using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlusNumberAnimation : MonoBehaviour
{
    bool isTransp;
    float transp = 1;
    [SerializeField] float moveSpeed = 20;
    TextMeshProUGUI txt;

    void Start()
    {
        Invoke(nameof(SetTransp), 0.5f);
        txt = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        transform.position += (Vector3.up * Time.deltaTime * moveSpeed);
        if (isTransp)
        {
            transp -= Time.deltaTime;
            txt.color = new Color(1, 1, 1, transp);
        }
        if (transp <= 0)
            Destroy(gameObject);
     }


    void SetTransp()
    {
        isTransp = true;
    }

}
