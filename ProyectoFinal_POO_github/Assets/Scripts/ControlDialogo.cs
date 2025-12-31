using UnityEngine;
using System.Collections;
using TMPro;


public class ControlDialogo : MonoBehaviour
{
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private GameObject monedasPanel;
    [SerializeField] private GameObject puntosPanel;
    [SerializeField] private GameObject botonOmitir;

    [SerializeField] private CanvasGroup fondoNegro;
    [SerializeField] private float fadeDuration = 1f;

    [SerializeField, TextArea(2, 6)] private string[] dialogueLines;
    

    private bool didDialogueStart;
    private int lineIndex;
    private float typingTime = 0.05f;

    void Start()
    {
        monedasPanel.SetActive(false);
        puntosPanel.SetActive(false);
        if (!didDialogueStart)
        {
            StartDialogue();

        }

    }

    void Update()
    {
        

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (dialogueText.text == dialogueLines[lineIndex])
            {
                NextDialogue();
            }
            else
            {
                StopAllCoroutines();
                dialogueText.text = dialogueLines[lineIndex];
            }
        }

        if (!didDialogueStart)
        {
            monedasPanel.SetActive(true);
            puntosPanel.SetActive(true);
        }



    }
    private void StartDialogue()
    {
        didDialogueStart = true;
        dialoguePanel.SetActive(true);
        botonOmitir.SetActive(true);
        lineIndex = 0;
        Time.timeScale = 0f;

        // MOSTRAR CURSOR
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        StartCoroutine(ShowLine());

    }

    private void NextDialogue(){
        lineIndex++;
        if (lineIndex < dialogueLines.Length)
        {
            StartCoroutine(ShowLine());
        }
        else { 
            didDialogueStart=false;
            dialoguePanel.SetActive(false);
            botonOmitir.SetActive(false);

            Time.timeScale = 1f;

            StartCoroutine(FadeOutFondoNegro());  // transicion fondo negro
        }
    
    }
    private IEnumerator ShowLine()
    {
        dialogueText.text = string.Empty;
        foreach (char c in dialogueLines[lineIndex])
        {
            dialogueText.text += c;     // se escribe un caracter
            yield return new WaitForSecondsRealtime(typingTime);   //se espera a que salga el siguiente

        }

    }

    public void SkipDialogue()
    {
        
        if (didDialogueStart) {
            StopAllCoroutines();          // Detiene el efecto de escritura
            didDialogueStart = false;     // Marca que el diálogo terminó
            dialoguePanel.SetActive(false); // Oculta el panel de historia
            botonOmitir.SetActive(false);

            monedasPanel.SetActive(true); // Activa HUD
            puntosPanel.SetActive(true);

            Time.timeScale = 1f;          // Reanuda el juego

            StartCoroutine(FadeOutFondoNegro());  // transicion fondo negro

            // OCULTAR CURSOR OTRA VEZ
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        
    }


    private IEnumerator FadeOutFondoNegro()
    {
        float elapsed = 0f;
        float startAlpha = fondoNegro.alpha;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.unscaledDeltaTime; // IMPORTANTE: funciona con Time.timeScale = 0
            fondoNegro.alpha = Mathf.Lerp(startAlpha, 0f, elapsed / fadeDuration);
            yield return null;
        }

        fondoNegro.alpha = 0f;
        fondoNegro.gameObject.SetActive(false);
    }
}
