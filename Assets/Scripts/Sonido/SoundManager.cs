using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager SoundInstance { get; private set; }

    [SerializeField] AudioSource launchPokeballSFX, pokemonCaughtSFX, saveGameSFX;

    private void Start()
    {
        if (SoundInstance == null)
        {
            SoundInstance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayLaunchSFX()
    {
        launchPokeballSFX.Play();
    }
    public void PlayCaughtSFX()
    {
        pokemonCaughtSFX.Play();
    }
    public void PlaySaveGameSFX()
    {
        saveGameSFX.Play();
    }
}
