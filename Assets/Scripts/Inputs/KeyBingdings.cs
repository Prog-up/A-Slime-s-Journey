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
    
}
