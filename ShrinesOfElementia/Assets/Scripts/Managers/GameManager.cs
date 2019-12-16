//Author: Sofia Kauko
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    

    //References to relevant stuff
    public AchievementManager Achievements;
    [SerializeField] private int MaxAllowedDeaths;
    private bool saveDataExists = false;
    private SaveData saveData;
    private bool gameHasBeenSetUp = false;
    private bool nextChapterLoading = false;

    //variables
    public int Level { get; set; }
    public int PlayerLevel { get; set; }
    public int PlayerXP { get; set; }
    public int PlayerHP { get; set; }
    public int PlayerLivesRemaining { get; set; }
    public int PlayerDeaths { get; set; }
    public bool FireUnlocked { get; set; }
    public bool WaterUnlocked { get; set; }
    public bool WindUnlocked { get; set; }
    public bool EarthUnlocked { get; set; }
    public Vector3 NearestCheckpoint { get; set; }

    public static GameManager Instance { get; private set; }
    public bool SaveDataExists { get => saveDataExists; private set => saveDataExists = value; }

    private void Awake()
    {
        // Prevents multiple instances
        if (Instance == null) { Instance = this; }
        else if (Instance != this) { Destroy(gameObject); }
        else { Debug.Log("Warning: multiple " + this + " in scene!"); }
    }


    // Start is called before the first frame update
    private void Start()
    {
        //secure gamobject
        DontDestroyOnLoad(gameObject);

        //gameObject.AddComponent<AchievementManager>();
        //Achievements = GetComponent<AchievementManager>();

        //Achievements = AchievementManager.Current;


        Debug.Log("start function -  deaths: " + PlayerDeaths);

        //check if game has a save
        LoadVariablesFromSave();

        if (saveDataExists)
        {
            AchievementManager.Instance.InitializeFromSave(saveData);
            MenuManager.Instance.ActivateContinueButton();
        }

    }

    public void LoadNewGame()
    {
        //init  acievement manager default
        Achievements.InitializeToDefault(); // is not finished.
        DeleteSave(); // just to be sure
        //set data to default
        SetDataToDefault();

        //load scene
        SceneManager.LoadScene(1);
        SceneManager.sceneLoaded += SetUpGame;

    }

    public void LoadFromSave()
    {
        //Loaded already in start, just init the rest.
        LoadVariablesFromSave();
        SceneManager.LoadScene(1);
        SceneManager.sceneLoaded += SetUpGame;
    }

    public void LoadNextLevel()
    {
        //SceneManager.sceneLoaded += SetBaseSpawn;
        Level += 1;
        PlayerHP = 150;
        PlayerLivesRemaining = MaxAllowedDeaths;
        nextChapterLoading = true;
        Save();
        LoadFromSave();
    }

    public void LoadAchievements()
    {
        LoadVariablesFromSave();
        AchievementManager.Instance.InitializeFromSave(saveData);
    }

    //Called from both new game and continue with save. Prepares gameworld according to previosly loaded data.(if no save, default data.)
    private void SetUpGame(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == 1) // set up game world if game world was loaded.
        {

            if (!gameHasBeenSetUp)
            {
                //register listeners        
                EventManager.Instance.RegisterListener<ShrineEvent>(RegisterShrine);
                EventManager.Instance.RegisterListener<PlayerDeathEvent>(OnPlayerDeath);
                EventManager.Instance.RegisterListener<ExperienceEvent>(OnPlayerXPEvent);
                EventManager.Instance.RegisterListener<BossDeathEvent>(OnBossDeath);
                PlayerHP = Player.Instance.Health.MaxHealth;
            }

            Debug.Log("Shrines aquired: " + FireUnlocked + " " + EarthUnlocked + " " + WaterUnlocked + " " + WindUnlocked);
            //load player abilities
            TimerManager.Instance.SetNewTimer(gameObject, 2f, LoadAbilities); // Event seems to not be heard if sent too early...

            //prepare player position and HP
            if (!saveDataExists || nextChapterLoading)
            {
                SetBaseSpawn();
                nextChapterLoading = false; // only true just efter player defeats a boss and clicks nect chapter.
            }
            Player.Instance.transform.position = NearestCheckpoint;
            Player.Instance.Health.CurrentHealth = PlayerHP;


            gameHasBeenSetUp = true;
            
        }
        
    }


    private void SetBaseSpawn()
    {
        NearestCheckpoint = PlayerSpawn.Instance.transform.position;
    }

    private void LoadAbilities()
    {
        if (FireUnlocked)
        {
            EventManager.Instance.FireEvent(new ShrineEvent("Fire enabled from gameManager", "Fire"));
        }
        if (WaterUnlocked)
        {
            EventManager.Instance.FireEvent(new ShrineEvent("Water enabled from gameManager", "Water"));
        }
        if (WindUnlocked)
        {
            EventManager.Instance.FireEvent(new ShrineEvent("Wind enabled from gameManager", "Wind"));
        }
        if (EarthUnlocked)
        {
            EventManager.Instance.FireEvent(new ShrineEvent("Earth enabled from gameManager", "Earth"));
        }
    }

    private void RegisterShrine(ShrineEvent ev)
    {
        switch (ev.Element)
        {
            case "Fire":
                FireUnlocked = true;
                break;
            case "Earth":
                EarthUnlocked = true;
                break;
            case "Wind":
                WindUnlocked = true;
                break;
            case "Water":
                WaterUnlocked = true;
                break;
            default:
                Debug.Log("Element not valid");
                break;
        }
        Save();
    }


    //Should be called from checkpoint manager each time a new checkpoint is reached. Update nearest and save game.
    public void SaveLatestCheckpoint(Vector3 checkpoint)
    {
        NearestCheckpoint = checkpoint;
        Save();
    }


    //Called from healtrh component each time a boss dies. Level increases and game saves. Add UI to continue or small cutscene or similar.
    public void OnBossDeath(BossDeathEvent ev)
    {
        Debug.Log("Boss died, level int has been increased");
        //go to menu? play cutscene? show UI with "next Chapter"? 
        Save();
        SceneManager.LoadScene(3);
    }

    private void RespawnPlayer()
    {
        
        NearestCheckpoint = CheckpointManager.Instance.FindNearestSpawnPoint();
        Player.Instance.transform.position = NearestCheckpoint;
        Player.Instance.Health.CurrentHealth = Player.Instance.Health.MaxHealth;
    }

    public void OnPlayerDeath(PlayerDeathEvent ev)
    {
        PlayerDeaths += 1;
        PlayerLivesRemaining -= 1;
        Debug.Log("OnPlayer death called -  deaths: " + PlayerDeaths);
        if (PlayerLivesRemaining < 1)
        {
            DeleteSave();
            SceneManager.LoadScene(2);
            return;
        }
        RespawnPlayer();
    }

    public void OnPlayerLevelUp()
    {
        PlayerLevel += 1;
        PlayerXP = 0;

    }
    public void OnPlayerXPEvent(ExperienceEvent ev)
    {
        PlayerXP += (int)ev.Experience;
    }

    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");
        SaveData data = new SaveData();

        //get HP from Player
        PlayerHP = Player.Instance.Health.CurrentHealth;

        //set save variables
        data.ContainsSaveData = true;
        data.Level = Level;
        data.PlayerLevel = PlayerLevel;
        data.PlayerXP = PlayerXP;
        data.PlayerHP = PlayerHP;
        data.PlayerLivesRemaining = PlayerLivesRemaining;
        data.PlayerDeaths = PlayerDeaths;
        data.FireUnlocked = FireUnlocked;
        data.WaterUnlocked = WaterUnlocked;
        data.WindUnlocked = WindUnlocked;
        data.EarthUnlocked = EarthUnlocked;
        data.SpawnX = NearestCheckpoint.x;
        data.SpawnY = NearestCheckpoint.y;
        data.SpawnZ = NearestCheckpoint.z;

        //save achievement data
        data.SpeedRunner = Achievements.SpeedRunner;
        data.Elementalist = Achievements.Elementalist;
        data.FlightExpert = Achievements.FlightExpert;
        data.KillcountHundred = Achievements.KillcountHundred;
        data.currentKills = Achievements.currentKills;
        bool fireKilled;
        Achievements.SlayedGiants.TryGetValue("Fire", out fireKilled);
        data.SlayedFireGiant = fireKilled;
        bool waterKilled;
        Achievements.SlayedGiants.TryGetValue("Fire", out waterKilled);
        data.SlayedWaterGiant = waterKilled;
        bool windKilled;
        Achievements.SlayedGiants.TryGetValue("Fire", out windKilled);
        data.SlayedWindGiant = windKilled;
        bool earthKilled;
        Achievements.SlayedGiants.TryGetValue("Fire", out earthKilled);
        data.SlayedEarthGiant = earthKilled;
        data.GiantBane = Achievements.GiantBane;
        
        saveDataExists = true;

        Debug.Log("SAVING: shrine:" + data.FireUnlocked + " " + data.EarthUnlocked + " " + data.WaterUnlocked + " " + data.WindUnlocked);
        //stream to file
        bf.Serialize(file, data);
        file.Close();
    }

    private void LoadVariablesFromSave()
    {
        if (File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
        {
            Debug.Log("SAVEFILE EXISTS");
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
            saveData = (SaveData)bf.Deserialize(file);
            file.Close();

            //makes sure gameManager knows if data is a save or just default null + 0
            saveDataExists = saveData.ContainsSaveData;

            //get Variables from save:
            Level = Level;
            PlayerLevel = saveData.PlayerLevel;
            PlayerXP = saveData.PlayerXP;
            PlayerHP = saveData.PlayerHP;
            PlayerLivesRemaining = saveData.PlayerLivesRemaining;
            PlayerDeaths = saveData.PlayerDeaths;
            FireUnlocked = saveData.FireUnlocked;
            WaterUnlocked = saveData.WaterUnlocked;
            WindUnlocked = saveData.WindUnlocked;
            EarthUnlocked = saveData.EarthUnlocked;
            NearestCheckpoint = new Vector3(saveData.SpawnX, saveData.SpawnY, saveData.SpawnZ);

            //init achievement data
            AchievementManager.Instance.InitializeFromSave(saveData);
        }
        else
        {
            saveDataExists = false;
            Debug.Log("SAVEFILE DOES NOT EXIST");
        }
    }

    private void DeleteSave()
    {
        Debug.Log("overriding (deleting) savedata");
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");
        //create empty savedata to override with. Ik, dumb solution but its safe.
        SaveData data = new SaveData();
        saveDataExists = false;
        //stream to file
        bf.Serialize(file, data);
        file.Close();
    }

    private void SetDataToDefault()
    {
        Level = 0;
        PlayerLevel = 1;
        PlayerXP = 0;
        PlayerHP = 150; // generalize
        PlayerLivesRemaining = 3;
        PlayerDeaths = 0;
        FireUnlocked = false;
        WaterUnlocked = false;
        WindUnlocked = false;
        EarthUnlocked = false;
    }



}
