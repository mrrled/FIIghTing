using UnityEngine;

[CreateAssetMenu(fileName = "CharacterDataBase", menuName = "Scriptable Objects/CharacterDataBase")]
public class CharacterDataBase : ScriptableObject
{
    public CharacterData[] character;

    public int CharacterCount => character.Length;
    
    public CharacterData GetCharacter(int index) => character[index];
}
