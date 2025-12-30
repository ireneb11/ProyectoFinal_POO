using UnityEngine;
using System.Collections;
using TMPro;

 

public class Textos : MonoBehaviour
{
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField, TextArea(2, 6)] private string[] dialogueLines;

    private bool didDialogueStart;
    private int lineIndex;
    private float typingTime = 0.05f;

    void Start() {
        if (!didDialogueStart)
        {
            StartDialogue();

        }

    }

    void Update() {
        // if
        /*
            if (!didDialogueStart) {
                StartDialogue();

            }
        */
    
    
    }
    private void StartDialogue() { 
        didDialogueStart = true;
        dialoguePanel.SetActive(true);
        lineIndex = 0;
        StartCoroutine(ShowLine());
    
    }
    private IEnumerator ShowLine() {
        dialogueText.text = string.Empty;
        foreach (char c in dialogueLines[lineIndex]) { 
            dialogueText.text += c;     // se escribe un caracter
            yield return new WaitForSeconds(typingTime);   //se espera a que salga el siguiente
        
        }
        
    }


}
