using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WinCheat : Cheat {
    public GameObject hand;
    GameObject handInstance;

    public Sprite winSprite;
    GameObject winBadge = null;
    bool holdingBadge = false;

    private void Start() {
        if(!getInUse()) return;

        if(hand == null)
            hand = FindObjectOfType<CheatIndex>().gameObject.GetComponent<WinCheat>().hand;
        if(winSprite == null)
            winSprite = FindObjectOfType<CheatIndex>().gameObject.GetComponent<WinCheat>().winSprite;

        handInstance = Instantiate(hand);
        handInstance.transform.SetParent(transform);
        handInstance.GetComponent<WinCheatHand>().forceHide();

        if(gameObject.tag == "Opponent" && GetComponent<OpponentAI>() == null)
            gameObject.AddComponent<WinCheatOpponentAI>();
    }

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

                //  if the player is holding the badge object
                if((hit != null && hit == winBadge) || holdingBadge) {
                    holdingBadge = true;
                    winBadge.GetComponent<SpriteRenderer>().sortingOrder = handInstance.GetComponent<SpriteRenderer>().sortingOrder + 2;
                    winBadge.GetComponent<ObjectShadow>().showShadow();


                    //  show the hand
                    handInstance.GetComponent<WinCheatHand>().show();
                }
            }
            
            //  moves the badge with the mouse if the player is holding it
            else if(Input.GetMouseButton(0) && holdingBadge) {
                winBadge.transform.DOMove((Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition), 0.1f);
            }

            //  if the player lets go of the badge, things happen
            else if(Input.GetMouseButtonUp(0) && holdingBadge) {
                var cm = FindObjectOfType<CardMovement>();
                var cbm = FindObjectOfType<CardBattleMechanics>();
                var col = winBadge.GetComponent<Collider2D>();
                col.enabled = false;
                winBadge.GetComponent<ObjectShadow>().destroyShadow();
                holdingBadge = false;
                col.enabled = true;


                var hit = getMouseHit();
                if(hit != null && hit == handInstance.gameObject) {
                    FindObjectOfType<CardBattleMechanics>().setTempPlayerCardValueMod(100);
                    addBadgeToHand();
                    setChargeAmount(0.0f);
                }

                
                else {
                    winBadge.GetComponent<ObjectShadow>().destroyShadow();
                    winBadge.transform.DOMove(gameObject.GetComponentInChildren<Deck>().getDeckPos() + new Vector2(1.5f, 0.0f), 0.25f);

                    handInstance.GetComponent<WinCheatHand>().hide();
                }
            }
        }


        //  the opponent played the cheat
        else if(gameObject.tag == "Opponent") {
            winBadge.transform.DOComplete();
            handInstance.GetComponent<WinCheatHand>().show();
            winBadge.GetComponent<SpriteRenderer>().sortingOrder = handInstance.GetComponent<SpriteRenderer>().sortingOrder + 2;

            FindObjectOfType<CardBattleMechanics>().setTempOpponentCardValueMod(100);

            StartCoroutine(opponentAddBadgeToHandWaiter());
            setChargeAmount(0.0f);
        }
    }

    GameObject getMouseHit() {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
        if(hit.collider != null)
            return hit.collider.gameObject;
        return null;
    }


    void addBadgeToHand() {
        winBadge.GetComponent<Collider2D>().enabled = false;
        winBadge.transform.SetParent(handInstance.gameObject.transform);
        handInstance.GetComponent<WinCheatHand>().hide();
        winBadge = null;
    }



    IEnumerator opponentAddBadgeToHandWaiter() {
        yield return new WaitForSeconds(0.5f);
        winBadge.transform.DOMove(new Vector3(handInstance.transform.position.x, handInstance.transform.position.y, winBadge.transform.position.z), 0.25f);

        yield return new WaitForSeconds(0.5f);
        addBadgeToHand();
    }


    public override void showCanUse() {
        if(winBadge == null) {
            winBadge = new GameObject("Win Badge");
            var sr = winBadge.AddComponent<SpriteRenderer>();
            sr.sprite = winSprite;
            winBadge.GetComponent<SpriteRenderer>().sortingOrder = handInstance.GetComponent<SpriteRenderer>().sortingOrder + 2;
            sr.maskInteraction = SpriteMaskInteraction.VisibleOutsideMask;
            winBadge.AddComponent<CircleCollider2D>();
            winBadge.AddComponent<ObjectShadow>();
            if(FindObjectOfType<TableCanvas>() != null)
                winBadge.transform.rotation = FindObjectOfType<TableCanvas>().getTableRotation();

            if(gameObject.tag == "Player") {
                winBadge.transform.position = gameObject.GetComponentInChildren<Deck>().getDeckPos() + new Vector2(1.5f, -1.0f);
                winBadge.transform.DOMove(gameObject.GetComponentInChildren<Deck>().getDeckPos() + new Vector2(1.5f, 0.0f), 0.15f);
            }
            else if(gameObject.tag == "Opponent") {
                winBadge.transform.position = gameObject.GetComponentInChildren<Deck>().getDeckPos() - new Vector2(1.5f, -1.0f);
                winBadge.transform.DOMove(gameObject.GetComponentInChildren<Deck>().getDeckPos() - new Vector2(1.5f, 0.0f), 0.15f);
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
            return gameObject.GetComponent<OpponentAI>().wantsToUseCheat;
        }
        return true;
    }
}
