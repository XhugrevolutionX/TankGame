using System.Timers;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    
    [SerializeField] private bool isFinished;

    private GameObject _turrets;
    private Canvas _endScreenCanvas;
    private Canvas _uiCanvas;
    private Text _remainingTurretsText;
    private GameObject _tank;
    private int _turretsTotalCount;
    private Text _endInfoText;
    private Text _timerText;
    private float _timer;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _endScreenCanvas = transform.GetChild(3).GetComponent<Canvas>();
        _uiCanvas = transform.GetChild(4).GetComponent<Canvas>();
        _turrets = transform.GetChild(2).gameObject;
        _tank = transform.GetChild(1).gameObject;
        _remainingTurretsText = _uiCanvas.transform.GetChild(0).GetComponent<Text>();
        _timerText = _uiCanvas.transform.GetChild(1).GetComponent<Text>();
        _endInfoText = _endScreenCanvas.transform.GetChild(1).GetComponent<Text>();
        _turretsTotalCount = _turrets.transform.childCount;
        _timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        _timer += Time.deltaTime;
        _remainingTurretsText.text = "Remaining Turrets : " + _turrets.transform.childCount + "/" + _turretsTotalCount;
        _timerText.text = "Timer : " + _timer.ToString("0.00");
        if (_turrets.transform.childCount == 0)
        {
            isFinished = true;
            _endInfoText.text = "You killed all the turrets in " + _timer.ToString("0.00") + " seconds!";
        }

        if (!_tank)
        {
            isFinished = true;
            _endInfoText.text = "You killed " + (_turretsTotalCount - _turrets.transform.childCount) +" turrets in " + _timer.ToString("0.00") + " seconds!";
        }
        
        if (isFinished)
        {
            _endScreenCanvas.enabled = true;
            _uiCanvas.enabled = false;
            Time.timeScale = 0;
        }
        else
        {
            _endScreenCanvas.enabled = false;
            _uiCanvas.enabled = true;
        }
    }
}