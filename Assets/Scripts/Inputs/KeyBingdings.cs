using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "KeyBindings", menuName = "Keybingings")]
public class KeyBingdings : ScriptableObject
{
    public KeyCode Jump;
    public KeyCode MoveLeft;
    public KeyCode MoveRight;
    public KeyCode Transfo;
    public KeyCode Climb;
    public KeyCode Shoot;
    public KeyCode Pause;

    public KeyCode CheckKey(string key)
    {
        switch(key)
        {
            case "Jump":
                return Jump;
            case "MoveLeft":
                return MoveLeft;
                case "MoveRight":
                return MoveRight;
            case "Transfo":
                return Transfo;
            case "Climb":
                return Climb;
            case "Shoot":
                return Shoot;
            case "Pause":
                return Pause;

            default:
                return KeyCode.None;
            
        }
    }
    
}
