using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* * * The script is tied to the text - "Press any key to start". 
 * Starts the tape animation and text animation, hides the game object responsible for the boot menu, and opens the main game * * */
public class AnyKeyStartScript : MonoBehaviour
{
    RectTransform rt;   // for text
    Vector3 normalScale; // normal Scale of our Text

    [SerializeField] GameObject loadScene;
    [SerializeField] GameObject gameScene;
    [SerializeField] GameObject VideoTape;

    Animator videoTapeAnimator;     // Animatior for Video Tape

    float phase;    // phase for Text sin-Animation
    [SerializeField] float speed;
    
    AudioSource aSource;
    [SerializeField] AudioClip tapeClip;
    [SerializeField] float afterSec;    // Play Clip after the time after pressing Key 

    bool isKey;    // was Key pressed


    void Start()
    {
        aSource = GetComponent<AudioSource>();
        rt = GetComponent<RectTransform>();
        videoTapeAnimator = VideoTape.GetComponent<Animator>();

        normalScale = rt.localScale;
    }


    void Update()
    {
        if (Input.anyKey && !isKey)
        {
            /* * * loading actual Game * * */
            isKey = true;
            videoTapeAnimator.SetTrigger("ShowAnimation");  // Plays the Video-Tape Animation

            Invoke(nameof(PlayClip), afterSec);     // Plays Video-Tape-Clip
            Invoke(nameof(DestroyLoad), 2f);    // Load actual game
        }

        /* * * Text animation * * */
        rt.localScale = (Mathf.Abs(Mathf.Sin(phase * speed)) + 1) * normalScale;
        phase += Time.deltaTime;
    }


    void DestroyLoad()
    {
        loadScene.SetActive(false);
        gameScene.SetActive(true);
        Destroy(gameObject);
    }

    void PlayClip() => aSource.PlayOneShot(tapeClip);
}
