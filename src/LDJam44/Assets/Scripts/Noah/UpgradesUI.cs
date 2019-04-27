﻿using System;
using UnityEngine;
using UnityEngine.UI;

public class UpgradesUI : MonoBehaviour
{
    public string SpaceStationName;
    public string PlayerName;

    private SpaceStation spaceStation;
    private Player player;
    private Action onAccept;
    private Action onReject;

    public Text Error;
    public GameObject TransitionUI;
    public GameObject MovementUI;
    public GameObject EconomicUI;
    public GameObject OffensiveUI;
    public GameObject DefensiveUI;
    public GameObject WeaponsUI;
    public GameObject SpecialsUI;
    public GameObject ConfirmUI;
    public Text ConfirmText;

    public Button ThrusterButton;
    public Text ThrusterCost;
    public Text CurrentThrusters;
    public Button StabilizerButton;
    public Text StabilizerCost;
    public Text CurrentStabilizers;
    public Button TradingButton;
    public Text TradingCost;
    public Text CurrentTrading;
    public Button LootingButton;
    public Text LootingCost;
    public Text CurrentLooting;
    public Button DroneButton;
    public Text DroneCost;
    public Text CurrentDrones;
    public Button AmpButton;
    public Text AmpCost;
    public Text CurrentAmp;
    public Button ShieldButton;
    public Text ShieldCost;
    public Text CurrentShields;
    public Button DrainButton;
    public Text DrainCost;
    public Text CurrentDrain;

    public void Start()
    {
        spaceStation = GameObject.Find(SpaceStationName).GetComponent<SpaceStation>();
        player = GameObject.Find(PlayerName).GetComponent<Player>();
        UpdateUpgrade(ThrusterButton, ThrusterCost, CurrentThrusters, player.Thrusters, spaceStation.ThrusterCosts);
        UpdateUpgrade(StabilizerButton, StabilizerCost, CurrentStabilizers, player.Stabilizers, spaceStation.StabilizerCosts);
        UpdateUpgrade(TradingButton, TradingCost, CurrentTrading, player.Trading, spaceStation.TradingCosts);
        UpdateUpgrade(LootingButton, LootingCost, CurrentLooting, player.Looting, spaceStation.LootingCosts);
        UpdateUpgrade(DroneButton, DroneCost, CurrentDrones, player.Drones, spaceStation.DroneCosts);
        UpdateUpgrade(AmpButton, AmpCost, CurrentAmp, player.Amp, spaceStation.AmpCosts);
        UpdateUpgrade(ShieldButton, ShieldCost, CurrentShields, player.Shields, spaceStation.ShieldCosts);
        UpdateUpgrade(DrainButton, DrainCost, CurrentDrain, player.Drain, spaceStation.DrainCosts);
    }

    private void UpdateUpgrade(Button button, Text cost, Text currentUpgradeLevel, int playerUpgradeLevel, int[] upgradeCosts)
    {
        button.interactable = playerUpgradeLevel != upgradeCosts.Length;
        cost.text = button.interactable ? "Cost: " + upgradeCosts[playerUpgradeLevel] : "";
        currentUpgradeLevel.text = playerUpgradeLevel + "/" + upgradeCosts.Length;
    }

    public void TransitionToMovement()
    {
        TransitionUI.SetActive(false);
        MovementUI.SetActive(true);
    }

    public void TransitionToEconomics()
    {
        TransitionUI.SetActive(false);
        EconomicUI.SetActive(true);
    }

    public void TransitionToOffesive()
    {
        TransitionUI.SetActive(false);
        OffensiveUI.SetActive(true);
    }

    public void TransitionToDefensive()
    {
        TransitionUI.SetActive(false);
        DefensiveUI.SetActive(true);
    }

    public void TransitionToWeapons()
    {
        TransitionUI.SetActive(false);
        WeaponsUI.SetActive(true);
    }

    public void TransitionToSpecial()
    {
        TransitionUI.SetActive(false);
        SpecialsUI.SetActive(true);
    }

    public void TransitionBack()
    {
        MovementUI.SetActive(false);
        EconomicUI.SetActive(false);
        OffensiveUI.SetActive(false);
        DefensiveUI.SetActive(false);
        WeaponsUI.SetActive(false);
        SpecialsUI.SetActive(false);
        TransitionUI.SetActive(true);
    }

    public void BuyThrusters()
    {
        BuyUpgrade(MovementUI, ThrusterButton, ThrusterCost, CurrentThrusters, player.Thrusters, spaceStation.ThrusterCosts, "Thrusters", () => player.Thrusters++);
    }

    public void BuyStabilizers()
    {
        BuyUpgrade(MovementUI, StabilizerButton, StabilizerCost, CurrentStabilizers, player.Stabilizers, spaceStation.StabilizerCosts, "Stabilizers", () => player.Stabilizers++);
    }

    public void BuyTrading()
    {
        BuyUpgrade(EconomicUI, TradingButton, TradingCost, CurrentTrading, player.Trading, spaceStation.TradingCosts, "Trading", () => player.Trading++);
    }

    public void BuyLooting()
    {
        BuyUpgrade(EconomicUI, LootingButton, LootingCost, CurrentLooting, player.Looting, spaceStation.LootingCosts, "Looting", () => player.Looting++);
    }

    public void BuyDrones()
    {
        BuyUpgrade(OffensiveUI, DroneButton, DroneCost, CurrentDrones, player.Drones, spaceStation.DroneCosts, "A Drone", () => player.Drones++);
    }

    public void BuyAmp()
    {
        BuyUpgrade(OffensiveUI, AmpButton, AmpCost, CurrentAmp, player.Amp, spaceStation.AmpCosts, "A Amp", () => player.Amp++);
    }

    public void BuyShields()
    {
        BuyUpgrade(DefensiveUI, ShieldButton, ShieldCost, CurrentShields, player.Shields, spaceStation.ShieldCosts, "Shields", () => player.Shields++);
    }

    public void BuyDrain()
    {
        BuyUpgrade(DefensiveUI, DrainButton, DrainCost, CurrentDrain, player.Drain, spaceStation.DroneCosts, "Drain", () => player.Drain++);
    }

    private void BuyUpgrade(GameObject upgradeUI, Button upgradeButton, Text upgradeCost, Text currentUpgradeLevel, int playerUpgradeLevel, int[] upgradeCosts, string upgrade, Action buyUpgrade)
    {
        var cost = upgradeCosts[playerUpgradeLevel];
        if (player.LifeForce <= cost)
        {
            Error.text = "Insufficient Life Force";
            return;
        }
        upgradeUI.SetActive(false);
        ConfirmText.text = $"Buy {upgrade} For {cost} Life Force";
        onAccept = () =>
        {
            ConfirmUI.SetActive(false);
            player.LifeForce -= cost;
            buyUpgrade();
            UpdateUpgrade(upgradeButton, upgradeCost, currentUpgradeLevel, playerUpgradeLevel + 1, upgradeCosts);
            upgradeUI.SetActive(true);
        };
        onReject = () =>
        {
            ConfirmUI.SetActive(false);
            upgradeUI.SetActive(true);
        };
        ConfirmUI.SetActive(true);
    }

    public void Accept()
    {
        onAccept();
    }

    public void Reject()
    {
        onReject();
    }
}