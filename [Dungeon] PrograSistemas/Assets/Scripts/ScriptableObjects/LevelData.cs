using UnityEngine;

[CreateAssetMenu(fileName = "NewLevelData", menuName = "ScriptableObjects/Levels/Levels index data", order = 1)]
public class LevelData : ScriptableObject
{
    private readonly string menuScene = "Menu";
    public string MenuScene => menuScene;
    
    private readonly string victoryScene = "Victory";
    public string VictoryScene => victoryScene;
    
    private readonly string defeatScene = "Defeat";
    public string DefeatScene => defeatScene;
    
    private readonly string tutorialScene = "Tutorial";
    public string TutorialScene => tutorialScene;
    
    private readonly string bossScene = "BossLevel";
    public string BossScene => bossScene;

    private int currentLevelIndex;
    public int CurrenLevelIndex => currentLevelIndex;

    private bool tutorialRun;
    public bool IsTutorialRun => tutorialRun;

    public string NewGameplay()
    {
        if (tutorialRun)
        {
            tutorialRun = false;
        }
        
        currentLevelIndex = 1;
        var level = $"Level_{currentLevelIndex}";
        return level;
    }

    public string LoadTutorial()
    {
        tutorialRun = true;
        var level = tutorialScene;
        return level;
    }

    public string LoadCurrentLevel()
    {
        if (tutorialRun)
        {
            tutorialRun = false;
        }
        
        var level = $"Level_{currentLevelIndex}";
        return level;
    }

    public string LoadNextLevel()
    {
        if (tutorialRun)
        {
            tutorialRun = false;
        }
        
        if (currentLevelIndex == 3)
        {
            var level = bossScene;
            return level;
        }
        else
        {
            currentLevelIndex++;
            var level = $"Level_{currentLevelIndex}";
            return level;
        }
    }
}
