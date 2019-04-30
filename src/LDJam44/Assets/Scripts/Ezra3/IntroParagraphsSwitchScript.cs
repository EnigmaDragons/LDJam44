using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IntroParagraphsSwitchScript : MonoBehaviour
{
    public Navigation navigation;
    public TextMeshProUGUI paragraphText;
    public int indexNumber = 0;
    public string[] paragraphs = {"Money had inflated so much, the galaxy was on the verge of economic collapse.Until people found a way to transfer their Life Force as payments.This quickly became the new currency that stabilized the economy."
        ,"Recently government empowered entities are banning more and more perfectly reasonable products everyday. With increasingly insane laws and taxes, piracy is on the rise!"
        ,"You are a lone smuggler, moving banned products between stations trying to not get caught or plundered. Most people die but you are determined to make it rich!"};

    public void NextParagraph()
    {
        indexNumber++;
        if (indexNumber != paragraphs.Length)
            paragraphText.text = paragraphs[indexNumber];
        else
            navigation.NavigateToSpaceStation();
    }
}