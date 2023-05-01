using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance;

    [HideInInspector] public int sheepSaved;
    [HideInInspector] public int sheepDropped;
    [HideInInspector] public float shootInterval;
    [HideInInspector] public int level;

    public int sheepDroppedBeforeGameOver;
    public int sheepSavedBeforeGameOver;
    public int sheepSavedBeforeLevel2;
    public int sheepSavedBeforeLevel3;
    public SheepSpawner sheepSpawner;

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
        shootInterval = 0.8f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Title");
        }
    }

    public void SavedSheep()
    {
        sheepSaved++;
        UIManager.Instance.UpdateSheepSaved();

        if (sheepSaved == sheepSavedBeforeLevel2)
        {
            shootInterval = 1.2f;
            UIManager.Instance.ShowNextLevelWindow();
        }
        else if (sheepSaved == sheepSavedBeforeLevel3)
        {
            shootInterval = 1.8f;
            UIManager.Instance.ShowNextLevelWindow();
        }
        else if (sheepSaved == sheepSavedBeforeGameOver)
        {
            Win();
        }
    }

    private void Win()
    {
        sheepSpawner.canSpawn = false;
        sheepSpawner.DestroyAllSheep();
        UIManager.Instance.ShowWinWindow();
        SoundManager.Instance.StopSheepFarmClip();
    }

    private void GameOver()
    {
        sheepSpawner.canSpawn = false;
        sheepSpawner.DestroyAllSheep();
        UIManager.Instance.ShowGameOverWindow();
        SoundManager.Instance.StopSheepFarmClip();
    }

    public void DroppedSheep()
    {
        sheepDropped++;
        UIManager.Instance.UpdateSheepDropped();

        if (sheepDropped == sheepDroppedBeforeGameOver)
        {
            GameOver();
        }
    }
}
