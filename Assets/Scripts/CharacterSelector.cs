using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelector : MonoBehaviour
{
    public CharacterDataBase db;

    public SpriteRenderer sprite;
    public int numPlayer;
    
    private int _selectedCharacter;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _selectedCharacter = !PlayerPrefs.HasKey($"selectedCharacter{numPlayer}") 
            ? 0 
            : PlayerPrefs.GetInt($"selectedCharacter{numPlayer}", _selectedCharacter);
        UpdateCharacter();
    }

    public void NextCharacter()
    {
        _selectedCharacter = (_selectedCharacter >= db.CharacterCount - 1)
            ? 0
            : _selectedCharacter + 1;
        _selectedCharacter++;
        // if (_selectedCharacter >= db.CharacterCount)
        //     _selectedCharacter = 0;
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
        PlayerPrefs.SetInt($"selectedCharacter{numPlayer}", _selectedCharacter);
    }

    public void ChangeScene(int id)
    {
        SceneManager.LoadScene(id);
    }
}
