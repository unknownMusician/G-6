﻿using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour {

    public PlaceForRoom[,] roomDirectionsDataMatrix;
    public GameObject[,] roomsMatrix;
    public GameObject[,] miniMapMatrix;

    public int rows;
    public int columns;
    public int currentRow;
    public int currentColumn;

    public List<GameObject> mapElementsPrefabs;
    private Dictionary<string, GameObject> mapElementsPrefabsDictionary;
    private Dictionary<string, GameObject[]> roomsPrefabsDictionary;

    public GameObject[] T;
    public GameObject[] R;
    public GameObject[] B;
    public GameObject[] L;
    public GameObject[] TR;
    public GameObject[] TB;
    public GameObject[] TL;
    public GameObject[] RB;
    public GameObject[] RL;
    public GameObject[] BL;
    public GameObject[] notT;
    public GameObject[] notR;
    public GameObject[] notB;
    public GameObject[] notL;
    public GameObject[] Base;
    public GameObject[] Block;

    private void Start() {

        roomDirectionsDataMatrix = new PlaceForRoom[rows, columns];
        roomsMatrix = new GameObject[rows, columns];
        miniMapMatrix = new GameObject[rows, columns];

        currentColumn = columns / 2;
        currentRow = rows / 2;

        PlaceForRoom baseRoom = new PlaceForRoom(1, 1, 1, 1);
        roomDirectionsDataMatrix[rows / 2, columns / 2] = baseRoom;

        mapElementsPrefabsDictionary = new Dictionary<string, GameObject>();
        // Dictionary things
        {
            mapElementsPrefabsDictionary.Add("2000", mapElementsPrefabs[0]);     // T
            mapElementsPrefabsDictionary.Add("0200", mapElementsPrefabs[1]);     // R
            mapElementsPrefabsDictionary.Add("0020", mapElementsPrefabs[2]);     // B
            mapElementsPrefabsDictionary.Add("0002", mapElementsPrefabs[3]);     // L
            mapElementsPrefabsDictionary.Add("2200", mapElementsPrefabs[4]);    // TR
            mapElementsPrefabsDictionary.Add("2020", mapElementsPrefabs[5]);    // TB
            mapElementsPrefabsDictionary.Add("2002", mapElementsPrefabs[6]);    // TL
            mapElementsPrefabsDictionary.Add("0220", mapElementsPrefabs[7]);    // RB
            mapElementsPrefabsDictionary.Add("0202", mapElementsPrefabs[8]);    // RL
            mapElementsPrefabsDictionary.Add("0022", mapElementsPrefabs[9]);    // BL
            mapElementsPrefabsDictionary.Add("0222", mapElementsPrefabs[10]); // notT
            mapElementsPrefabsDictionary.Add("2022", mapElementsPrefabs[11]); // notR
            mapElementsPrefabsDictionary.Add("2202", mapElementsPrefabs[12]); // notB
            mapElementsPrefabsDictionary.Add("2220", mapElementsPrefabs[13]); // notL
            mapElementsPrefabsDictionary.Add("2222", mapElementsPrefabs[14]); // Base
            mapElementsPrefabsDictionary.Add("0000", mapElementsPrefabs[15]); // Block
        }        

        roomsPrefabsDictionary = new Dictionary<string, GameObject[]>();
        // Dictionary things
        {
            roomsPrefabsDictionary.Add("2000", T);     // T
            roomsPrefabsDictionary.Add("0200", R);     // R
            roomsPrefabsDictionary.Add("0020", B);     // B
            roomsPrefabsDictionary.Add("0002", L);     // L
            roomsPrefabsDictionary.Add("2200", TR);    // TR
            roomsPrefabsDictionary.Add("2020", TB);    // TB
            roomsPrefabsDictionary.Add("2002", TL);    // TL
            roomsPrefabsDictionary.Add("0220", RB);    // RB
            roomsPrefabsDictionary.Add("0202", RL);    // RL
            roomsPrefabsDictionary.Add("0022", BL);    // BL
            roomsPrefabsDictionary.Add("0222", notT); // notT
            roomsPrefabsDictionary.Add("2022", notR); // notR
            roomsPrefabsDictionary.Add("2202", notB); // notB
            roomsPrefabsDictionary.Add("2220", notL); // notL
            roomsPrefabsDictionary.Add("2222", Base); // Base
            roomsPrefabsDictionary.Add("0000", Block); // Block
        }

        GenerateDungeon();

        SpawnDungeonMap();

        SpawnDungeon();

    }

    private void GenerateDungeon() {

        // Генерирует подземелье, используя абстрактную матрицу

        for (int i = 0; i < rows; i++) {
            for (int j = 0; j < columns; j++) {

                PlaceForRoom point = roomDirectionsDataMatrix[i, j];

                // Mapping
                {

                    //

                    if ((i - 1 >= 0) && (roomDirectionsDataMatrix[i - 1, j] != null)) {
                        if (point == null) {
                            roomDirectionsDataMatrix[i, j] = new PlaceForRoom();
                            point = roomDirectionsDataMatrix[i, j];
                        }
                        point.setTop(roomDirectionsDataMatrix[i - 1, j].getDoorParams()[2]);
                    } else if (i - 1 < 0) {
                        if (point == null) {
                            roomDirectionsDataMatrix[i, j] = new PlaceForRoom();
                            point = roomDirectionsDataMatrix[i, j];
                        }
                        point.setTop(-1);
                    }
                    if ((j + 1 < columns) && (roomDirectionsDataMatrix[i, j + 1] != null)) {
                        if (point == null) {
                            roomDirectionsDataMatrix[i, j] = new PlaceForRoom();
                            point = roomDirectionsDataMatrix[i, j];
                        }
                        point.setRight(roomDirectionsDataMatrix[i, j + 1].getDoorParams()[3]);
                    } else if (j + 1 >= columns) {
                        if (point == null) {
                            roomDirectionsDataMatrix[i, j] = new PlaceForRoom();
                            point = roomDirectionsDataMatrix[i, j];
                        }
                        point.setRight(-1);
                    }
                    if ((i + 1 < rows) && (roomDirectionsDataMatrix[i + 1, j] != null)) {
                        if (point == null) {
                            roomDirectionsDataMatrix[i, j] = new PlaceForRoom();
                            point = roomDirectionsDataMatrix[i, j];
                        }
                        point.setBottom(roomDirectionsDataMatrix[i + 1, j].getDoorParams()[0]);
                    } else if (i + 1 >= rows) {
                        if (point == null) {
                            roomDirectionsDataMatrix[i, j] = new PlaceForRoom();
                            point = roomDirectionsDataMatrix[i, j];
                        }
                        point.setBottom(-1);
                    }
                    if ((j - 1 >= 0) && (roomDirectionsDataMatrix[i, j - 1] != null)) {
                        if (point == null) {
                            roomDirectionsDataMatrix[i, j] = new PlaceForRoom();
                            point = roomDirectionsDataMatrix[i, j];
                        }
                        point.setLeft(roomDirectionsDataMatrix[i, j - 1].getDoorParams()[1]);
                    } else if (j - 1 < 0) {
                        if (point == null) {
                            roomDirectionsDataMatrix[i, j] = new PlaceForRoom();
                            point = roomDirectionsDataMatrix[i, j];
                        }
                        point.setLeft(-1);
                    }
                }

                // Filling
                {
                    if (point != null) {
                        short[] doors = point.getDoorParams();
                        if (doors[0] == 0) {
                            float rd = Random.value;
                            if (rd <= 0.5f) {
                                point.setTop(-1);
                            } else if (rd > 0.5f) {
                                point.setTop(1);
                            }
                        }
                        if (doors[1] == 0) {
                            float rd = Random.value;
                            if (rd <= 0.5f) {
                                point.setRight(-1);
                            } else if (rd > 0.5f) {
                                point.setRight(1);
                            }
                        }
                        if (doors[2] == 0) {
                            float rd = Random.value;
                            if (rd <= 0.5f) {
                                point.setBottom(-1);
                            } else if (rd > 0.5f) {
                                point.setBottom(1);
                            }
                        }
                        if (doors[3] == 0) {
                            float rd = Random.value;
                            if (rd <= 0.5f) {
                                point.setLeft(-1);
                            } else if (rd > 0.5f) {
                                point.setLeft(1);
                            }
                        }
                    }
                }
            }
        }
    }

    private void SpawnDungeonMap() {

        // Спавнит прообразы комнаты, которые вместе формируют карту подземелья

        for (int i = 0; i < rows; i++) {
            for (int j = 0; j < columns; j++) {

                if (roomDirectionsDataMatrix[i, j] != null) {

                    short[] array = roomDirectionsDataMatrix[i, j].getDoorParams();
                    for (int k = 0; k < array.Length; k++) {
                        array[k] += 1;
                    }

                    string key = string.Concat<short>(array);

                    if (mapElementsPrefabsDictionary.ContainsKey(key) == false) {
                        continue;
                    }
                    else {
                        miniMapMatrix[i, j] = Instantiate(mapElementsPrefabsDictionary[key],
                            new Vector2(j * 10 - 300, -i * 10 + 300),
                            Quaternion.identity,
                            this.gameObject.transform.GetChild(1)
                            );
                    }
                }
            }
        }
    }

    private void SpawnDungeon() {

        // Спавнит комнаты и составляет из них готовое подземелье

        for (int i = 0; i < rows; i++) {
            for (int j = 0; j < columns; j++) {

                if (roomDirectionsDataMatrix[i, j] != null) {

                    short[] array = roomDirectionsDataMatrix[i, j].getDoorParams();
                    for (int k = 0; k < array.Length; k++) {
                        array[k] += 1;
                    }

                    string key = string.Concat<short>(array);

                    if (mapElementsPrefabsDictionary.ContainsKey(key) == false) {
                        continue;
                    } else {

                        roomsMatrix[i, j] = Instantiate(
                            roomsPrefabsDictionary[key][Random.Range(0, roomsPrefabsDictionary[key].Length)],
                            new Vector2(j * 250, -i * 200),
                            Quaternion.identity,
                            this.gameObject.transform.GetChild(0)
                            );
                        roomsMatrix[i, j].SetActive(false);

                        if ((i == rows / 2) && (j == columns / 2)) {
                            roomsMatrix[i, j].SetActive(true);

                        }
                    }
                }
            }
        }
    }

    public GameObject[,] GetRoomsMatrix() {
        
        return roomsMatrix;
    }

    public Vector2 getCurrentLocationAll() {

        currentRow = rows / 2;
        currentColumn = columns / 2;
        Vector2 result = new Vector2(currentColumn * 250, -currentRow * 200);
        return result;
    }
}