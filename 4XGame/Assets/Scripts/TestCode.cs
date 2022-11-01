using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCode : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameTile desert = new DesertTile();
        Debug.Log(desert);

        Debug.Log("--------------------------------------------");

        GameTile hills = new HillsTile();
        Debug.Log(hills);

        Debug.Log("--------------------------------------------");

        GameTile plains = new PlainsTile();
        Debug.Log(plains);

        Debug.Log("--------------------------------------------");

        GameTile mountain = new MountainTile();
        Debug.Log(mountain);

        Debug.Log("--------------------------------------------");

        GameTile water = new WaterTile();
        Debug.Log(water);
        
    }
}
