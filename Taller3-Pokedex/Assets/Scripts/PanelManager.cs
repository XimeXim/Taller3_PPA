using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelManager : MonoBehaviour
{
    public Image[] pokemonImages;  // Array de UI Image para los diferentes Pokémon

    void Start()
    {
        // Desactivar todas las imágenes
        foreach (Image img in pokemonImages)
        {
            img.gameObject.SetActive(false);
        }

        // Activar la imagen correspondiente al Pokémon detectado
        int index = PokemonData.pokemonIndex;
        if (index >= 0 && index < pokemonImages.Length)
        {
            pokemonImages[index].gameObject.SetActive(true);
        }
    }
}
