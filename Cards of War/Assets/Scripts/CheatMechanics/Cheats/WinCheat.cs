using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WinCheat : Cheat {
    [SerializeField] Sprite winSprite;
    GameObject winBadge = null;
    bool holdingBadge = false;

    public override float getChargeWinAmount() {
        return 2.5f;
    }

    public override string getName() {
        return "Win Cheat";
    }

    //  this cheat makes the active card's value increase by 100, automatically winning
    public override void use() {
        //  the player played the cheat
        //  starts holding the badge
        if(gameObject.tag == "Player") {
            if(Input.GetMouseButtonDown(0)) {
                var hit = getMouseHit();
                if((hit != null && hit == winBadge) || holdingBadge) {
                    holdingBadge = true;
                    winBadge.GetComponent<ObjectShadow>().showShadow();
                }
            }
            
            //  moves the badge with the mouse if the player is holding it
            else if(Input.GetMouseButton(0) && holdingBadge) {
                winBadge.transform.DOMove((Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition), 0.1f);
            }

            //  if the player lets go of the badge, things happen
            else if(Input.GetMouseButtonUp(0) && holdingBadge) {
                var col = winBadge.GetComponent<Collider2D>();
                col.enabled = false;
                winBadge.GetComponent<ObjectShadow>().hideShadow();
                winBadge.GetComponent<ObjectShadow>().destroyShadow();
                holdingBadge = false;
                var hit = getMouseHit();
                col.enabled = true;
                bool returnToOrigin = true;

                var cm = FindObjectOfType<CardMovement>();
                var cbm = FindObjectOfType<CardBattleMechanics>();
                //  adds to player
                if(hit != null) {
                    if(hit == cbm.getPlayerPlayedCard().gameObject) {
                        cbm.setTempPlayerCardValueMod(cbm.getTempPlayerCardValueMod() + 100);
                        addBadgeToCard(hit);
                        setChargeAmount(0.0f);
                        returnToOrigin = false;
                    }
                    //  adds to opponent
                    else if(hit == cbm.getOpponentPlayedCard().gameObject) {
                        cbm.setTempOpponentCardValueMod(cbm.getTempOpponentCardValueMod() + 100);
                        addBadgeToCard(hit);
                        setChargeAmount(0.0f); 
                        returnToOrigin = false;
                    }
                }

                if(returnToOrigin) {
                    if(gameObject.tag == "Player")
                        winBadge.transform.DOMove(gameObject.GetComponentInChildren<Deck>().getDeckPos() + new Vector2(2.5f, 0.0f), 0.25f);
                    else if(gameObject.tag == "Opponent")
                        winBadge.transform.DOMove(gameObject.GetComponentInChildren<Deck>().getDeckPos() - new Vector2(2.5f, 0.0f), 0.25f);
                }
            }
        }

        //  the opponent played the cheat
        else if(gameObject.tag == "Opponent") {
            FindObjectOfType<CardBattleMechanics>().setTempOpponentCardValueMod(FindObjectOfType<CardBattleMechanics>().getTempOpponentCardValueMod() + 100);
        }

        //setChargeAmount(0.0f);
    }

    GameObject getMouseHit() {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
        if(hit.collider != null)
            return hit.collider.gameObject;
        return null;
    }


    void addBadgeToCard(GameObject card) {
        winBadge.GetComponent<Collider2D>().enabled = false;
        winBadge.GetComponent<ObjectShadow>().destroyShadow();
        winBadge.transform.SetParent(card.transform);
        winBadge.GetComponent<SpriteRenderer>().sortingOrder = card.GetComponent<SpriteRenderer>().sortingOrder + 1;
        winBadge = null;
    }


    public override void showCanUse() {
        if(winBadge == null) {
            winBadge = new GameObject("Win Badge");
            var sr = winBadge.AddComponent<SpriteRenderer>();
            sr.sprite = winSprite;
            sr.sortingOrder = 2;
            winBadge.AddComponent<CircleCollider2D>();
            winBadge.AddComponent<ObjectShadow>();

            if(gameObject.tag == "Player") {
                winBadge.transform.position = gameObject.GetComponentInChildren<Deck>().getDeckPos() + new Vector2(2.5f, -1.0f);
                winBadge.transform.DOMove(gameObject.GetComponentInChildren<Deck>().getDeckPos() + new Vector2(2.5f, 0.0f), 0.15f);
            }
            else if(gameObject.tag == "Opponent") {
                winBadge.transform.position = gameObject.GetComponentInChildren<Deck>().getDeckPos() - new Vector2(2.5f, -1.0f);
                winBadge.transform.DOMove(gameObject.GetComponentInChildren<Deck>().getDeckPos() - new Vector2(2.5f, 0.0f), 0.15f);
            }
        }
        winBadge.SetActive(true);
    }

    public override void hideCanUse() {
    }


    public override bool useCondition() {
        if(!getCharged())
            return false;

        //  returns false if no enough cards for cheat to be played
        CardMovement cm = FindObjectOfType<CardMovement>();
        CardBattleMechanics cbm = FindObjectOfType<CardBattleMechanics>();
        if(gameObject.tag == "Player") {
            if(gameObject.GetComponentInChildren<Deck>().getNumOfCardsInDeck() <= 0 && cbm.getShown())
                return false;
        }
        else if(gameObject.tag == "Opponent") {
            if(gameObject.GetComponentInChildren<Deck>().getNumOfCardsInDeck() <= 0 && cbm.getShown())
                return false;
        }
        return true;
    }
}
