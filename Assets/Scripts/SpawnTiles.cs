using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTiles : MonoBehaviour
{
    public GameObject TileToSpawn;
    public GameObject ReferenceTile;
    public GameObject EmptyTileToSpawn;
    public GameObject Block;
    public GameObject Coin;
    private bool empty, changeDirection;
    private int distanceBetweenTiles = 8;
    public float randomDirectionValue = 0.8f;
    public float randomEmptyValue = 0.8f;
    public float randomBlockValue = 0.3f;
    public float timeOffset = 0.4f;
    public float randomCoinValue = 0.3f;
    private Vector3 previousTilePosition;
    private float startTime;
    private Queue<GameObject> tilesQueue;
    private GameObject previousTile;
    private Vector3 direction, mainDirection = Vector3.forward, left = Vector3.left, right = Vector3.right;

    // Start is called before the first frame update
    void Start()
    {
        tilesQueue = new Queue<GameObject>();
        empty = false;
        changeDirection = false;
        previousTilePosition = ReferenceTile.transform.position;
        startTime = Time.time;
        for (int i=0; i< transform.childCount; i++) 
            tilesQueue.Enqueue(transform.GetChild(i).gameObject);
        previousTile = tilesQueue.Peek();
        tilesQueue.Dequeue();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - startTime >= timeOffset) {
            Destroy(previousTile);
            previousTile = tilesQueue.Peek();
            tilesQueue.Dequeue();

            if (!changeDirection && Random.value < randomDirectionValue) {
                changeDirection = true;
                if ((int)(Random.value * 10) % 2 == 0) direction = left;
                else direction = right;

                if (left == Vector3.forward) left = Vector3.left;
                else if (left == Vector3.left) left = Vector3.back;
                else if (left == Vector3.back) left = Vector3.right;
                else if (left == Vector3.right) left = Vector3.forward;

                if (right == Vector3.forward) right = Vector3.right;
                else if (right == Vector3.right) right = Vector3.back;
                else if (right == Vector3.back) right = Vector3.left;
                else if (right == Vector3.left) right = Vector3.forward;
                mainDirection = direction;
            }

            else {
                changeDirection = false;
                direction = mainDirection;
            }

            Vector3 spawnPos = previousTilePosition + distanceBetweenTiles * direction;
            startTime = Time.time;
            
            if (!empty && Random.value < randomEmptyValue) {
                empty = true;
                GameObject obj = Instantiate(EmptyTileToSpawn, spawnPos, Quaternion.Euler(0, 0, 0), transform);
                tilesQueue.Enqueue(obj);
            }

            else {
                empty = false;
                GameObject obj = Instantiate(TileToSpawn, spawnPos, Quaternion.Euler(0, 0, 0));
                obj.transform.parent = transform;
                for (int i=0; i<3; i++) 
                {
                    if (Random.value < randomCoinValue) 
                    {
                        Instantiate(Coin, obj.transform.GetChild(i).position, Quaternion.Euler(0, 0, 0), obj.transform.GetChild(i));
                    }
                }

                if (Random.value < randomBlockValue) {
                    GameObject block = Instantiate(Block, spawnPos + Block.transform.localPosition, Quaternion.Euler(0, 0, 0), obj.transform);
                }

                tilesQueue.Enqueue(obj);
            }
            previousTilePosition = spawnPos;
        }
    }
}
