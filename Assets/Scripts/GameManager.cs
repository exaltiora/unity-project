using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField]
    private TMP_Text _scoreText, _endScoreText, _highScoreText;

    private int score;

    [SerializeField]
    private Animator _scoreAnimator;

    [SerializeField]
    private AnimationClip _scoreClip;

    [SerializeField]
    private GameObject _scorePrefab;

    [SerializeField]
    private Vector3 _startPos;

    [SerializeField]
    private GameObject _endPanel;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        score = 0;

        _scoreText.text = score.ToString();
        _scoreAnimator.Play(_scoreClip.name, -1, 0f);
        
        UpdateScorePrefab();
    }

    private void UpdateScorePrefab()
    {
        float currentRotation = _scorePrefab.transform.rotation.eulerAngles.z;
        
        currentRotation = Mathf.Abs(currentRotation) < 0.01f ? 180f : 0f;
        Vector3 newRotation = new Vector3(0, 0, currentRotation);
        _scorePrefab.transform.rotation = Quaternion.Euler(newRotation);
    }

    public void ReloadGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    
    public void EndGame()
    {
        _endPanel.SetActive(true);
        _endScoreText.text = score.ToString();

        int highScore = PlayerPrefs.HasKey(Constants.DATA.HIGH_SCORE) ? PlayerPrefs.GetInt(Constants.DATA.HIGH_SCORE) : 0;
        
        if(score > highScore)
        {
            _highScoreText.text = "New High Score";
            highScore = score;
            PlayerPrefs.SetInt(Constants.DATA.HIGH_SCORE, highScore);
        }
        else
        {
            _highScoreText.text = "High Score: " + highScore.ToString();
        }
    }

    public void UpdateScore()
    {
        score++;

        _scoreText.text = score.ToString();
        _scoreAnimator.Play(_scoreClip.name, -1, 0f);

        UpdateScorePrefab();
    }
}
