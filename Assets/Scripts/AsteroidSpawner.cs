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
        //ZnajdŸ gracza i przypisz go do zmiennej
        player = GameObject.FindWithTag("Player").transform;

        //Zeruj czas
        timeSinceSpawn = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //Dolicz czas od ostatniej klatki
        timeSinceSpawn += Time.deltaTime;
        //Je¿eli czas przekroczy³ sekundê do spawnuj i zresetuj
        if(timeSinceSpawn > 1 )
        {
            GameObject asteroid = SpawnAsteroid(staticAsteroid);
            timeSinceSpawn = 0;
        }
        
    }

    GameObject SpawnAsteroid(GameObject prefab)
    {
        //Generyczna funkcja s³u¿¹ca do wylosowania wspó³rzêdnych i umieszczenia w tym miejscu asteroidy z prefaba

        //Losowa pozycja w odleg³oœci 10 jednostek od œrodka œwiata
        Vector3 randomPosition = Random.onUnitSphere * 10;

        //Na³ó¿ pozycjê gracza - teraz mamy pozycje 10 jednostek od gracza
        randomPosition += player.position;

        //Stwórz zmienna asteroid, zespawnuj nowy asteroid korzystaj¹c z prefaba w losowym miejscu, z rotacj¹ domyœln¹ (Quaternion.identity)
        GameObject asteroid = Instantiate(staticAsteroid, randomPosition, Quaternion.identity);

        //Zwróæ asteroidê jako wynik dzia³ania
        return asteroid;
    }
}
