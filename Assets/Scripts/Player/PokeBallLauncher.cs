using UnityEngine;

public class PokeBallLauncher : MonoBehaviour
{
    //Rotacion de la camara para tomar en cuenta el vector de la pokebola
    [SerializeField] GameObject cameraRot;

    //Prefabs y efectos secundarios
    [SerializeField] GameObject pokeballProjectile;

    //offset de donde se lanzara la pokebola
    public GameObject pokeballLaunchPos;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && !GameManager.Instance.isPaused)
        {
            /// Lanzar pokebola
            Instantiate(pokeballProjectile, pokeballLaunchPos.transform.position, cameraRot.transform.rotation);
            SoundManager.SoundInstance.PlayLaunchSFX();
        }
    }

}
