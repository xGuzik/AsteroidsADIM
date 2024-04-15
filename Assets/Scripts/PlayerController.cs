using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    public float rotationSpeed = 100f;
    public float flySpeed = 5f;
    //Odniesienie do menadżera poziomu
    GameObject levelManagerObject;
    //Stan osłon w procentach 1 = 100%
    float shieldCapacity = 1;

    // Start is called before the first frame update
    void Start()
    {
        levelManagerObject = GameObject.Find("LevelManager");
    }

    // Update is called once per frame
    void Update()
    {
        //Dodaj do współrzędnych wartość x=1, y=0, z=0 pomnożone przez czas 
        //Mierzony w sekundach od ostatniej klatki
        //transform.position += new Vector3(1, 0, 0) * Time.deltaTime;

        //Prezentacja działania wygładzonego sterowania (emulcja joystika)
        //Debug.Log(Input.GetAxis("Vertical"));

        //Sterowanie prędkością
        //Stwórz nowy wektor przesunięcia o wartości 1 do przodu
        Vector3 movement = transform.forward;
        //Pomnóź go przez czas od ostatniej klatki
        movement *= Time.deltaTime;
        //Pomnóż go przez wychylenie joystika
        movement *= Input.GetAxis("Vertical");
        //pomnóż przez prędkość lotu
        movement *= flySpeed;
        //Dodaj ruch do obiektu
        //Zmiana na fizyke
        // --- transform.position += movement;

        //Komponent fizyki wewnątrz gracza
        Rigidbody rb = GetComponent<Rigidbody>();
        //Dodaj siłe - do przodu statku w trybie zmiany prędkości
        rb.AddForce(movement, ForceMode.VelocityChange);

        //Obrót
        //Modyfikuj oś "Y" obiektu player
        Vector3 rotation = Vector3.up;
        //Przemnóż przez czas
        rotation *= Time.deltaTime;
        //Przemnóż przez klawiaturę
        rotation *= Input.GetAxis("Horizontal");
        //Pomnóż przez prędkość obrotu
        rotation *= rotationSpeed;
        //Dodaj obrót do obiektu
        //Nie możemy użyć += ponieważ unity używa Quaternionów do zapisu rotacji
        transform.Rotate(rotation);
        UpdateUI();
    }

    private void UpdateUI()
    {
        //Metoda wykonuje wszystko związane z aktualizacją interfejsu użytkownika 

        //Wyciągnij z menadżera poziomu pozycję wyjścia
        Vector3 target = levelManagerObject.GetComponent<LevelManager>().exitPosition;
        //Obróć znacznik w stronę wyjścia
        transform.Find("NavUI").Find("TargetMarker").LookAt(target);
        //Zmień ilość procentowo widoczna w interfejsie
        //TODO: Poprawić wyświetlanie stanu osłon!
        TextMeshProUGUI shieldText = 
            GameObject.Find("Canvas").transform.Find("ShieldCapacityText").GetComponent<TextMeshProUGUI>();
        shieldText.text = "Shield: " + (shieldCapacity*100).ToString() + "%";

        //Sprawdzamy czy poziom się zakończył i czy musimy wyświetlić ekran końcowy
        if (levelManagerObject.GetComponent<LevelManager>().levelComplete)
        {
            //Znajdź canvas (interfejs), znajdź w nim ekran końca poziomu i go włącz
            GameObject.Find("Canvas").transform.Find("LevelCompleteScreen").gameObject.SetActive(true);
        }
        //Sprawdzamy czy poziom się zakończył i czy musimy wyświetlić ekran końcowy
        if (levelManagerObject.GetComponent<LevelManager>().LevelFailed)
        {
            //Znajdź canvas (interfejs), znajdź w nim ekran końca poziomu i go włącz
            GameObject.Find("Canvas").transform.Find("GameOverScreen").gameObject.SetActive(true);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Uruchamia się automatycznie jeśli zetkniemy się z innym coliderem

        //Sprawdzamy czy dotkneliśmy asteroidy
        if (collision.collider.transform.CompareTag("Asteroid"))
        {
            //Transform asteroidy
            Transform asteroid = collision.collider.transform;
            //Policz wektor według którego odepchniemy asteroidę
            Vector3 shieldForce = asteroid.position - transform.position;
            //Popchnij asteroidę
            asteroid.GetComponent<Rigidbody>().AddForce(shieldForce * 5, ForceMode.Impulse);
            shieldCapacity -= 0.25f;
            if(shieldCapacity <= 0)
            {
                //Poinformuj levelManager, że gra się zakończyła bo nie mamy osłon
                levelManagerObject.GetComponent<LevelManager>().LevelFailed = true;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        //Jeżeli dotkniemy znacznika końca poziomu to ustaw w levelManager flagę, że poziom jest ukończony
        if (other.transform.CompareTag("LevelExit"))
        {
            //Wywołaj dla LevelManager metodę zakończenia poziomu
            levelManagerObject.GetComponent<LevelManager>().OnSuccess();
        }
    }
}
