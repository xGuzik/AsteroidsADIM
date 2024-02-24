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
        //Dodaj do wsp�rz�dnych warto�� x=1, y=0, z=0 pomno�one przez czas 
        //Mierzony w sekundach od ostatniej klatki
        //transform.position += new Vector3(1, 0, 0) * Time.deltaTime;

        //Prezentacja dzia�ania wyg�adzonego sterowania (emulcja joystika)
        //Debug.Log(Input.GetAxis("Vertical"));

        //Sterowanie pr�dko�ci�
        //Stw�rz nowy wektor przesuni�cia o warto�ci 1 do przodu
        Vector3 movement = transform.forward;
        //Pomn� go przez czas od ostatniej klatki
        movement *= Time.deltaTime;
        //Pomn� go przez wychylenie joystika
        movement *= Input.GetAxis("Vertical");
        //pomn� przez pr�dko�� lotu
        movement *= flySpeed;
        //Dodaj ruch do obiektu
        transform.position += movement;

        //Obr�t
        //Modyfikuj o� "Y" obiektu player
        Vector3 rotation = Vector3.up;
        //Przemn� przez czas
        rotation *= Time.deltaTime;
        //Przemn� przez klawiatur�
        rotation *= Input.GetAxis("Horizontal");
        //Pomn� przez pr�dko�� obrotu
        rotation *= rotationSpeed;
        //Dodaj obr�t do obiektu
        //Nie mo�emy u�y� += poniewa� unity u�ywa Quaternion�w do zapisu rotacji
        transform.Rotate(rotation);

    }
}
