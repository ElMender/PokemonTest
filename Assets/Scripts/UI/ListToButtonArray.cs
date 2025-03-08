using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ListoToButtonArray : MonoBehaviour
{
    public GameObject buttonPrefab;
    public List<string> Pokemon;

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            RefreshScrollView();
        }
    }

    public void RefreshScrollView()
    {
        Pokemon = GameManager.Instance.pokemonNameList;
        ClearScrollView();
        PopulateScrollView();
    }

    void PopulateScrollView()
    {
        foreach (string name in Pokemon)
        {
            GameObject newButton = Instantiate(buttonPrefab, transform);
            newButton.GetComponentInChildren<TextMeshProUGUI>().text = name;

            newButton.GetComponent<Button>().onClick.AddListener(() => OnButtonClick(name));
        }
        LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());
    }

    void ClearScrollView()
    {
        // Destruir Anteriores botones para actualizar informacion)
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }

    void OnButtonClick(string buttonName)
    {
        PokeAPIManager.PokeInstance.FetchPokemonData(buttonName);
        Debug.Log("Clicked: " + buttonName);
    }
}