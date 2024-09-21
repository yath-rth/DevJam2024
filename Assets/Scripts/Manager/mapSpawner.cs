using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class mapSpawner : MonoBehaviour
{
    public static mapSpawner Grid;

    [SerializeField] GameColorManager colorManager;
    [SerializeField] GameObject ground;

    Vector2 mapsize;
    Vector2 Maxmapsize = new Vector2(12, 12);
    public List<GameObject> tile = new List<GameObject>();
    [SerializeField] List<GameObject> spawner = new List<GameObject>();
    [SerializeField] GameObject spawner_corner, coll;
    [Range(0, 50)]
    public int maxSizeX, maxSizeY, minSizeX, minSizeY;
    [Range(-5, 5)]
    public float outlinePercent, tileHeight = 1f, camOffset, obstacleHeight, spawner_height;
    [Range(-5f, 5f)]
    public float tileSize = 1f;
    [SerializeField] List<GameObject> obstacles = new List<GameObject>();

    Color foregroundColor;
    Color backgroundColor;

    [HideInInspector]
    public List<GameObject> positions, spawnPos;

    List<Transform> mapBlockers;

    List<float> rotations = new List<float>();

    void Start()
    {
        rotations.Add(0);
        rotations.Add(90);
        rotations.Add(-90);
        rotations.Add(180);

        if (Grid != null && Grid != this)
        {
            Destroy(this);
        }
        else
        {
            Grid = this;
        }

        positions = new List<GameObject>((int)(mapsize.x * mapsize.y));
        spawnPos = new List<GameObject>((int)(mapsize.x * mapsize.y));

        //tileScripts = new List<Tile>((int)(mapsize.x * mapsize.y));

        GenerateGrid();
    }

    private void OnDestroy()
    {
        if (Grid == this)
        {
            Grid = null;
        }
    }

    Vector3 CoordToPosition(int x, int y)
    {
        return new Vector3(-mapsize.x / 2f + 0.5f + x, 0, -mapsize.y / 2f + 0.5f + y) * tileSize;
    }

    public void GenerateGrid()
    {
        Color[] colors = colorManager.getColor();
        backgroundColor = colors[0];
        foregroundColor = colors[1];

        mapsize = new Vector2(UnityEngine.Random.Range(minSizeX, maxSizeX), UnityEngine.Random.Range(minSizeY, maxSizeY));
        Maxmapsize = new Vector2(maxSizeX, maxSizeY);

        string holderName = "Generated Map";
        if (transform.Find(holderName))
        {
            DestroyImmediate(transform.Find(holderName).gameObject);
            positions = new List<GameObject>((int)(mapsize.x * mapsize.y));
            spawnPos = new List<GameObject>((int)(mapsize.x * mapsize.y));
            rotations = new List<float>();
            //tileScripts = new List<Tile>((int)(mapsize.x * mapsize.y));
        }

        Transform mapHolder = new GameObject(holderName).transform;
        mapHolder.parent = transform;

        rotations.Add(0);
        rotations.Add(90);
        rotations.Add(-90);
        rotations.Add(180);

        ground.transform.localScale = new Vector3((float)Math.Sqrt(mapsize.x), ground.transform.localScale.y, (float)Math.Sqrt(mapsize.y));

        for (int x = 0; x < mapsize.x; x++)
        {
            for (int y = 0; y < mapsize.y; y++)
            {
                GameObject newTile = null;
                int random_index = UnityEngine.Random.Range(0, tile.Count);
                if ((x == 0) && spawner != null)
                {
                    Vector3 tilePosition = CoordToPosition(x, y);
                    if (x == 0 && y == mapsize.y - 1)
                    {
                        newTile = Instantiate(spawner_corner, tilePosition, tile[random_index].transform.rotation);
                    }
                    else
                    {
                        newTile = Instantiate(spawner[UnityEngine.Random.Range(0, spawner.Count)], tilePosition, tile[random_index].transform.rotation);
                    }
                    newTile.transform.eulerAngles = new Vector3(0, -90, 0);

                    spawnPos.Add(newTile);

                    newTile.transform.parent = mapHolder;
                    newTile.transform.localScale = new Vector3(-tileSize, tileSize * spawner_height, -tileSize);
                }
                else if (x == mapsize.x - 1 && spawner != null)
                {
                    Vector3 tilePosition = CoordToPosition(x, y);
                    if (x == mapsize.x - 1 && y == mapsize.y - 1)
                    {
                        newTile = Instantiate(spawner_corner, tilePosition, tile[random_index].transform.rotation);
                        newTile.transform.eulerAngles = new Vector3(0, 0, 0);
                    }
                    else
                    {
                        newTile = Instantiate(spawner[UnityEngine.Random.Range(0, spawner.Count)], tilePosition, tile[random_index].transform.rotation);
                        newTile.transform.eulerAngles = new Vector3(0, 90, 0);
                    }

                    spawnPos.Add(newTile);

                    newTile.transform.parent = mapHolder;
                    newTile.transform.localScale = new Vector3(-tileSize, tileSize * spawner_height,-tileSize);

                }
                else if (x != 0 && x != mapsize.x - 1)
                {
                    if (y == mapsize.y - 1 && spawner != null)
                    {
                        Vector3 tilePosition = CoordToPosition(x, y);
                        newTile = Instantiate(spawner[UnityEngine.Random.Range(0, spawner.Count)], tilePosition, tile[random_index].transform.rotation);

                        spawnPos.Add(newTile);

                        newTile.transform.parent = mapHolder;
                        newTile.transform.localScale = new Vector3(-tileSize,tileSize * spawner_height,-tileSize);

                        //tileScripts.Add(newTile.GetComponent<Tile>());

                        // Renderer obstacleRenderer = newTile.GetComponent<Renderer>();
                        // Material obstacleMaterial = new Material(obstacleRenderer.sharedMaterial);
                        // float colourPercent = x / (float)mapsize.x;
                        // obstacleMaterial.SetColor("_color", Color.Lerp(foregroundColor, backgroundColor, colourPercent));
                        // obstacleRenderer.sharedMaterial = obstacleMaterial;
                    }
                    else
                    {
                        Vector3 tilePosition = CoordToPosition(x, y);
                        newTile = Instantiate(tile[random_index], tilePosition, tile[random_index].transform.rotation);
                        newTile.transform.eulerAngles = new Vector3(newTile.transform.rotation.x, rotations[UnityEngine.Random.Range(0, 4)], newTile.transform.rotation.z);

                        positions.Add(newTile);

                        newTile.transform.parent = mapHolder;
                        newTile.transform.localScale = new Vector3(1 * (1 - outlinePercent) * -tileSize, (1 - outlinePercent) * tileSize * tileHeight, 1 * (1 - outlinePercent) * -tileSize);

                        //tileScripts.Add(newTile.GetComponent<Tile>());

                        Renderer obstacleRenderer = newTile.GetComponent<Renderer>();
                        Material obstacleMaterial = new Material(obstacleRenderer.sharedMaterial);
                        float colourPercent = x / (float)mapsize.x;
                        obstacleMaterial.SetColor("_color", Color.Lerp(foregroundColor, backgroundColor, colourPercent));
                        obstacleRenderer.sharedMaterial = obstacleMaterial;
                    }
                }
            }
        }

        Transform obstacleHolder = new GameObject("obstacle holder").transform;
        obstacleHolder.parent = mapHolder;

        CinemachineTargetGroup targetGroup = obstacleHolder.gameObject.AddComponent<CinemachineTargetGroup>();
        targetGroup.m_Targets = new CinemachineTargetGroup.Target[10];
        targetGroup.m_UpdateMethod = CinemachineTargetGroup.UpdateMethod.FixedUpdate;

        CinemachineTargetGroup.Target target;
        target.weight = 1;
        target.radius = 0;

        if (obstacles.Count > 0)
        {
            for (int i = 0; i < positions.Count; i++)
            {
                float a = UnityEngine.Random.Range(0f, 1f);
                if (a > 0.95f)
                {
                    GameObject obstacle = Instantiate(obstacles[UnityEngine.Random.Range(0, obstacles.Count)], positions[i].transform.position, positions[i].transform.rotation);
                    obstacle.transform.eulerAngles = new Vector3(0, UnityEngine.Random.Range(0f, 360), 0);
                    obstacle.transform.parent = mapHolder;
                    obstacle.transform.localScale = new Vector3(1 * (1 - outlinePercent) * -tileSize, (1 - outlinePercent) * tileSize * UnityEngine.Random.Range(obstacleHeight / 2, obstacleHeight), 1 * (1 - outlinePercent) * -tileSize);
                }
            }
        }

        if (coll != null)
        {
            mapBlockers = new List<Transform>();

            Transform maskLeft = Instantiate(coll, Vector3.left * (mapsize.x + Maxmapsize.x) / 4f * tileSize, Quaternion.identity).transform;
            maskLeft.parent = mapHolder;
            maskLeft.localScale = new Vector3((Maxmapsize.x - mapsize.x) / 2f, 5, mapsize.y + 1f) * tileSize;

            Transform maskRight = Instantiate(coll, Vector3.right * (mapsize.x + Maxmapsize.x) / 4f * tileSize, Quaternion.identity).transform;
            maskRight.parent = mapHolder;
            maskRight.localScale = new Vector3((Maxmapsize.x - mapsize.x) / 2f, 5, mapsize.y + 1f) * tileSize;

            Transform maskTop = Instantiate(coll, Vector3.forward * (mapsize.y + Maxmapsize.y) / 4f * tileSize, Quaternion.identity).transform;
            maskTop.parent = mapHolder;
            maskTop.localScale = new Vector3(Maxmapsize.x + 1f, 5, (Maxmapsize.y - mapsize.y) / 2f) * tileSize;

            Transform maskBottom = Instantiate(coll, Vector3.back * (mapsize.y + Maxmapsize.y) / 4f * tileSize, Quaternion.identity).transform;
            maskBottom.parent = mapHolder;
            maskBottom.localScale = new Vector3(Maxmapsize.x + 1f, 5, (Maxmapsize.y - mapsize.y) / 2f) * tileSize;


            mapBlockers.Add(maskLeft);
            mapBlockers.Add(maskRight);
            mapBlockers.Add(maskTop);
            mapBlockers.Add(maskBottom);
        }
    }

    public GameObject getRandomPos()
    {
        return positions[UnityEngine.Random.Range(0, positions.Count)];
    }
}
