using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //Wspó³rzêdne gracza
    Transform player;
    //Wysokoœæ kamery
    public float cameraHeight = 10.0f;
    //Prêdkoœæ kamery - do u¿ytku dla smootdamp
    Vector3 cameraSpeed;
    //Szybkoœæ wyg³adzania ruchu kamery - dla smoothdamp
    public float dampSpeed = 1f;
    // Start is called before the first frame update
    void Start()
    {
        //Pod³¹cz pozycjê gracza do lokalnej zmiennej korzystaj¹c z jego taga
        //To nie jest zapisanie wartoœci jeden raz tylko referencja do obiektu
        //to znaczy, ¿e Player zawsze bêdzie zawiera³ aktualn¹ pozycjê gracza
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        //oblicz docelow¹ pozycjê kamery
        Vector3 targetPosition = player.position + Vector3.up * cameraHeight;
        //P³ynnie przesuñ kamerê w kierunku gracza
        //Funkcja Vector3.Lerp
        //P³ynnie przechodzi z pozycji pierwszego argumentu do pozycji drugiego w czasie trzeciego
        //transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime);

        //Smoothdamp dzia³a jak sprê¿yna staraj¹ca sie doci¹gn¹æ kamerê do was
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref cameraSpeed, dampSpeed);
    }
}
