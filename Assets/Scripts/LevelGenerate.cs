using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelGenerate : MonoBehaviour {

	public GameObject player;

	private float HORIZONTAL_SEPARATION = 50f;
	private float VERTICAL_SEPARATION = 35f;

	private int numRooms;

	Queue<string> roomList;

	void Awake()
	{
		roomList = new Queue<string>();
		roomList.Enqueue("caveRoom1");
		roomList.Enqueue("caveRoom2");
		roomList.Enqueue("caveRoom3");
		roomList.Enqueue("caveRoom12");
		roomList.Enqueue("caveRoom11");
		roomList.Enqueue("caveRoom14");
		roomList.Enqueue("TeleRoom");
		roomList.Enqueue("caveRoom13");
		roomList.Enqueue("caveRoom9");
		roomList.Enqueue("caveRoom8");
		roomList.Enqueue("caveRoom7");
		roomList.Enqueue("caveRoom10");
		roomList.Enqueue("caveRoom4");

		Application.LoadLevelAdditive("storeRoom");
		Application.LoadLevelAdditive("originRoom");
		Invoke("Other", 1f);
	}

	void Other()
	{
		GameObject room = GameObject.Find("storeRoom");
		room.transform.position += Vector3.up*VERTICAL_SEPARATION;
		Instantiate(player, new Vector3(0f, 0f, 0f), room.transform.rotation);
		GameObject newPlayer = GameObject.FindWithTag("Player");
		newPlayer.transform.position = room.transform.position;
		Door[] totallyNotDoors = room.GetComponentsInChildren<Door>();
		room = GameObject.Find("originRoom");
		foreach(Door door in totallyNotDoors)
		{
			Teleporter[] teleporter = room.GetComponentsInChildren<Teleporter>();
			
			door.pairExit = teleporter[0].transform;
		}

		Dictionary<string, Door> doors = new Dictionary<string, Door>();
		totallyNotDoors = room.GetComponentsInChildren<Door>();

		foreach (Door door in totallyNotDoors)
		{
			doors.Add(door.name, door);
		}

		Door currentDoor;
		doors.TryGetValue("WestDoor", out currentDoor);
		
		StartCoroutine(Generate(currentDoor, room.transform.position, "WestDoor"));
	}

	IEnumerator Generate(Door pastDoor, Vector3 pastPosition, string pastSide)
	{
		if (numRooms >= 3) yield break;
		numRooms ++;
		Application.LoadLevelAdditive( roomList.Dequeue() );
		yield return new WaitForSeconds(1f);
		GameObject newLevel = GameObject.FindWithTag("UnplacedLevel");
		newLevel.tag = "CaveLevel";

		Dictionary<string, Door> doors = new Dictionary<string, Door>();
		Door[] totallyNotDoors = newLevel.GetComponentsInChildren<Door>();

		foreach (Door door in totallyNotDoors)
		{
			doors.Add(door.name, door);
		}

		Door doorStorage = null;

		switch (pastSide)
		{
			case "NorthDoor":
			{
				pastPosition.y += VERTICAL_SEPARATION;
				doors.TryGetValue("SouthDoor", out doorStorage);
				break;
			}
			case "SouthDoor":
			{
				pastPosition.y -= VERTICAL_SEPARATION;
				doors.TryGetValue("NorthDoor", out doorStorage);
				break;
			}
			case "EastDoor":
			{
				pastPosition.x += HORIZONTAL_SEPARATION;
				doors.TryGetValue("WestDoor", out doorStorage);
				break;
			}
			case "WestDoor":
			{
				pastPosition.x -= HORIZONTAL_SEPARATION;
				doors.TryGetValue("EastDoor", out doorStorage);
				break;
			}
		}
		newLevel.transform.position = pastPosition;
		//doorStorage.pairExit = pastDoor.thisExit;
		//pastDoor.pairExit = doorStorage.thisExit;

		doors.TryGetValue("WestDoor", out doorStorage);
		if (doorStorage != null)
			StartCoroutine(Generate(doorStorage, pastPosition, "WestDoor"));

		doors.TryGetValue("SouthDoor", out doorStorage);
		if (doorStorage != null)
			StartCoroutine(Generate(doorStorage, pastPosition, "SouthDoor"));

		doors.TryGetValue("NorthDoor", out doorStorage);
		if (doorStorage != null)
			StartCoroutine(Generate(doorStorage, pastPosition, "NorthDoor"));
	}
}
