using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class CharacterLoader : MonoBehaviour
{
    public CharacterDataBase characterDatabase1;
    public CharacterDataBase characterDatabase2;
    
    private const float OffsetPlayer1 = 9.4f;
    private const float OffsetPlayer2 = 14f;
    
    private readonly ReadOnlyArray<Gamepad> _gamepads = Gamepad.all;

    void Awake()
    {
        LoadCharacter(1);
        LoadCharacter(2);
    }

    private void LoadCharacter(int num)
    {
        var mainCamera = Camera.main;
        if (mainCamera is null)
            return;
        var halfHeight = mainCamera.orthographicSize;
        var halfWidth = halfHeight * mainCamera.aspect;
        var index = PlayerPrefs.GetInt($"SelectedCharacter{num}");
        var position = Vector3.zero;
        position.y = mainCamera.transform.position.y - halfHeight - 0.5f;
        position.x = num == 1
            ? mainCamera.transform.position.x - halfWidth + OffsetPlayer1
            : mainCamera.transform.position.x + halfWidth - OffsetPlayer2;
        var character = num == 1 ? characterDatabase1.GetCharacter(index) : characterDatabase2.GetCharacter(index); 
        var player = Instantiate(character.characterPrefab, position, Quaternion.identity);
        var playerInput = player.transform.GetComponentInChildren<PlayerInput>();
        if (_gamepads.Count > 0)
            playerInput.SwitchCurrentControlScheme("Gamepad", _gamepads[num - 1]);
    }
}