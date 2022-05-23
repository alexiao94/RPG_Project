using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum GameState { FreeRoam, Battle}
public class GameController : MonoBehaviour
{   
    [SerializeField] PlayerController playerController;
    [SerializeField] BattleSystem battleSystem;
    [SerializeField] GameObject overWorld;
    GameState state;
    private void Start() {
        playerController.OnEncounter += StartBattle;
        battleSystem.OnBattleOver += EndBattle;
    }
    void StartBattle() {
        state = GameState.Battle;
        battleSystem.gameObject.SetActive(true);
        overWorld.SetActive(false);
        battleSystem.StartBattle();
    }
    private void Update() {
        if (state == GameState.FreeRoam){
            playerController.HandleUpdate();

        }
        else if (state == GameState.Battle){
            battleSystem.HandleUpdate();

        }

    }

    void EndBattle(bool won) {
        state = GameState.FreeRoam;
        battleSystem.gameObject.SetActive(false);
        overWorld.SetActive(true);
    }
}

