using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour {

    [SerializeField] private Sprite[] palette = default;

    public void CheckSelfSprite(string blocksAround) {
        Sprite selectedSprite;
        if (palette.Length > 16) {
            selectedSprite = CheckHard(blocksAround);
        } else {
            string simpleBlocksAround = blocksAround.Substring(1, 1) + blocksAround.Substring(3, 3) + blocksAround.Substring(7, 1);
            selectedSprite = CheckSimple(simpleBlocksAround);
        }
        gameObject.GetComponent<SpriteRenderer>().sprite = selectedSprite;
    }

    private Sprite CheckSimple(string blocksAround) {
        switch (blocksAround) {
            case "0" + "111" + "1": return palette[0];
            case "1" + "110" + "1": return palette[1];
            case "1" + "111" + "0": return palette[2];
            case "1" + "011" + "1": return palette[3];
            case "0" + "110" + "1": return palette[4];
            case "0" + "111" + "0": return palette[5];
            case "0" + "011" + "1": return palette[6];
            case "1" + "110" + "0": return palette[7];
            case "1" + "010" + "1": return palette[8];
            case "1" + "011" + "0": return palette[9];
            case "0" + "110" + "0": return palette[10];
            case "0" + "010" + "1": return palette[11];
            case "0" + "011" + "0": return palette[12];
            case "1" + "010" + "0": return palette[13];
            case "0" + "010" + "0": return palette[14];
            case "1" + "111" + "1": return palette[15];
            default: return palette[14];
        }
    }

    private Sprite CheckHard(string blocksAround) {
        switch (blocksAround) {
            case "000" + "010" + "000":
                // todo
                break;
            default:
                break;
        }
        throw new System.NotImplementedException(); // todo
    }
}
