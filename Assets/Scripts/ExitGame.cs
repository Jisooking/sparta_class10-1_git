using Unity.VisualScripting;
using UnityEngine;

public class ExitGame : MonoBehaviour
{
    public void Exitgame()
    {
        UnityEditor.EditorApplication.isPlaying = false;
    }
}
