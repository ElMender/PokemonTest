using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;

public class PokeAPIManager : MonoBehaviour
{
    public static PokeAPIManager PokeInstance { get; private set; }



    [Header("Basic Info")]
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text idText;
    [SerializeField] private TMP_Text typeText;
    [SerializeField] private TMP_Text heightText;
    [SerializeField] private TMP_Text weightText;
    [SerializeField] private Image pokemonImage;

    [Header("Stats")]
    [SerializeField] private TMP_Text hpText;
    [SerializeField] private TMP_Text attackText;
    [SerializeField] private TMP_Text defenseText;
    [SerializeField] private TMP_Text spAttackText;
    [SerializeField] private TMP_Text spDefenseText;
    [SerializeField] private TMP_Text speedText;

    private readonly string baseUrl = "https://pokeapi.co/api/v2/pokemon/";

    private void Start()
    {
        if (PokeInstance == null)
        {
            PokeInstance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // llamar metodo mediante un boton para buscar pokemon
    public void FetchPokemonData(string pokemonName)
    {
        StartCoroutine(GetPokemonData(pokemonName.ToLower().Trim()));
    }

    private IEnumerator GetPokemonData(string pokemonName)
    {
        string url = baseUrl + pokemonName;
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"Error: {request.error}");
                yield break;
            }

            PokemonData data = JsonUtility.FromJson<PokemonData>(request.downloadHandler.text);
            UpdateUI(data);
            StartCoroutine(LoadPokemonSprite(data.sprites.front_default));
        }
    }

    private void UpdateUI(PokemonData data)
    {
        // Informacion Basica
        nameText.text = data.name.ToUpper();
        idText.text = $"#{data.id.ToString("000")}";
        typeText.text = string.Join(" / ", data.types.Select(t => t.type.name.ToUpper()));
        heightText.text = $"{(data.height / 10f):0.0}m";
        weightText.text = $"{(data.weight / 10f):0.0}kg";

        // Stats
        hpText.text = GetStat(data.stats, "hp").ToString();
        attackText.text = GetStat(data.stats, "attack").ToString();
        defenseText.text = GetStat(data.stats, "defense").ToString();
        spAttackText.text = GetStat(data.stats, "special-attack").ToString();
        spDefenseText.text = GetStat(data.stats, "special-defense").ToString();
        speedText.text = GetStat(data.stats, "speed").ToString();
    }

    //Metodo para conseguir el valor desde el nombre del stat
    private int GetStat(List<StatEntry> stats, string statName)
    {
        return stats.FirstOrDefault(s => s.stat.name == statName)?.base_stat ?? 0;
    }


    // Llamar al sprite desde el url
    private IEnumerator LoadPokemonSprite(string spriteUrl)
    {
        using (UnityWebRequest request = UnityWebRequestTexture.GetTexture(spriteUrl))
        {
            yield return request.SendWebRequest();
            if (request.result == UnityWebRequest.Result.Success)
            {
                Texture2D texture = DownloadHandlerTexture.GetContent(request);
                pokemonImage.sprite = Sprite.Create(texture,new Rect(0, 0, texture.width, texture.height),Vector2.zero);
            }
        }
    }
}

// JSON Data Structure (Modificar en torno a que tipo de datos se necesitaran)
[System.Serializable]
public class PokemonData
{
    public string name;
    public int id;
    public int height;
    public int weight;
    public List<TypeEntry> types;
    public List<StatEntry> stats;
    public SpriteData sprites;
}

[System.Serializable]
public class SpriteData
{
    public string front_default;
}

[System.Serializable]
public class TypeEntry
{
    public TypeInfo type;
}

[System.Serializable]
public class TypeInfo
{
    public string name;
}

[System.Serializable]
public class StatEntry
{
    public int base_stat;
    public StatInfo stat;
}

[System.Serializable]
public class StatInfo
{
    public string name;
}
