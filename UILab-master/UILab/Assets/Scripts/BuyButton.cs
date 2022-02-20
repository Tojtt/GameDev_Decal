using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyButton : MonoBehaviour {

	public int itemID;

	public void BuyItem() {
		
		for (int i = 0; i < ItemShop.itemShop.itemList.Count; i++) {
			if (!ItemShop.itemShop.itemList [i].bought && ItemShop.itemShop.itemList [i].itemID == itemID) {
				if (GameManager.gameManager.HasEnough(ItemShop.itemShop.itemList [i].itemPrice)){
					ItemShop.itemShop.itemList [i].bought = true;
					GameManager.gameManager.ReduceMoney(ItemShop.itemShop.itemList [i].itemPrice);
				}
			}
		}
		ItemShop.itemShop.UpdateSprite (itemID);
	}
}
