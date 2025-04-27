using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelector : MonoBehaviour
{
    public CharacterDataBase db;

    public SpriteRenderer sprite;
    public int numPlayer;
    
    private int selectedCharacter;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (!PlayerPrefs.HasKey($"selectedCharacter{numPlayer}"))
        {
            selectedCharacter = 0;
        }
        else
        {
            Load();
        }
        UpdateCharacter();
    }

    public void NextCharacter()
    {
        selectedCharacter++;
        if (selectedCharacter >= db.CharacterCount)
            selectedCharacter = 0;
        UpdateCharacter();
        Save();
    }

    public void PreviousCharacter()
    {
        selectedCharacter--;
        if (selectedCharacter < 0)
            selectedCharacter = db.CharacterCount - 1;
        UpdateCharacter();
        Save();
    }

    private void UpdateCharacter()
    {
        CharacterData character = db.GetCharacter(selectedCharacter);
        sprite.sprite = character.characterPreview;
    }

    private void Load()
    {
        selectedCharacter = PlayerPrefs.GetInt($"selectedCharacter{numPlayer}", selectedCharacter);
    }

    private void Save()
    {
        PlayerPrefs.SetInt($"selectedCharacter{numPlayer}", selectedCharacter);
    }

    public void ChangeScene(int id)
    {
        SceneManager.LoadScene(id);
    }
}
