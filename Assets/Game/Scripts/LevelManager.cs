using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameHandleSO GameHandle;
    public List<SceneAsset> SceneAssets;

    public static LevelManager Instance { get; private set; }
    private int LevelIndex = 0;

    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        DontDestroyOnLoad(this);
    }

    public void LoadNextLevel()
    {
        LevelIndex++;
        GameHandle.OpenLevel(SceneAssets[LevelIndex]);
    }
}
