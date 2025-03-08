using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    //UI Manager
    [SerializeField] GameObject pokedexMenu;
    public TextMeshProUGUI totalPokemonCountTxt, pokemonCaughtTxt;

    //Datos del juego
    public bool isPaused;
    public List<string> pokemonNameList;
    public GameObject[] pokemonInGame;
    public int pokemonCaught;

    private void Start()
    {
        pokemonCaught = 0;
    }

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        pokemonInGame = GameObject.FindGameObjectsWithTag("Pokemon");
        totalPokemonCountTxt.text = "/ " + pokemonInGame.Length;

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isPaused)
        {
            PauseGame();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && isPaused)
        {
            UnPauseGame();
        }
    }

    public void AddPokemonToList(string name)
    {
        pokemonNameList.Add(name);
        pokemonCaught++;
        pokemonCaughtTxt.text = pokemonCaught.ToString();
    }

    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0;
        pokedexMenu.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
    }

    public void UnPauseGame()
    {
        isPaused = false;
        Time.timeScale = 1;
        pokedexMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }

}
