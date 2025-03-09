using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class SaveSystem : MonoBehaviour
{
    public static SaveSystem SaveInstance { get; private set; }

    int ListPos = 0;

    private void Awake()
    {
        if (SaveInstance == null)
        {
            SaveInstance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SaveAllData()
    {
        SavePokemonList();
    }

    public void DeleteAllData()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void SavePokemonList()
    {
        foreach (string pokemon in GameManager.Instance.pokemonNameList)
        {
            PlayerPrefs.SetString("PokemonInv" + ListPos , pokemon);
            ListPos++;
        }
    }
}
