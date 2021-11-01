using UnityEngine;

internal enum GamePhase
{
    WaitingPhase,
    ShootingPhase,
    RestartPhase
}

public class GameManager : MonoBehaviour
{
    public Material BallMat;
    public AudioSource audioSource;
    public AudioClip startClip;
    public AudioClip hitClip;
    public Player player1;
    public Player player2;
    public float lengthOfWaitingPhase = 3.3f;
    public float lengthOfRestartPhase = 5;
    public KeyCode p1Key;
    public KeyCode p2Key;

    private GamePhase gamePhase;
    private float timeStarted;

    private void Start()
    {
        InitWaitingPhase();
    }

    private void Update()
    {
        switch (gamePhase)
        {
            case GamePhase.WaitingPhase:
                WaitingPhase();
                break;

            case GamePhase.ShootingPhase:
                ShootingPhase();
                break;

            case GamePhase.RestartPhase:
                RestartPhase();
                break;

            default:
                break;
        }
    }

    private void WaitingPhase()
    {
        if (Time.time > timeStarted + lengthOfWaitingPhase)
        {
            InitShootingPhase();
        }
        else
        {
            Shoot();
        }
    }

    private void ShootingPhase()
    {
        Shoot();
    }

    private void Shoot()
    {
        bool p1ShotEarly = Input.GetKeyDown(p1Key) && gamePhase == GamePhase.WaitingPhase;
        bool p2ShotEarly = Input.GetKeyDown(p2Key) && gamePhase == GamePhase.WaitingPhase;
        bool p1ShotOnTime = Input.GetKeyDown(p1Key) && gamePhase == GamePhase.ShootingPhase;
        bool p2ShotOnTime = Input.GetKeyDown(p2Key) && gamePhase == GamePhase.ShootingPhase;

        if (p1ShotEarly || p2ShotOnTime)
        {
            Lose(player1.pid);
        }
        else if (p2ShotEarly || p1ShotOnTime)
        {
            Lose(player2.pid);
        }
    }

    private void Lose(int pid)
    {
        if (pid == player1.pid)
        {
            player1.Lose();
        }
        else
        {
            player2.Lose();
        }
        InitRestartPhase();
    }

    private void RestartPhase()
    {
        if (Time.time > timeStarted + lengthOfRestartPhase)
        {
            player1.ResetPlayer();
            player2.ResetPlayer();
            InitWaitingPhase();
        }
    }

    private void InitWaitingPhase()
    {
        if (audioSource)
        {
            audioSource.Stop();
            audioSource.volume = 0.1f;
            audioSource.PlayOneShot(startClip);
        }
        timeStarted = Time.time;
        BallMat.color = Color.red;
        gamePhase = GamePhase.WaitingPhase;
    }

    private void InitShootingPhase()
    {
        BallMat.color = Color.green;
        gamePhase = GamePhase.ShootingPhase;
    }

    private void InitRestartPhase()
    {
        if (audioSource)
        {
            audioSource.Stop();
            audioSource.volume = 1;
            audioSource.PlayOneShot(hitClip);
        }
        timeStarted = Time.time;
        gamePhase = GamePhase.RestartPhase;
    }
}