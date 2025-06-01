using UnityEngine;

public class CharacterLoader : MonoBehaviour
{
    public CharacterDataBase characterDatabase1;
    public CharacterDataBase characterDatabase2;

    void Awake()
    {
        LoadCharacter(1);
        LoadCharacter(2);
    }

    private void LoadCharacter(int num)
    {
        var camera = Camera.main;
        var halfHeight = camera.orthographicSize;
        var halfWidth = halfHeight * camera.aspect;
        Debug.Log(num);
        Debug.Log(halfWidth);
        Debug.Log(halfHeight);
        var index = PlayerPrefs.GetInt($"SelectedCharacter{num}");
        var position = Vector3.zero;
        position.y = camera.transform.position.y - halfHeight;
        if (num == 1)
        {
            // position.x = camera.transform.position.x + halfWidth;
            // position.x /= 2;
            var character = characterDatabase1.GetCharacter(index);
            Instantiate(character.characterPrefab, position, Quaternion.identity);
        }
        else
        {
            // position.x = camera.transform.position.x - halfWidth;
            // position.x /= 2;
            var character = characterDatabase2.GetCharacter(index);
            Instantiate(character.characterPrefab, position, Quaternion.identity);
        }
        Debug.Log(position);
    }
}