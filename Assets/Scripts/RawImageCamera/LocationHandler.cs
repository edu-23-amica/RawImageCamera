using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Unity.Android;
using TMPro;

public class LocationHandler : MonoBehaviour
{
    [SerializeField] private float updateInterval = 2f;
    public TextMeshProUGUI textGPS;

    private IEnumerator Start()
    {
        /*
         SOLO PARA ANDROID
        if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
        {
            Permission.RequestUserPermission(Permission.FineLocation);
        }
        */

        if (!Input.location.isEnabledByUser)
        {
            Debug.LogWarning("GPS Disabled");
            yield break;
        }

        Input.location.Start();

        int maxWait = 20;

        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        if(maxWait <= 0)
        {
            Debug.LogWarning("GPS Start timeout");
            yield break;
        }

        if(Input.location.status == LocationServiceStatus.Failed)
        {
            Debug.LogWarning("GPS Location failed");
            yield break;
        }

        StartCoroutine(RoutineGetLocation());
    }

    IEnumerator RoutineGetLocation()
    {
        while (true)
        {
            LocationInfo location = Input.location.lastData;

            Debug.Log("Latitud: " + location.latitude +
                " | Longitud: " + location.longitude +
                " | Altitud: " + location.altitude +
                " | Precisión: " + location.horizontalAccuracy);

            textGPS.text = "Latitud: " + location.latitude +
                " | Longitud: " + location.longitude +
                " | Altitud: " + location.altitude +
                " | Precisión: " + location.horizontalAccuracy;

            yield return new WaitForSeconds(updateInterval);
        }
    }

    private void OnDisable()
    {
        Input.location.Stop();
    }


}
