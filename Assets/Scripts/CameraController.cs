using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //Wsp�rz�dne gracza
    Transform player;
    //Wysoko�� kamery
    public float cameraHeight = 10.0f;
    //Pr�dko�� kamery - do u�ytku dla smootdamp
    Vector3 cameraSpeed;
    //Szybko�� wyg�adzania ruchu kamery - dla smoothdamp
    public float dampSpeed = 1f;
    // Start is called before the first frame update
    void Start()
    {
        //Pod��cz pozycj� gracza do lokalnej zmiennej korzystaj�c z jego taga
        //To nie jest zapisanie warto�ci jeden raz tylko referencja do obiektu
        //to znaczy, �e Player zawsze b�dzie zawiera� aktualn� pozycj� gracza
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        //oblicz docelow� pozycj� kamery
        Vector3 targetPosition = player.position + Vector3.up * cameraHeight;
        //P�ynnie przesu� kamer� w kierunku gracza
        //Funkcja Vector3.Lerp
        //P�ynnie przechodzi z pozycji pierwszego argumentu do pozycji drugiego w czasie trzeciego
        //transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime);

        //Smoothdamp dzia�a jak spr�yna staraj�ca sie doci�gn�� kamer� do was
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref cameraSpeed, dampSpeed);
    }
}
