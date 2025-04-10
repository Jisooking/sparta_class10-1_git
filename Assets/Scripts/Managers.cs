using UnityEngine;

//모드 나타내기
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

    //게임 모드
    public GameLevel gameType;

    //난이도 해금 유무 변수
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
