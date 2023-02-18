using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PauseGame : MonoBehaviour
{

    private bool paused;
    public GameObject pauseScreen;
    public GameObject crosshair;
    public GameObject lightObject;
    public GameObject player;
    public GameObject HUD;
    [SerializeField]
    private bool playBGM;
    private Light flashLight;
    private float originalLight;
    private InteractionSystem interact;

    // Start is called before the first frame update
    void Start()
    {
        paused = false;
        flashLight = lightObject.GetComponent<Light>();
        originalLight = flashLight.intensity;
        pauseScreen.SetActive(false);
        interact = GameObject.Find("Special Agent").GetComponent<InteractionSystem>();
        playBGM = true;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.P)) {
            GameObject bgm = GameObject.Find("background music");
            bgm.GetComponent<AudioSource>().enabled = playBGM;
            playBGM = !playBGM;
        }


        if (Input.GetKeyDown(KeyCode.Escape)
        && !player.GetComponent<WeaponsAndTools>().isReloading() 
        && !Input.GetKey(KeyCode.Mouse1) && !interact.isPressed) {

            paused = !paused;

            if (paused && Time.timeScale != 0) {
                Time.timeScale = 0;
                flashLight.intensity = originalLight * 0.3f;
                crosshair.SetActive(false);
                pauseScreen.SetActive(true);
                HUD.SetActive(false);
                GameObject.Find("background music").GetComponent<AudioSource>().Pause();
            }
            else {
                Time.timeScale = 1;
                flashLight.intensity = originalLight;
                crosshair.SetActive(true);
                pauseScreen.SetActive(false);
                HUD.SetActive(true);
                GameObject.Find("background music").GetComponent<AudioSource>().Play();
            }
        }

        if (pauseScreen.activeSelf) {
            if (Input.GetKeyDown(KeyCode.Delete) || Input.GetKeyDown(KeyCode.Backspace)) {
                    Application.Quit();
                    // EditorApplication.isPlaying = false;
                }
        }
    }
}
