using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridScript : MonoBehaviour
{
    public GameObject tile;
    public float tileSize = 1f;
    private float lineLength = 2;
    public int layers;
    public LayerMask gridLayer;

    public GameObject gridParent;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(-0.5f, 0.5f, 10f);
        SpawnGrid(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void SpawnTile()
    {
        Instantiate(tile, new Vector3(transform.position.x, transform.position.y, 0), transform.rotation, gridParent.transform);
    }
    void SpawnGrid()
    {
        for(int layer = 1; layer < layers + 1; layer++)
        {
            for(int line = 1; line < 5; line++)
                {
                    for(int i = 1; i < lineLength; i++)
                    {
                        if(Physics2D.BoxCast(transform.position, new Vector2(tileSize/2, tileSize/2), 0, Vector2.left, 0).collider == null)
                        {
                            SpawnTile();
                        }
                        transform.position += transform.right * tileSize;
                    }
                    transform.Rotate(0, 0, -90);
                }
            lineLength += 2;
            transform.position = new Vector3(transform.position.x - tileSize, transform.position.y + tileSize, 0);
        }
    }
}
