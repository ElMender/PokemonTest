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

    int pokemonLoadOrder = 0;

    private void Awake()
    {
        Time.timeScale = 1;
        pokemonLoadOrder = 0;

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        Cursor.lockState = CursorLockMode.Locked;
        pokemonInGame = GameObject.FindGameObjectsWithTag("Pokemon");
        totalPokemonCountTxt.text = "/ " + pokemonInGame.Length;

        LoadSavedPkmn();
        pokemonCaught = 0;
        pokemonCaughtTxt.text = pokemonCaught.ToString();
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
    void LoadSavedPkmn()
    {
        if (PlayerPrefs.HasKey("PokemonInv" + pokemonLoadOrder))
        {
            AddPokemonToList(PlayerPrefs.GetString("PokemonInv" + pokemonLoadOrder));
            pokemonLoadOrder++;
            LoadSavedPkmn();
        }
    }

}
