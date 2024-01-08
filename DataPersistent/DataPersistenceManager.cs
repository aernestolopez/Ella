using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class DataPersistenceManager : MonoBehaviour
{
    [Header("Debugging")]
    [SerializeField] private bool initializeDataIfNull=false;

    [Header("Configuración de Guardado de Archivos")]
    [SerializeField] private string fileName;
    [SerializeField] private bool useEncryption;
    private GameData gameData;
    private List<IDataPersistence> dataPersistenceObjects;
    private FileDataHandler dataHandler;

    public static DataPersistenceManager instance { get; private set; }
    
    private void Awake()
    {
        if (instance !=null)
        {
            Debug.Log("Encontrado más de un Manager de persistencia de datos, destruyendo el más nuevo");
            Destroy(this.gameObject);
            return;
        }
        instance=this;

        DontDestroyOnLoad(this.gameObject);

        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName, useEncryption);
    }

    //Se llama Primero en el ciclo de vida
    private void OnEnable()
    {
        //Nos suscribimos a los eventos
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    //Se llama Ultimo en el ciclo de vida
    private void OnDisable()
    {
        //Nos desuscribimos de los eventos
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    //Se llama antes del Start()
    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        this.dataPersistenceObjects = FindAllDataPersistenceObjects();
        LoadGame();

    }

    public void NewGame()
    {
        //Creamos un nuevo GameData
        this.gameData=new GameData();
    }

    public void LoadGame()
    {
        //Cargar desde un archivo de guardado
        this.gameData=dataHandler.Load();

        //Si es nulo se crea uno nuevo
        if(this.gameData == null && initializeDataIfNull)
        {
            NewGame();
        }

        //Si no hay archivo de juego no continua

        if(this.gameData==null)
        {
            Debug.Log("No hay datos encontrados. Un nuevo juego debe ser creado");
            return;
        }

        foreach(IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.LoadData(gameData);
        }
    }

    public void SaveGame()
    {
        if(this.gameData == null)
        {
            Debug.LogWarning("No hay datos de juego");
            return;
        }

        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.SaveData(ref gameData);
        }
        //Guardar datos a un archivo usando el DataHandler
        dataHandler.Save(gameData);
    }

    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        //Encuentra todos los monoBehaviour que tienen tipo IDataPersistence
        IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>()
        .OfType<IDataPersistence>();

        //Se guarda en una lista
        return new List<IDataPersistence>(dataPersistenceObjects);
    }

    public bool HasGameData()
    {
        return gameData != null;
    }
}
