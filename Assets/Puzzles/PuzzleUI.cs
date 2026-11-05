using TMPro;
using UnityEngine;

public class PuzzleUI : MonoBehaviour
{
    public static PuzzleUI Instance 
    { 
        get; 
        private set; 
    }

    [SerializeField] private TMP_Text debugText;

    private void Awake()
    {
        Instance = this;
        SetText("");
    }

    public void SetText(string message)
    {
        debugText.text = message;
    }
}
