using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Vuforia;

public class ChangeScene : MonoBehaviour
{
    public int pokemonIndex;  // Índice que corresponde al Pokémon detectado
    private bool sceneLoaded = false;
    private ObserverBehaviour observerBehaviour;

    void Awake()
    {
        observerBehaviour = GetComponentInParent<ObserverBehaviour>();
    }

    void OnEnable()
    {
        if (observerBehaviour)
        {
            observerBehaviour.OnTargetStatusChanged += OnObserverStatusChanged;
        }
    }

    void OnDisable()
    {
        if (observerBehaviour)
        {
            observerBehaviour.OnTargetStatusChanged -= OnObserverStatusChanged;
        }
    }

    void OnObserverStatusChanged(ObserverBehaviour behaviour, TargetStatus targetStatus)
    {
        // Verifica si el target fue detectado y la escena aún no ha sido cargada
        if (targetStatus.Status == Status.TRACKED && !sceneLoaded)
        {
            // Aquí puedes activar la interacción en el modelo 3D
            EnableInteraction(true);
        }
        else
        {
            // Desactivar la interacción si el target no está siendo rastreado
            EnableInteraction(false);
        }
    }

    void EnableInteraction(bool enabled)
    {
        // Habilitar o deshabilitar la interacción en el modelo 3D
        var colliders = GetComponentsInChildren<Collider>();
        foreach (var collider in colliders)
        {
            collider.enabled = enabled;
        }
    }

    void OnMouseDown()
    {
        if (!sceneLoaded)
        {
            sceneLoaded = true;
            PokemonData.pokemonIndex = pokemonIndex;
            SceneManager.LoadScene(1);
        }
    }
}
