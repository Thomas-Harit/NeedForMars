using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using Random = UnityEngine.Random;

public class Sky : MonoBehaviour
{
    [SerializeField] private GameObject ship;
    [SerializeField] private Camera cam;
    [SerializeField] private CinemachineVirtualCamera vcam;
    private float minHeight = 80;
    private float maxHeight = 100;
    private Color color = new Color(0, 125f / 255f, 205f / 255f);

    public GameObject starPrefab; // Reference to the star prefab
    public int numberOfStars = 1000; // Number of stars to generate initially
    public float starSpawnDistance = 300f; // Distance around the ship where stars are spawned
    public float starRecycleDistance = 310f; // Distance at which stars are recycled

    private List<GameObject> stars; // List to keep track of star instances
    private bool starsGenerated = false;

    private void Start()
    {
        stars = new List<GameObject>();
    }

    void Update()
    {
        if (this.ship.transform.position.y > minHeight)
        {
            this.cam.backgroundColor = color - color * Math.Min((this.ship.transform.position.y - minHeight) / (maxHeight - minHeight), 1);
            this.vcam.GetCinemachineComponent<CinemachineFramingTransposer>().m_CameraDistance = 4 +  Math.Min((this.ship.transform.position.y - minHeight) / (maxHeight - minHeight) * 10, 10);
        }

        if (this.ship.transform.position.y > maxHeight)
        {
            if (starsGenerated == false)
            {
                starsGenerated = true;
                GenerateInitialStars();
            }
            RecycleStars();
            if (this.ship.transform.position.y < maxHeight + 40)
            {
                foreach (GameObject star in stars)
                {
                    star.transform.localScale = new Vector3(0.5f, 0.5f, 1) * Math.Min((this.ship.transform.position.y - maxHeight) / 40, 1);
                }
            }
        }
    }
    
    void GenerateInitialStars()
    {
        for (int i = 0; i < numberOfStars; i++)
        {
            Vector3 starPosition = GetRandomPositionAroundShip();
            GameObject star = Instantiate(starPrefab, starPosition, Quaternion.identity);
            stars.Add(star);
        }
    }

    Vector3 GetRandomPositionAroundShip()
    {
        return new Vector3(
            Random.Range(ship.transform.position.x - starSpawnDistance, ship.transform.position.x + starSpawnDistance),
            Random.Range(ship.transform.position.y - starSpawnDistance, ship.transform.position.y + starSpawnDistance),
            Random.Range(20, 40)
        );
    }

    void RecycleStars()
    {
        foreach (GameObject star in stars)
        {
            if (Vector2.Distance(ship.transform.position, star.transform.position) > starRecycleDistance)
            {
                star.transform.position = GetRandomPositionAroundShip();
            }
        }
    }
}
