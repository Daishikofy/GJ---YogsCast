using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequestManager : MonoBehaviour
{
    private static RequestManager instance;

    private int requestsNumber;
    private Queue<Request> requests;

    private Request testRequest;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }
    public static RequestManager Instance { get { return instance; } }

    // Start is called before the first frame update
    public void Start()
    {
        requests = new Queue<Request>();
    }

    public void generateNewRequest()
    {
        int id = requestsNumber++;
        string requestLetter = "No letter for the moment";
        int[] attributes = new int[3];
        attributes[0] = Random.Range(0, 3);
        attributes[1] = Random.Range(0, 3);
        attributes[2] = Random.Range(0, 3);

        var request = new Request(id, requestLetter, attributes);
        requests.Enqueue(request);
        requestsNumber += 1;
    }

    public bool validateRequest(Plantimal plantimal)
    {
        if (requestsNumber == 0)
        {
            Debug.LogWarning("Il n'y a pas de request à résoudre");
            return false;
        }
        var lastRequest = requests.Dequeue();
        requestsNumber -= 1;

        int[] requestAttributes = lastRequest.getAttributes();
        int[] plantimalAttributes = plantimal.sendPlantimal();

        int success = lastRequest.getHapiness();
        float loss = success / requestAttributes.Length; 

        for (int i = 0; i < requestAttributes.Length; i++)
        {
            if (requestAttributes[i] != plantimalAttributes[i])
                success = (int)((float)success - loss);
        }
        if (success <= 0)
            success = 0;
        //DEBUG
        string att1 = requestAttributes[0].ToString() + requestAttributes[1].ToString() + requestAttributes[2].ToString();
        string att2 = plantimalAttributes[0].ToString() + plantimalAttributes[1].ToString() + plantimalAttributes[2].ToString();
        Debug.Log("Request: " + att1 + " - Plantimal: "+ att2);
        Debug.Log("Seccess: " + success);

        GameManager.Instance.addHappinessPoints(success);
        return true;
    }
}
