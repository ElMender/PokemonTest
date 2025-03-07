using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using TMPro;
using UnityEngine.UI;

public class PokeAPIManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField pokemonInput;
    //[SerializeField] private TMP_Text displayText;
    //[SerializeField] private Image pokemonSprite;

    private readonly string baseUrl = "https://pokeapi.co/api/v2/pokemon/";

    // llamar metodo mediante un boton para buscar pokemon
    public void FetchPokemonData()
    {
        StartCoroutine(GetPokemonData(pokemonInput.text.ToLower().Trim()));
    }

    private IEnumerator GetPokemonData(string pokemonName)
    {
        string url = baseUrl + pokemonName;
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError ||
                request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError($"Error: {request.error}");
            }
            else
            {
                // Parse JSON data
                PokemonData data = JsonUtility.FromJson<PokemonData>(request.downloadHandler.text);
                //displayText.text = $"Name: {data.name}\nID: {data.id}";

                // Cargar Sprite del pokemon
                StartCoroutine(LoadPokemonSprite(data.sprites.front_default));
            }
        }
    }

    // Llamar al sprite desde el url
    private IEnumerator LoadPokemonSprite(string spriteUrl)
    {
        using (UnityWebRequest request = UnityWebRequestTexture.GetTexture(spriteUrl))
        {
            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Failed to load sprite");
            }
            else
            {
                Texture2D texture = DownloadHandlerTexture.GetContent(request);
                //pokemonSprite.sprite = Sprite.Create(texture,new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
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
    public SpriteData sprites;
}

[System.Serializable]
public class SpriteData
{
    public string front_default;
}
