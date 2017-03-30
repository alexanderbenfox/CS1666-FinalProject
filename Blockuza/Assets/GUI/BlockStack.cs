using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class BlockStack : MonoBehaviour {

	public TileSelector selector;

	public Image block1,block2,block3;

	public Sprite whiteBlock, orangeBlock;

	private Color slideColor, stickyColor;

	void resetBlocks(){
		block1.sprite = null;
		block2.sprite = null;
		block3.sprite = null;
		block1.color = Color.white;
		block2.color = Color.white;
		block3.color = Color.white;
	}

	void Start(){
		resetBlocks ();
		slideColor = new Color (255, 255, 0, 255);
		stickyColor = new Color (25, 255, 199, 255);
	}

	void FixedUpdate () {
		resetBlocks ();
		//int bCount = 0;
		List<BlockType> b = selector.savedBlocks;
		for (int i = 0; i < b.Count; i++) {
			Sprite showSprite = new Sprite ();
			Color color = Color.white;
			switch (b [i]) {
			case (BlockType.Destroyable):
				showSprite = orangeBlock;
				break;
			case(BlockType.Sliding):
				showSprite = whiteBlock;
				color = slideColor;
				break;
			case(BlockType.Sticky):
				showSprite = whiteBlock;
				color = stickyColor;
				break;
			default:
				break;
			}
			if (i == 0) {
				block1.sprite = showSprite;
				block1.color = color;
			}
			if (i == 1) {
				block2.sprite = showSprite;
				block2.color = color;
			}
			if (i == 2) {
				block3.sprite = showSprite;
				block3.color = color;
			}
		}
	}
}
