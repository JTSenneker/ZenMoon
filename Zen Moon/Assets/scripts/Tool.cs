using UnityEngine;
using System.Collections;

public class Tool : MonoBehaviour {

	public enum ToolType
    {
        hoe,
        wateringCan
    }

    public ToolType toolType;

    public string AnimVar()
    {
        switch (toolType)
        {
            case ToolType.hoe:
                return "isHoe";
            case ToolType.wateringCan:
                return "isWater";
        }
        return null;
    }
}
