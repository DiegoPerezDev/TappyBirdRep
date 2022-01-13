using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// <para> This class manages all of the inputs of the player, reads them and take action for it by calling other codes methods. </para>
/// <para> This input manager works with Unitys input system, the one that uses 'input actions'. </para>
/// <para> This code separates the inputs between menu inputs and player inputs so we can disable them separately. </para>
/// </summary>
public sealed class InputsManager
{
    public  static InputsActionsControl input;
    private static Player player;

#if UNITY_ANDROID
    private static Vector2 fingerPos = new Vector2();
    private static List<RaycastResult> results = new List<RaycastResult>();
    private static EventSystem eventSystem = EventSystem.current;
#endif


    public static void SetInputManager()
    {
        input = new InputsActionsControl();
        input.MenuInputs.Enable();
        SetInputMask();
        SetInputsCallBacks();
        GameManager.OnLevelStarted += LevelStart;
        GameManager.OnLoseGame += LoseLevel;
    }

#region Delegate functions

    private static void LevelStart()
    {
        if (input != null)
            input.PlayingInputs.Enable();

        // Get player
        var players = GameObject.FindGameObjectsWithTag("Player");
        for(int i = 0; i < players.Length; i++)
        {
            Player playerCode = players[i].GetComponent<Player>();
            if(playerCode != null)
            { 
                player = playerCode;
                break;
            }
            if( (i == players.Length - 1) && (player == null))
            {
                Debug.Log("no player was found!");
                Debug.Break();
            }
        }
    }
    private static void LoseLevel() => input.PlayingInputs.Disable();

#endregion

#region Input system settings

    private static void SetInputMask()
    {
#if UNITY_STANDALONE
            input.bindingMask = InputBinding.MaskByGroup("pc");
#endif

#if UNITY_ANDROID
            input.bindingMask = InputBinding.MaskByGroup("smartphone");
#endif
    }

    private static void SetInputsCallBacks()
    {
#if UNITY_STANDALONE
            input.PlayingInputs.pause.performed += ctx => GameManager.Pause(true);
            input.MenuInputs.back.performed     += ctx => UI_MenusManagement.CloseMenu();
            input.PlayingInputs.jump.performed += ctx => player.Jump();
#endif
        input.PlayingInputs.tap.performed += ctx => CheckIfTapOnButton();
    }

#endregion

#region  Input functions

    private static void CheckIfTapOnButton()
    {
#if UNITY_STANDALONE
        if(!EventSystem.current.IsPointerOverGameObject())
            player.Jump();
#endif
#if UNITY_ANDROID
        fingerPos = input.PlayingInputs.fingerPos.ReadValue<Vector2>();
        if (!IsPointerOverUIObject())
            player.Jump();
#endif
    }

#if UNITY_ANDROID
    private static bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(eventSystem);
        eventDataCurrentPosition.position = new Vector2(fingerPos.x, fingerPos.y);
        results.Clear();
        eventSystem.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
#endif

#endregion

}