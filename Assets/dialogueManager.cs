using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class dialogueManager : MonoBehaviour
{
    public Queue<string> sentences;
    public Text nameText;
    public Text diaText;
    public Animator anim;
    // Start is called before the first frame update
    void Awake()
    {
        sentences = new Queue<string>();

    }

    public void startDialogue(dialogue dia)
    {
        anim.SetBool("isOpen", true);
        nameText.text = dia.name;
        sentences.Clear();
        foreach (string sentence in dia.sentences){
            sentences.Enqueue(sentence);
        }
        displayNextSentence();
    }

    public void displayNextSentence()
    {
        if (sentences.Count == 0)
        {
            endDialogue();
            return;
        }
        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(typeSentence(sentence));
    }

    IEnumerator typeSentence(string s)
    {
        diaText.text = "";
        foreach (char letter in s.ToCharArray())
        {
            diaText.text += letter;
            yield return new WaitForSeconds(0.02f);
        }
    }

    public void endDialogue()
    {
        anim.SetBool("isOpen", false);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
