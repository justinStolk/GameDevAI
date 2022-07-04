using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BTDisplayText : BTNode
{
    private Text textComponent;
    private string textLine;
    public BTDisplayText(Text displayComponent, string textToDisplay)
    {
        textComponent = displayComponent;
        textLine = textToDisplay;
    }
    public override BTResult Run()
    {
        textComponent.text = textLine;
        return BTResult.Success;
    }
}
