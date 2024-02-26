using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidGenerator : MonoBehaviour
{
    //Model zawieraj�cy trzy kostki
    GameObject model;
    //Wylosowana rotacja/s
    Vector3 rotation = Vector3.one;
    // Start is called before the first frame update
    void Start()
    {
        //Przypisuje do zmiennej model obiekt - pojemnik zawieraj�cy kostki b�d�ce cz�ci� modelu asteroidy
        model = transform.Find("Model").gameObject;
        //Przygotuj generator liczb losowych
        //Random r = new Random();
        //Nie robimy tego bo unity.random jest statyczne w przeciwie�stwie do system.random
        //Iteruj przez cz�ci modelu 
        foreach (Transform cube in model.transform)
        {
            //U�yj wbudowanego random.rotation
            cube.rotation = Random.rotation;

            //Losowa liczba
            float scale = Random.Range(0.9f, 1.1f);

            //Przeskaluj 
            cube.localScale = new Vector3 (scale, scale, scale);
        }

        //Wylosuj jednorazowo rotacje/s naszej asteroidy
        rotation.x = Random.value;
        rotation.y = Random.value;
        rotation.z = Random.value;
        rotation *= Random.Range(10, 20);
    }

    // Update is called once per frame
    void Update()
    {
        //Obr�� asteroid� (Model) w wyznaczonym kierunku
        model.transform.Rotate(rotation * Time.deltaTime);
    }
}
