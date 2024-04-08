using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    Transform player;
    //Odleg�o�� od ko�ca poziomu
    public float levelExitDistance = 100;
    //Punkt ko�ca poziomu
    public Vector3 exitPosition;
    public GameObject exitPrefab;
    //Zmienna - flaga - oznaczaj�ca uko�czenie poziomu
    public bool levelComplete = false;
    //Taka sama zmienna tylko je�li przegramy
    public bool LevelFailed = false;
    // Start is called before the first frame update
    void Start()
    {
        //Znajd� gracza
        player = GameObject.FindGameObjectWithTag("Player").transform;
        //Wylosuj pozycj� na kole o �rednicy 100 jednostek
        Vector2 spawnCircle = Random.insideUnitCircle; //Losowa pozycja x,y wewn�trz ko�a o r=1
        //Chcemy tylko pozycj� na okr�gu, a nie wewn�trz ko�a
        spawnCircle = spawnCircle.normalized; //Pozycje x,y w odleg�o�ci 1 od �rodka
        spawnCircle *= levelExitDistance; //Pozycja x,y w odleg�o�ci 100 od �rodka
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
