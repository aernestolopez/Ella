using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using CandyCoded.env;

public class FileDataHandler
{
    private string dataDirPath="";
    private string dataFileName="";
    private bool useEncryption = false;
    private string encryptionCodeWord;


    public FileDataHandler(string dataDirPath, string dataFileName, bool useEncryption)
    {
        this.dataDirPath = dataDirPath;
        this.dataFileName = dataFileName;
        this.useEncryption= useEncryption;
    }

    public GameData Load()
    {
        //Path.Combine permite combinar varios path para evitar problemas con el separador de archivos
        string fullPath=Path.Combine(dataDirPath, dataFileName);
        GameData loadedData = null;
        if (File.Exists(fullPath))
        {
            try
            {   
                //Cargar los datos serializados desde el archivo
                string dataToLoad = "";
                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using(StreamReader reader=new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }

                //Desencriptar Datos opcional
                if(useEncryption)
                {
                    dataToLoad = EncryptDecrypt(dataToLoad);
                }

                //Deserializar los datos del json para el C#

                loadedData=JsonUtility.FromJson<GameData>(dataToLoad);

            }
            catch (Exception e)
            {
                Debug.LogError("Error ocurrido mientras se intentaba cargar datos desde " + fullPath + "\n" + e);
            }

        }
        return loadedData;
    }

    public void Save(GameData data)
    {

        //Path.Combine permite combinar varios path para evitar problemas con el separador de archivos
        string fullPath=Path.Combine(dataDirPath, dataFileName);

        try{
            //Creamos el directorio para guardar el archivo
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
            //Serializamos el C# game data en un json
            string dataToStore=JsonUtility.ToJson(data, true);
            //opcional encriptar datos
            if(useEncryption)
            {
                dataToStore = EncryptDecrypt(dataToStore);
            }
            //Escribimos los datos serializados en un archivo
            using (FileStream stream=new FileStream(fullPath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }
        }catch (Exception e){

            Debug.LogError("Error ocurrido mientras se intentaba guardar datos en el archivo " + fullPath +"\n" + e);
        }

       
    }
     //Para la encriptacion se utiliza una implementacion de XOR encryption
    private string EncryptDecrypt(string data)
    {
        if (env.TryParseEnvironmentVariable("CODEWORD", out string palabra))
        {
        encryptionCodeWord=palabra;
        }
        string modifiedData="";
        for (int i=0; i<data.Length; i++)
        {
            modifiedData +=(char) (data[i] ^ encryptionCodeWord[i % encryptionCodeWord.Length]);
        }
        return modifiedData;
    }
}
