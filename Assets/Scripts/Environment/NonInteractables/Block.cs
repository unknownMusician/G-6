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
            case "0" + "111" + "1": return palette.Length > 0 ? palette[0] : GetComponent<SpriteRenderer>().sprite;
            case "1" + "110" + "1": return palette.Length > 1 ? palette[1] : GetComponent<SpriteRenderer>().sprite;
            case "1" + "111" + "0": return palette.Length > 2 ? palette[2] : GetComponent<SpriteRenderer>().sprite;
            case "1" + "011" + "1": return palette.Length > 3 ? palette[3] : GetComponent<SpriteRenderer>().sprite;
            case "0" + "110" + "1": return palette.Length > 4 ? palette[4] : GetComponent<SpriteRenderer>().sprite;
            case "0" + "111" + "0": return palette.Length > 5 ? palette[5] : GetComponent<SpriteRenderer>().sprite;
            case "0" + "011" + "1": return palette.Length > 6 ? palette[6] : GetComponent<SpriteRenderer>().sprite;
            case "1" + "110" + "0": return palette.Length > 7 ? palette[7] : GetComponent<SpriteRenderer>().sprite;
            case "1" + "010" + "1": return palette.Length > 8 ? palette[8] : GetComponent<SpriteRenderer>().sprite;
            case "1" + "011" + "0": return palette.Length > 9 ? palette[9] : GetComponent<SpriteRenderer>().sprite;
            case "0" + "110" + "0": return palette.Length > 10 ? palette[10] : GetComponent<SpriteRenderer>().sprite;
            case "0" + "010" + "1": return palette.Length > 11 ? palette[11] : GetComponent<SpriteRenderer>().sprite;
            case "0" + "011" + "0": return palette.Length > 12 ? palette[12] : GetComponent<SpriteRenderer>().sprite;
            case "1" + "010" + "0": return palette.Length > 13 ? palette[13] : GetComponent<SpriteRenderer>().sprite;
            case "0" + "010" + "0": return palette.Length > 14 ? palette[14] : GetComponent<SpriteRenderer>().sprite;
            case "1" + "111" + "1": return palette.Length > 15 ? palette[15] : GetComponent<SpriteRenderer>().sprite; // todo: remove
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
