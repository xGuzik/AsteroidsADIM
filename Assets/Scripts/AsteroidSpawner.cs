using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    //Gracz (jego pozycja)
    Transform player;

    //Prefab statycznej asteroidy
    public GameObject staticAsteroid;

    //Czas od ostatnio wygenerowanej asteroidy
    float timeSinceSpawn;

    // Start is called before the first frame update
    void Start()
    {
        //Znajd� gracza i przypisz go do zmiennej
        player = GameObject.FindWithTag("Player").transform;

        //Zeruj czas
        timeSinceSpawn = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //Dolicz czas od ostatniej klatki
        timeSinceSpawn += Time.deltaTime;
        //Je�eli czas przekroczy� sekund� do spawnuj i zresetuj
        if(timeSinceSpawn > 1 )
        {
            GameObject asteroid = SpawnAsteroid(staticAsteroid);
            timeSinceSpawn = 0;
        }
        
    }

    GameObject SpawnAsteroid(GameObject prefab)
    {
        //Generyczna funkcja s�u��ca do wylosowania wsp�rz�dnych i umieszczenia w tym miejscu asteroidy z prefaba

        //Losowa pozycja w odleg�o�ci 10 jednostek od �rodka �wiata
        Vector3 randomPosition = Random.onUnitSphere * 10;

        //Na�� pozycj� gracza - teraz mamy pozycje 10 jednostek od gracza
        randomPosition += player.position;

        //Stw�rz zmienna asteroid, zespawnuj nowy asteroid korzystaj�c z prefaba w losowym miejscu, z rotacj� domy�ln� (Quaternion.identity)
        GameObject asteroid = Instantiate(staticAsteroid, randomPosition, Quaternion.identity);

        //Zwr�� asteroid� jako wynik dzia�ania
        return asteroid;
    }
}
