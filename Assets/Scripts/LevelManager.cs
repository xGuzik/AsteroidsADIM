using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    Transform player;
    //Odleg³oœæ od koñca poziomu
    public float levelExitDistance = 100;
    //Punkt koñca poziomu
    public Vector3 exitPosition;
    public GameObject exitPrefab;
    //Zmienna - flaga - oznaczaj¹ca ukoñczenie poziomu
    public bool levelComplete = false;
    //Taka sama zmienna tylko jeœli przegramy
    public bool LevelFailed = false;
    // Start is called before the first frame update
    void Start()
    {
        //ZnajdŸ gracza
        player = GameObject.FindGameObjectWithTag("Player").transform;
        //Wylosuj pozycjê na kole o œrednicy 100 jednostek
        Vector2 spawnCircle = Random.insideUnitCircle; //Losowa pozycja x,y wewn¹trz ko³a o r=1
        //Chcemy tylko pozycjê na okrêgu, a nie wewn¹trz ko³a
        spawnCircle = spawnCircle.normalized; //Pozycje x,y w odleg³oœci 1 od œrodka
        spawnCircle *= levelExitDistance; //Pozycja x,y w odleg³oœci 100 od œrodka
        //Konwertujemy do Vector3
        //Podstawiamy x=x, y=0, z=y
        exitPosition = new Vector3(spawnCircle.x, 0, spawnCircle.y);
        Instantiate(exitPrefab, exitPosition, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
