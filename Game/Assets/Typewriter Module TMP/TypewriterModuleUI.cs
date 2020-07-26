using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TypewriterModuleUI : MonoBehaviour {
    private TextMeshProUGUI text;
    [SerializeField] private float secondsBetweenCharacters = 0.05f;

    private Coroutine currentCoroutine = null;

    void Start() {
        text = GetComponent<TextMeshProUGUI>();
    }

    /// <summary>
    /// Shows the entire text
    /// </summary>
    /// <param name="totalLength">Length of the text to reveal</param>
    /// <returns>If the text was gradually revealed (false if instantly shown)</returns>
    public bool Reveal(int totalLength) {

        if (currentCoroutine != null) {             //If called when the text was gradually revealing, shows it entirely and instantly
            StopCoroutine(currentCoroutine);
            text.maxVisibleCharacters = text.textInfo.characterCount;

            currentCoroutine = null;
            return false;
        }

        currentCoroutine = StartCoroutine(TextReveal(totalLength));
        return true;
    }

    private IEnumerator TextReveal(int totalLength) {
        int count = 0;
        text.maxVisibleCharacters = 0;

        while (count < totalLength) {
            count++;
            text.maxVisibleCharacters = count;

            yield return new WaitForSeconds(secondsBetweenCharacters);
        }

        currentCoroutine = null;
        yield break;
    }
}
