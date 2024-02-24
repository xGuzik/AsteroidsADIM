using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
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
        Vector3 movement = Vector3.forward;
        //PomnóŸ go przez czas od ostatniej klatki
        movement *= Time.deltaTime;
        //Pomnó¿ go przez wychylenie joystika
        movement *= Input.GetAxis("Vertical");
        //Dodaj ruch do obiektu
        transform.position += movement;
    }
}
