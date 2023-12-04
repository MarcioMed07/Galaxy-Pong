using System.Collections;
using System.Linq.Expressions;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;



public class GameController : MonoBehaviour
{

    public float gameStartDelay = 1.5f;
    public int maxScore = 0;
    public Paddle otherPaddle;
    public Ball gameBall;
    public TextMeshProUGUI player1ScoreText;
    public TextMeshProUGUI player2ScoreText;
    public GameObject pausePanel;
    public GameObject pauseButton;
    private int _player1Score = 0;
    private int _player2Score = 0;
    private bool _gamePaused = false;
    private AudioSource _music;

    void Awake()
    {
        Application.targetFrameRate = 300;
        _music = GetComponent<AudioSource>();
        ChangePause(false);
        if(GameInfo.gameMode == GameMode.Player){
            otherPaddle.tag = "Player2";
            GameObject.Find("Player2Score").transform.Rotate(new Vector3(0,0,180));
        }else{
            otherPaddle.tag = "AI";
            GameObject.Find("Player2Score").transform.Rotate(new Vector3(0,0,0));
        }
    }

    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            ChangePause(!_gamePaused);
        }
    }

    void Start()
    {
        gameBall.ResetBall(1);
    }

    public void QuitGame(){
        SceneManager.LoadSceneAsync(0);
    }

    public void ChangePause(bool state){
        _gamePaused = state;
        pausePanel.SetActive(_gamePaused);
        pauseButton.SetActive(!_gamePaused);
        Time.timeScale = _gamePaused ? 0 : 1;
        _music.volume *= _gamePaused ? 0.25f : 4;
    }

    public void SetVolume(float volume){
        AudioListener.volume = volume;
    }


    public void ScorePoint(int player)
    {
        gameBall.StopBall();
        StartCoroutine(ResetGame(gameStartDelay, player == 1 ? 1 : -1));
        switch (player)
        {
            case 1:
                _player1Score++;
                player1ScoreText.text = $"Player 1 score: {_player1Score}";
                if (_player1Score >= maxScore)
                {
                    FinishGame(player);
                }
                break;
            case 2:
                _player2Score++;
                player2ScoreText.text = $"Player 2 score: {_player2Score}";
                if (_player2Score >= maxScore)
                {
                    FinishGame(player);
                }
                break;
        }

    }

    private void FinishGame(int player)
    {

    }

    private IEnumerator ResetGame(float waitTime, float yDirection)
    {
        yield return new WaitForSeconds(waitTime);
        gameBall.ResetBall(yDirection);
    }
}
