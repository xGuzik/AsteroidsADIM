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
        //Dodaj do wspó³rzêdnych wartoœæ x=1, y=0, z=0 pomno¿one przez czas 
        //Mierzony w sekundach od ostatniej klatki
        //transform.position += new Vector3(1, 0, 0) * Time.deltaTime;

        //Prezentacja dzia³ania wyg³adzonego sterowania (emulcja joystika)
        //Debug.Log(Input.GetAxis("Vertical"));

        //Sterowanie prêdkoœci¹
        //Stwórz nowy wektor przesuniêcia o wartoœci 1 do przodu
        Vector3 movement = transform.forward;
        //PomnóŸ go przez czas od ostatniej klatki
        movement *= Time.deltaTime;
        //Pomnó¿ go przez wychylenie joystika
        movement *= Input.GetAxis("Vertical");
        //pomnó¿ przez prêdkoœæ lotu
        movement *= flySpeed;
        //Dodaj ruch do obiektu
        transform.position += movement;

        //Obrót
        //Modyfikuj oœ "Y" obiektu player
        Vector3 rotation = Vector3.up;
        //Przemnó¿ przez czas
        rotation *= Time.deltaTime;
        //Przemnó¿ przez klawiaturê
        rotation *= Input.GetAxis("Horizontal");
        //Pomnó¿ przez prêdkoœæ obrotu
        rotation *= rotationSpeed;
        //Dodaj obrót do obiektu
        //Nie mo¿emy u¿yæ += poniewa¿ unity u¿ywa Quaternionów do zapisu rotacji
        transform.Rotate(rotation);

    }
}
