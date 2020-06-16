using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveAndLoadGame : MonoBehaviour
{
    public void SaveGame()
    {
        SaveLoad.SaveGame();
    }
    public void LoadGame()
    {
        SaveLoad.LoadGame();
    }
}


[System.Serializable] //Обязательно нужно указать, что класс должен сериализоваться
public class SaveData
{
    //Создание полей с игровыми параметрами
    public int currHP;
    public int HP;

    public int currXP;
    public int XP;

    public int level;

    public float[] position; //В Unity позиция игрока записана с помощью класса Vector3, но его нельзя сериализовать. Чтобы обойти эту проблему, данные о позиции будут помещены в массив типа float.

    public SaveData() //Конструктор класса
    {
        //Получение данных, которые нужно сохранить
        HP = MainData.OverallHP;
        currHP = MainData.CurrentHP;
        
        XP = MainData.OverallXP;
        currXP = MainData.CurrentXP;

        level = MainData.CurrentLevel;

        position = new float[3] //Получение позиции
		{
            MainData.CurrentPosition.x,
            MainData.CurrentPosition.y,
            MainData.CurrentPosition.z,
        };
    }

}
public static class SaveLoad //Создание статичного класса позволит использовать методы без объявления его экземпляров
{

    private static string path = Application.persistentDataPath + "/gamesave.skillbox"; //Путь к сохранению. Вы можете использовать любое расширение
    private static BinaryFormatter formatter = new BinaryFormatter(); //Создание сериализатора 

    public static void SaveGame() //Метод для сохранения
    {

        FileStream fs = new FileStream(path, FileMode.Create); //Создание файлового потока

        SaveData data = new SaveData(); //Получение данных

        formatter.Serialize(fs, data); //Сериализация данных

        fs.Close(); //Закрытие потока

    }

    public static SaveData LoadGame() //Метод загрузки
    {
        if (File.Exists(path))
        { //Проверка существования файла сохранения
            FileStream fs = new FileStream(path, FileMode.Open); //Открытие потока

            SaveData data = formatter.Deserialize(fs) as SaveData; //Получение данных

            fs.Close(); //Закрытие потока

            return data; //Возвращение данных
        }
        else
        {
            return null; //Если файл не существует, будет возвращено null
        }

    }
}
