#nullable enable
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

    //Odleg³oœæ w jakiej spawnuj¹ siê asteroidy
    public float spawnDistance = 10;

    //Odleg³oœæ pomiêdzy asteroidami
    public float safeDistance = 10;

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
        SpawnAsteroid(staticAsteroid);

        AsteroidCountControll();
    }

    GameObject? SpawnAsteroid(GameObject prefab)
    {
        //Generyczna funkcja s³u¿¹ca do wylosowania wspó³rzêdnych i umieszczenia w tym miejscu asteroidy z prefaba

        //Stwórz losow¹ pozycjê na okrêgu (x,y)
        Vector2 randomCirclePosition = Random.insideUnitCircle.normalized;

        //Losowa pozycja w odleg³oœci 10 jednostek od œrodka œwiata
        //Mapujemy x -> x, y -> z, a y ustawiamy 0
        Vector3 randomPosition = new Vector3(randomCirclePosition.x, 0, randomCirclePosition.y) * spawnDistance;

        //Na³ó¿ pozycjê gracza - teraz mamy pozycje 10 jednostek od gracza
        randomPosition += player.position;

        //SprawdŸ czy miejsce jest wolne
        //! oznacza "nie" czyli nie ma nic w promieniu 5 jednostek od miejsca randomPosition
        if(!Physics.CheckSphere(randomPosition, safeDistance))
        {
            //Stwórz zmienna asteroid, zespawnuj nowy asteroid korzystaj¹c z prefaba w losowym miejscu, z rotacj¹ domyœln¹ (Quaternion.identity)
            GameObject asteroid = Instantiate(staticAsteroid, randomPosition, Quaternion.identity);

            //Zwróæ asteroidê jako wynik dzia³ania
            return asteroid;
        }
        else
        {
            return null;
        }
    }
    void AsteroidCountControll()
    {
        //Przygotuj tablicê wszystkich asteroidów na scenie
        GameObject[] asteroids = GameObject.FindGameObjectsWithTag("Asteroid");

        //PrzejdŸ pêtl¹ przez wszystkie
        foreach (GameObject asteroid in asteroids)
        {
            //Odleg³oœæ od gracza

            //Wektor przesuniêcia miedzy graczem a asteroid¹
            //O ile muszê przesun¹æ gracza, ¿eby znalaz³ siê w miejscu asteroidy
            Vector3 delta = player.position - asteroid.transform.position;

            //Magnitude to d³ugoœæ wektora = odleg³oœæ od gracza
            float distanceToPlayer = delta.magnitude;

            if ( distanceToPlayer > 30)
            {
                Destroy(asteroid);
            }
        }
    }
}
