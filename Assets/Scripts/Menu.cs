using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour {

	public GameObject input;
	public GameObject finalMenu;
	public TextMeshProUGUI txtCurrentScore;
	public TextMeshProUGUI txtFinalScore;
	public TextMeshProUGUI txtHighScore;
	public TextMeshProUGUI txtTempo;
	public TextMeshProUGUI txtTempoHighScore;
	public TMP_InputField txtHighScoreAcronym;
	public Toggle toggleGyro;
	public int currentScore;
	public int highScore;
	public float hourCount;
	public float minuteCount;
	public float secondsCount;
	public bool isGameOver;
	public bool isGyroActivated;
	

	private void OnEnable() {
		EventManager.OnAsteroidDestroyed += AsteroidDestroyed;
		EventManager.OnGameOver += GameOver;
	}
	
	private void OnDisable() {
		EventManager.OnAsteroidDestroyed -= AsteroidDestroyed;
		EventManager.OnGameOver -= GameOver;
	}

	private void Awake() {
		isGyroActivated = PlayerPrefs.GetInt("Gyro") == 1 ? true : false;

		txtCurrentScore = GameObject.Find("TxtCurrentScore").GetComponent<TextMeshProUGUI>();
		txtFinalScore = GameObject.Find("TxtFinalScore").GetComponent<TextMeshProUGUI>();
		txtHighScore = GameObject.Find("TxtHighScore").GetComponent<TextMeshProUGUI>();
		txtTempo = GameObject.Find("TxtTempo").GetComponent<TextMeshProUGUI>();
		txtTempoHighScore = GameObject.Find("TxtTempoHighScore").GetComponent<TextMeshProUGUI>();
		txtHighScoreAcronym = GameObject.Find("InputHighScoreAcronym").GetComponent<TMP_InputField>();
		toggleGyro = GameObject.Find("ToggleGyro").GetComponent<Toggle>();
		finalMenu = transform.Find("FinalMenu").gameObject;
		input = transform.Find("Input").gameObject;
		txtCurrentScore.text = "" + 0;
		toggleGyro.isOn = isGyroActivated;
		
		finalMenu.SetActive(false);
	}

	private void Update() {
		if(!isGameOver)
		{
			secondsCount += Time.deltaTime;
			
			if(hourCount.ToString().Length == 1)
				txtTempo.text = "0" + hourCount;
			else
				txtTempo.text = "" + hourCount;
			txtTempo.text += ":";
			
			if(minuteCount.ToString().Length == 1)
				txtTempo.text += "0" + minuteCount;
			else
				txtTempo.text += "" + minuteCount;
			txtTempo.text += ":";
			
			if(((int)secondsCount).ToString().Length == 1)
				txtTempo.text += "0" + ((int)secondsCount);
			else
				txtTempo.text += "" + (int)secondsCount;
						
			if(secondsCount >= 60)
			{
				minuteCount++;
				secondsCount = 0;
			}
			else if(minuteCount >= 60)
			{
				hourCount++;
				minuteCount = 0;
			}
		}
	}

	public void Reset()
	{
		if(txtHighScoreAcronym.text == "")
			txtHighScoreAcronym.text = "???";
		
		PlayerPrefs.SetString("HighScorePlayer", txtHighScoreAcronym.text);
		PlayerPrefs.SetString("TempoHighScorePlayer", txtTempoHighScore.text);
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	public void AsteroidDestroyed()
	{
		currentScore += 10;
		txtCurrentScore.text = "" + currentScore;
	}

	public void ToggleGyro()
	{
		PlayerPrefs.SetInt("Gyro", toggleGyro.isOn ? 1 : 0);
	}

	public void GameOver()
	{
		isGameOver = true;
		input.SetActive(false);
		finalMenu.SetActive(true);

		if(!PlayerPrefs.HasKey("HighScore"))
		{
			PlayerPrefs.SetInt("HighScore", 0);
			PlayerPrefs.SetString("TempoHighScorePlayer", "00:00:00");
		}
		
		txtFinalScore.text = "" + currentScore;	
		highScore = PlayerPrefs.GetInt("HighScore");

		if(currentScore > highScore)
		{
			txtHighScore.text = "" + currentScore;
			PlayerPrefs.SetInt("HighScore", currentScore);
			txtTempoHighScore.text = "" + txtTempo.text;
			PlayerPrefs.SetString("TempoHighScorePlayer", txtTempoHighScore.text);
		}
		else
		{
			txtHighScore.text = "" + highScore;
			txtHighScoreAcronym.interactable = false;
			txtHighScoreAcronym.text = "" + PlayerPrefs.GetString("HighScorePlayer");
			txtTempoHighScore.text = "" + PlayerPrefs.GetString("TempoHighScorePlayer");
		}
	}
}
