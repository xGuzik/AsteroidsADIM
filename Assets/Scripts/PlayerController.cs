using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    public float rotationSpeed = 100f;
    public float flySpeed = 5f;
    // Start is called before the first frame update
    void Start()
    {
        
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

    }

    private void OnCollisionEnter(Collision collision)
    {
        //Uruchamia się automatycznie jeśli zetkniemy się z innym coliderem

        //Sprawdzamy czy dotkneliśmy asteroidy
        if (collision.collider.transform.CompareTag("Asteroid"))
        {
            Debug.Log("Boom!");
            //Pauza
            Time.timeScale = 0;
        }
    }
}
