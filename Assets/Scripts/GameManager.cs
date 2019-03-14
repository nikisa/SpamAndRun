using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public Text letterToSpamText;
    private char _letterToSpam;

    
    public Text pointsTextEnd;
    public Text highscoreText;
    public Text highscoreTextEnd;


    bool _hasLevelStarted = false;
    bool _isGamePlaying = false;
    bool _isGameOver = false;
    bool _hasLevelFinished = false;

    public bool HasLevelStarted { get { return _hasLevelStarted; } set { _hasLevelStarted = value; } }
    public bool IsGamePlaying { get { return _isGamePlaying; } set { _isGamePlaying = value; } }
    public bool IsGameOver { get { return _isGameOver; } set { _isGameOver = value; } }
    public bool HasLevelFinished { get { return _hasLevelFinished; } set { _hasLevelFinished = value; } }

    public float delay = 1f;
    public float letterSpawnRate = 2f;

    public UnityEvent startLevelEvent;
    public UnityEvent playLevelEvent;
    public UnityEvent endLevelEvent;

    Timer _timer;
    PlayerInput _playerInput;

    private void Awake() {
        int highscore = GetHighscore();
        Debug.Log(highscore);
        _playerInput = Object.FindObjectOfType<PlayerInput>().GetComponent<PlayerInput>();
        _timer = Object.FindObjectOfType<Timer>().GetComponent<Timer>();
    }

    void Start() {
        StartCoroutine("RunGameLoop");
    }
    
    IEnumerator RunGameLoop() {
        yield return StartCoroutine("StartLevelRoutine");
        yield return StartCoroutine("PlayLevelRoutine");
        yield return StartCoroutine("EndLevelRoutine");
    }

    IEnumerator StartLevelRoutine() {

        _timer.started = false;
        _isGamePlaying = false;

        highscoreText.text = GetHighscore().ToString();

        
        while (!_hasLevelStarted) {
            yield return null;
        }

        if (startLevelEvent != null) {
            startLevelEvent.Invoke();
        }
    }

    IEnumerator PlayLevelRoutine() {
        
        _timer.started = true;
        _isGamePlaying = true;


        InvokeRepeating("setLetter", 0.1f, letterSpawnRate);


        yield return new WaitForSeconds(delay);

        if (playLevelEvent != null) {
            playLevelEvent.Invoke();
        }

        while (!_isGameOver) {
            _isGameOver = isLoser();
            pointsTextEnd.text = _playerInput.points.ToString();
            highscoreTextEnd.text = GetHighscore().ToString();
            yield return null;
        }
        
        yield return null;
    }

    IEnumerator EndLevelRoutine() {

        _timer.started = false;
        _isGamePlaying = false;

        if (endLevelEvent != null) {
            endLevelEvent.Invoke();
        }

        while (!_hasLevelFinished) {
            yield return null;
        }
        setHighscore(_playerInput.points);
        Restart();
    }

    public void Restart() {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void PlayLevel() {
        _hasLevelStarted = true;
    }
    
    bool isLoser() {
        bool ris = false;
        if (_playerInput.isDead || _timer.t <= 0) {
            ris = true;
        }
        return ris;
    }

    public static int GetHighscore() {
        return PlayerPrefs.GetInt("highscore", 0);
    }

    public static bool setHighscore(int points) {
        int highscore = GetHighscore();
        if (points > highscore) {
            PlayerPrefs.SetInt("highscore", points);
            PlayerPrefs.Save();
            return true;
        }
        else {
            return false;
        }
    }
    
    public void setLetter() {
        int rnd = Random.Range(65,90);
        _letterToSpam = (char)rnd;
        Debug.Log(getLetter());
        letterToSpamText.text = _letterToSpam.ToString();
    }

    public string getLetter() {
        return _letterToSpam.ToString();
    }
}
