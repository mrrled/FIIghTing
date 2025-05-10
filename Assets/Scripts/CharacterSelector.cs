using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelector : MonoBehaviour
{
    public CharacterDataBase db;
    public SpriteRenderer sprite;
    public int numPlayer;
    
    private int _selectedCharacter;
    void Start()
    {
        _selectedCharacter = PlayerPrefs.GetInt($"SelectedCharacter{numPlayer}", 0);
        UpdateCharacter();
    }

    public void NextCharacter()
    {
        _selectedCharacter = (_selectedCharacter >= db.CharacterCount - 1)
            ? 0
            : _selectedCharacter + 1;
        UpdateCharacter();
        Save();
    }

    public void PreviousCharacter()
    {
        _selectedCharacter = (_selectedCharacter <= 0)
            ? db.CharacterCount - 1
            : _selectedCharacter - 1;
        UpdateCharacter();
        Save();
    }

    private void UpdateCharacter()
    {
        var character = db.GetCharacter(_selectedCharacter);
        sprite.sprite = character.characterPreview;
    }

    private void Save()
    {
        PlayerPrefs.SetInt($"SelectedCharacter{numPlayer}", _selectedCharacter);
    }

    public void ChangeScene(int id)
    {
        SceneManager.LoadScene(id);
    }
}
