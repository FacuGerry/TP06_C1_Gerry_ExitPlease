using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSettings", menuName = "Data/PlayerSettings")]

public class PlayerDataSo : ScriptableObject
{

    [Header("Stats")]
    public float speed;
    public float jumpForce;
    public float dashForce;

    [Header("Key Bindings")]
    public KeyCode goLeft;
    public KeyCode goRight;
    public KeyCode goUp;
    public KeyCode attack;
    public KeyCode dash;
    public KeyCode pauseGame;
}
