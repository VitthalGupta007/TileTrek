using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    void Awake() 
    {
        int numMusicPlayers = FindObjectsOfType<BackgroundMusic>().Length;
        if (numMusicPlayers > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
