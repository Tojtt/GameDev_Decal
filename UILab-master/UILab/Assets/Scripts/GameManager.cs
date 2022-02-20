using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour {

	public static GameManager gameManager;
	[SerializeField]private float money;

    public GameObject moneyObject;
	private TextMeshProUGUI moneyText;

	// Use this for initialization
	void Start () {
		gameManager = this;
        moneyText = moneyObject.GetComponent<TextMeshProUGUI>();
		UpdateUI ();
	}
	
	// Update is called once per frame
	void UpdateUI () {
		moneyText.text = "$" + money.ToString ("N2");
	}

	public void AddMoney(float amount){
		money += amount;
		UpdateUI ();
	}

	public void ReduceMoney(float amount) {
		money -= amount;
		UpdateUI ();
	}

	public bool HasEnough(float amount) {
		if (amount <= money) {
			return true;
		}
		return false;
	}
}
