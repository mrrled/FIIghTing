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
        var index = PlayerPrefs.GetInt($"SelectedCharacter{num}");
        var character = num == 1 
            ? characterDatabase1.GetCharacter(index) 
            : characterDatabase2.GetCharacter(index);
        Instantiate(character.characterPrefab);
    }
}