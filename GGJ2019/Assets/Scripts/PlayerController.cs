﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float moveSpeed = 1;

    public Vector3 position;

    private bool isMoving = false;

    // Start is called before the first frame update
    void Start() {
        // Store reference to attached controller
        position = new Vector3(13,8,0);
    }

    // Update is called once per frame
    void Update() {
        if (!isMoving) {
            changePosition();
        }
        this.transform.position = new Vector3(position.x, position.y, 0);

    }

    // public void move() {
    //     // NEXT: Vector2.Lerp + time.deltaTime
    //     this.transform.position = new Vector3(position.x, position.y, 0);
    // }

    public void changePosition() {

        /*
            make (static public) tilecontroller accessible to PlayerController.
            find "next" tile
            change move speed according to type of tile.
            if fn in tile --> move player 
            pick up hat
         */

        TileController[,] tiles = GameController.tcArray;

        int toX = 0;
        int toY = 0;

        if (Input.GetKeyDown("w")) {
            toY = 1;
        }
        else if (Input.GetKeyDown("a")) {
            toX = -1;
        }
        else if (Input.GetKeyDown("s")) {
            toY = -1;
        }
        else if (Input.GetKeyDown("d")) {
            toX = 1;            
        }

        if (Mathf.Abs(toX) > 0 || Mathf.Abs(toY) > 0) {
            Vector2Int toPosition = new Vector2Int((int)position.x + toX, (int) position.y + toY);
            // TileController tc = tiles[toPosition.x, toPosition.y];
            // tc.

            StartCoroutine(Move((int)toPosition.x, (int)toPosition.y, this.moveSpeed));
        }
        
    }

    public IEnumerator Move(float x, float y, float speed) {
        isMoving = true;
        Vector3 startPosition = new Vector3(position.x, position.y, 0);
        Vector3 endPosition = new Vector3(x, y, 0);
        // float pos =0;
        for (float i =0; i<1; i += speed * Time.deltaTime) {
            position = Vector3.Lerp(startPosition, endPosition, i);
            yield return null;
        }
        position = endPosition;
        isMoving = false;

    }
}
