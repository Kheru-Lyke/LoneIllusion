using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TypewriterModuleUI : MonoBehaviour {
    private TextMeshProUGUI text;
    [SerializeField] private float secondsBetweenCharacters = 0.05f;

    private Coroutine currentCoroutine = null;

    // Start is called before the first frame update
    void Start() {
        text = GetComponent<TextMeshProUGUI>();
    }

    public bool Reveal(int totalLength) {

        if (currentCoroutine != null) {
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
