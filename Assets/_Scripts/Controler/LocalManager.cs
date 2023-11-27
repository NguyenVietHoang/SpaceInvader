using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalManager : MonoBehaviour
{
    [Header ("Pool")]
    [SerializeField]
    private ObjectPooling BulletPool;
    [SerializeField]
    private ObjectPooling InvaderPool;

    [Header("Path")]
    [SerializeField]
    private PathModel InvaderPath_01;
    [SerializeField]
    private PathModel InvaderPath_02;
    [SerializeField]
    private List<StageModel> Stages;

    [Header("Controler")]
    [SerializeField]
    private PlayerControler player;
    [SerializeField]
    private InputModel inputKey;
    [SerializeField]
    private ScoreControler scoreControler;
    [SerializeField]
    private float bulletDelay = 0.5f;

    [Header ("Views")]
    [SerializeField]
    private MainMenuView mainMenu;
    [SerializeField]
    private GameView gameView;
    [SerializeField]
    private GameOverView gameOverView;

    // Start is called before the first frame update
    void Start()
    {
        BulletPool.Initialize();
        InvaderPool.Initialize();

        InvaderPath_01.Initialize();
        InvaderPath_02.Initialize();

        player.Initialize();
        player.OnInvaderCollide = null;
        player.OnInvaderCollide += InvaderCollide;        

        currentBulletDelay = 0;
        SetView(VIEW_TYPE.MAIN_MENU);
    }

    bool enableInput = false;
    // Update is called once per frame
    void Update()
    {
        if(enableInput)
        {
            if (Input.GetKey(inputKey.Right))
            {
                player.Move(false);
            }
            else if (Input.GetKey(inputKey.Left))
            {
                player.Move(true);
            }

            if (Input.GetKey(inputKey.Shoot))
            {
                if (currentBulletDelay <= 0)
                {
                    currentBulletDelay = bulletDelay;
                    ShootBullet();
                }
            }

            if (currentBulletDelay > 0)
                currentBulletDelay -= Time.deltaTime;
        }       
    }

    #region Bullet Control
    float currentBulletDelay = 0;
    /// <summary>
    /// Spawn the Bullet.
    /// </summary>
    private void ShootBullet()
    {
        PoolingElt tmp = BulletPool.GetPooledObject();
        if(tmp != null)
        {
            BulletControler bulletControler = tmp.GetComponent<BulletControler>();
            if(bulletControler != null)
            {
                bulletControler.Initialize(player.GetBulletSpawnPos());

                bulletControler.OnMapBoundCollide = OnBulletHitBound;
                bulletControler.OnInvaderCollide = OnBulletHitInvader;
            }
        }
    }

    private void OnBulletHitBound(BulletControler bulletControler)
    {
        bulletControler.TriggerDeathProcess();
    }

    private void OnBulletHitInvader(BulletControler bulletControler, InvaderControler invaderControler)
    {
        bulletControler.TriggerDeathProcess();
        invaderControler.SetDeath();

        SetScore(score + invaderControler.GetScore());

        invaderKilled++;
        if (invaderKilled >= currentStage.maxInvaderToSpawn)
        {
            NextStage();
        }
    }
    #endregion

    #region Stage Control
    StageModel currentStage;
    int score;
    int invaderKilled;

    /// <summary>
    /// Update Score to the View
    /// </summary>
    private void SetScore(int _score)
    {
        score = _score;
        gameView.SetScore(_score);
    }

    private IEnumerator spawnInvaderCoroutine;

    public void NewGame()
    {
        SetScore(0);
        SetView(VIEW_TYPE.GAME);
        NextStage();
    }

    /// <summary>
    /// Spawn a random stage of Invader.
    /// </summary>
    private void NextStage()
    {
        currentStage = Stages[Random.Range(0, Stages.Count)];        
        invaderKilled = 0;

        //Reset the Stage
        ClearInvader();

        enableInput = true;

        spawnInvaderCoroutine = SpawnInvader(currentStage);
        StartCoroutine(spawnInvaderCoroutine);
    }

    private void GameOver()
    {
        Debug.Log("Game Over");

        enableInput = false;
        scoreControler.SetScore(score);
        SetView(VIEW_TYPE.GAME_OVER);

        gameOverView.SetScore(score, scoreControler.GetHighScore());
        ClearInvader();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    #endregion

    #region Invader Control
    private List<InvaderControler> currentInvaderLst;

    /// <summary>
    /// Clear all Invader in cache.
    /// </summary>
    private void ClearInvader()
    {
        if(spawnInvaderCoroutine != null)
        {
            StopCoroutine(spawnInvaderCoroutine);
        }

        if (currentInvaderLst != null && currentInvaderLst.Count > 0)
        {
            for (int i = 0; i < currentInvaderLst.Count; i++)
            {
                currentInvaderLst[i].SetDeath();
            }
        }
        currentInvaderLst = new List<InvaderControler>();
    }

    /// <summary>
    /// Spawn the Invader following the stage data.
    /// </summary>
    /// <param name="stage"></param>
    /// <returns></returns>
    IEnumerator SpawnInvader(StageModel stage)
    {
        Debug.Log("[LocalManager][SpawnInvader] Start spawning...");
        for(int i = 0; i < stage.maxInvaderToSpawn; i++)
        {
            PoolingElt tmp = InvaderPool.GetPooledObject();
            if (tmp != null)
            {
                InvaderControler invaderControl = tmp.GetComponent<InvaderControler>();

                if (invaderControl != null)
                {
                    PathModel currentPath = InvaderPath_01;
                    switch (stage.path)
                    {
                        case PATH.DEFAULT:
                            currentPath = InvaderPath_01;
                            break;
                        case PATH.MEDIUM:
                            currentPath = InvaderPath_02;
                            break;
                    }
                    invaderControl.Initialize(currentPath, stage.invaderData);

                    //If any alien Escape, the game is over.
                    invaderControl.OnInvaderEscape = null;
                    invaderControl.OnInvaderEscape += InvaderEscape;
                }

                currentInvaderLst.Add(invaderControl);
            }

            yield return new WaitForSeconds(stage.spawnDelay);
        }
    }

    private void InvaderEscape()
    {
        Debug.Log("GAME OVER.");
        GameOver();
    }

    private void InvaderCollide()
    {
        //Debug.Log("Invader Collide...");
        GameOver();
    }
    #endregion

    #region View Control
    private void SetView(VIEW_TYPE type)
    {
        mainMenu.SetActive(type == VIEW_TYPE.MAIN_MENU);
        gameView.SetActive(type == VIEW_TYPE.GAME);
        gameOverView.SetActive(type == VIEW_TYPE.GAME_OVER);
    }
    #endregion
}

public enum VIEW_TYPE
{
    MAIN_MENU,
    GAME,
    GAME_OVER
}
