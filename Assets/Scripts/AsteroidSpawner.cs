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
        if(timeSinceSpawn > 0.1 )
        {
            GameObject asteroid = SpawnAsteroid(staticAsteroid);
            timeSinceSpawn = 0;
        }

        AsteroidCountControll();
    }

    GameObject? SpawnAsteroid(GameObject prefab)
    {
        //Generyczna funkcja s�u��ca do wylosowania wsp�rz�dnych i umieszczenia w tym miejscu asteroidy z prefaba

        //Stw�rz losow� pozycj� na okr�gu (x,y)
        Vector2 randomCirclePosition = Random.insideUnitCircle.normalized;

        //Losowa pozycja w odleg�o�ci 10 jednostek od �rodka �wiata
        //Mapujemy x -> x, y -> z, a y ustawiamy 0
        Vector3 randomPosition = new Vector3(randomCirclePosition.x, 0, randomCirclePosition.y) * 10;

        //Na�� pozycj� gracza - teraz mamy pozycje 10 jednostek od gracza
        randomPosition += player.position;

        //Sprawd� czy miejsce jest wolne
        //! oznacza "nie" czyli nie ma nic w promieniu 5 jednostek od miejsca randomPosition
        if(!Physics.CheckSphere(randomPosition, 5))
        {
            //Stw�rz zmienna asteroid, zespawnuj nowy asteroid korzystaj�c z prefaba w losowym miejscu, z rotacj� domy�ln� (Quaternion.identity)
            GameObject asteroid = Instantiate(staticAsteroid, randomPosition, Quaternion.identity);

            //Zwr�� asteroid� jako wynik dzia�ania
            return asteroid;
        }
        else
        {
            return null;
        }
    }
    void AsteroidCountControll()
    {
        //Przygotuj tablic� wszystkich asteroid�w na scenie
        GameObject[] asteroids = GameObject.FindGameObjectsWithTag("Asteroid");

        //Przejd� p�tl� przez wszystkie
        foreach (GameObject asteroid in asteroids)
        {
            //Odleg�o�� od gracza

            //Wektor przesuni�cia miedzy graczem a asteroid�
            //O ile musz� przesun�� gracza, �eby znalaz� si� w miejscu asteroidy
            Vector3 delta = player.position - asteroid.transform.position;

            //Magnitude to d�ugo�� wektora = odleg�o�� od gracza
            float distanceToPlayer = delta.magnitude;

            if ( distanceToPlayer > 30)
            {
                Destroy(asteroid);
            }
        }
    }
}
