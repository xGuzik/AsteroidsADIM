using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //Współrzędne gracza
    Transform player;
    //Wysokość kamery
    public float cameraHeight = 10.0f;
    //Prędkość kamery - do użytku dla smootdamp
    Vector3 cameraSpeed;
    //Szybkość wygładzania ruchu kamery - dla smoothdamp
    public float dampSpeed = 1f;
    // Start is called before the first frame update
    void Start()
    {
        //Podłącz pozycję gracza do lokalnej zmiennej korzystając z jego taga
        //To nie jest zapisanie wartości jeden raz tylko referencja do obiektu
        //to znaczy, że Player zawsze będzie zawierał aktualną pozycję gracza
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        //oblicz docelową pozycję kamery
        Vector3 targetPosition = player.position + Vector3.up * cameraHeight;
        //Płynnie przesuń kamerę w kierunku gracza
        //Funkcja Vector3.Lerp
        //Płynnie przechodzi z pozycji pierwszego argumentu do pozycji drugiego w czasie trzeciego
        //transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime);

        //Smoothdamp działa jak sprężyna starająca sie dociągnąć kamerę do was
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref cameraSpeed, dampSpeed);
    }
}
