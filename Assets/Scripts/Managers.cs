using UnityEngine;

public enum GameLevel
{
    Easy,
    Normal,
    Hard,
    Hidden,
    Infinite,

    Zombie,
}

public class Managers : MonoBehaviour
{
    static public Managers Instance;

    public GameLevel gameType;

    public bool unlockNormal;
    public bool unlockHard;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
