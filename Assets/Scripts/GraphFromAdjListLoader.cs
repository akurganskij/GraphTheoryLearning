using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GraphFromAdjListLoader : MonoBehaviour
{
    public void HandleOnVertexInputEndedEvent()
    {
        Text inp = gameObject.GetComponent<Text>();
        int v;
        if (int.TryParse(inp.text, out v))
        {
            if (v > 0)
            {

            }
            else inp.text = "";
        }
    }
}
