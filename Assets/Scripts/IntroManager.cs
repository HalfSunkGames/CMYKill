using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class IntroManager : MonoBehaviour
{
    [SerializeField] private VideoPlayer intro;
    [SerializeField] private VideoPlayer cinematic;
    private AudioSource introMusic;
    private InputAction escape;

    public PlayerInputActions playerInputActions;
    // Start is called before the first frame update
    void Start()
    {
        playerInputActions = new PlayerInputActions();
        escape = playerInputActions.Player.Escape;
        escape.Enable();
        escape.performed += Skip;
        introMusic = GetComponent<AudioSource>();
        Invoke("StartIntro", 1f);
    }

    void StartIntro()
    {
        intro.Play();
        introMusic.Play();
        Invoke("StartCinematic", 6f);
    }

    void StartCinematic()
    {
        cinematic.Play();
        Invoke("StartGame", 26f);
    }

    void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    private void Skip(InputAction.CallbackContext contenxt)
    {
        escape.Disable();
        StartGame();
    }

}
