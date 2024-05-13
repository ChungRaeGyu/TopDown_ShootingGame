using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField]private string playertag;

    public Transform player{get; private set;}

    public ObjectPool pool{get;private set;}


    private void Awake(){
        if(Instance != null)Destroy(gameObject);
        else
            Instance = this;

        player = GameObject.FindGameObjectWithTag(playertag).transform;
        pool = GameObject.FindObjectOfType<ObjectPool>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
