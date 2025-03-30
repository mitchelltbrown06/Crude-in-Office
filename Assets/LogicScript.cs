using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LogicScript : MonoBehaviour
{
    public float gasPrice = 3.75f;
    public TextMeshProUGUI ScoreBoard;
    public TextMeshProUGUI CoinBoard;
    public float OilRigRate;
    public float GasPriceIncrease;
    public int Coins;
    public string Equiped;
    public int OilRigPrice;
    public int MintPrice;
    public int WallPrice;

    // Start is called before the first frame update
    void Start()
    {
        ScoreBoard.text = ("Gas Price: $" + gasPrice.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        ScoreBoard.text = ("Gas Price: $" + gasPrice.ToString());
        CoinBoard.text = ("Coins: " + Coins.ToString());
    }
    public void DigWithOilRig()
    {
        gasPrice -= OilRigRate;
    }
    void FixedUpdate()
    {
        gasPrice += GasPriceIncrease;
    }
    public void CoinPickup()
    {
        Coins += 1;
    }
    public void EquipMint()
    {
        Equiped = "Mint";
    }
    public void EquipOilRig()
    {
        Equiped = "OilRig";
    }
    public void EquipWall()
    {
        Equiped = "Wall";
    }
    public void PurchaseOilRig()
    {
        Coins -= OilRigPrice;
    }
    public void PurchaseMint()
    {
        Coins -= MintPrice;
    }
    public void PurchaseWall()
    {
        Coins -= WallPrice;
    }
}
