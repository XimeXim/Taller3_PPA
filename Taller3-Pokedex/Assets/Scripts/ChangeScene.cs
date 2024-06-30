using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Vuforia;

public class ChangeScene : MonoBehaviour
{
    public int pokemonIndex;  // �ndice que corresponde al Pok�mon detectado
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
        // Verifica si el target fue detectado y la escena a�n no ha sido cargada
        if (targetStatus.Status == Status.TRACKED && !sceneLoaded)
        {
            // Aqu� puedes activar la interacci�n en el modelo 3D
            EnableInteraction(true);
        }
        else
        {
            // Desactivar la interacci�n si el target no est� siendo rastreado
            EnableInteraction(false);
        }
    }

    void EnableInteraction(bool enabled)
    {
        // Habilitar o deshabilitar la interacci�n en el modelo 3D
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
