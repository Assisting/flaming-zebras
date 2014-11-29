using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelGenerate : MonoBehaviour {

	public GameObject player1; //prefabs of players to spwan
	public GameObject player2;

	private float HORIZONTAL_SEPARATION = 50f; //the distances between rooms
	private float VERTICAL_SEPARATION = 35f;

	private int numRooms;

	Queue<string> roomList; //alpha pregenerated room list

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

		Application.LoadLevelAdditive("storeRoom"); //spawn store
		Application.LoadLevelAdditive("originRoom"); //spawn first room
		StartCoroutine(Other()); //so we can wait in code
	}

	IEnumerator Other()
	{
		yield return new WaitForSeconds(0.5f);
		GameObject room = GameObject.Find("storeRoom");
		room.tag = "CaveLevel";
		room.transform.position += Vector3.up*VERTICAL_SEPARATION; //put store in place
		GameObject storeRoom = room; //store for later
		
		Door[] totallyNotDoors = room.GetComponentsInChildren<Door>(); //map store doors to the origin teleporter
		room = GameObject.Find("originRoom");
		room.tag = "CaveLevel";
		foreach(Door door in totallyNotDoors)
		{
			Teleporter[] teleporter = room.GetComponentsInChildren<Teleporter>();
			
			door.pairExit = teleporter[0].transform;
		}

		Dictionary<string, Door> doors = new Dictionary<string, Door>();
		totallyNotDoors = room.GetComponentsInChildren<Door>();

		foreach (Door door in totallyNotDoors)
		{
			doors.Add(door.name, door); //populate list of oridin doors (for recursion)
		}

		Door currentDoor;
		doors.TryGetValue("WestDoor", out currentDoor);
		
		yield return StartCoroutine(Generate(currentDoor, room.transform.position, "WestDoor")); //start recursive generation

		Instantiate(player1, new Vector3(0f, 0f, 0f), room.transform.rotation); //spawn player 1
		yield return new WaitForSeconds(0.05f);
		Instantiate(player2, new Vector3(0f, 0f, 0f), room.transform.rotation); //spawn player 2

		GameObject[] newPlayers = GameObject.FindGameObjectsWithTag("Player");
		foreach (GameObject player in newPlayers)
		{
			player.transform.position = storeRoom.transform.position; //move all players to shop
			player.transform.Find("Camera").GetComponent<CameraShift>().UpdateViewport(); //separate players into correct number of quadrants
		}
	}

	// recursive generation function, spawns levels from a predefined list currently
	IEnumerator Generate(Door pastDoor, Vector3 pastPosition, string pastSide)
	{
		Application.LoadLevelAdditive( roomList.Dequeue() ); //spawn next level
		yield return new WaitForSeconds(0.1f); //wait for it to happen
		GameObject newLevel = GameObject.FindWithTag("UnplacedLevel"); //get the level that just spawned
		newLevel.tag = "CaveLevel"; //only one level tagged unplaced at a time

		Dictionary<string, Door> doors = new Dictionary<string, Door>();
		Door[] totallyNotDoors = newLevel.GetComponentsInChildren<Door>(); //list of easily indexed doors

		foreach (Door door in totallyNotDoors)
		{
			doors.Add(door.name, door);
		}

		Door doorStorage = null;

		switch (pastSide) //for setting up direction of door linking and layout positioning
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
		doorStorage.pairExit = pastDoor.thisExit;
		pastDoor.pairExit = doorStorage.thisExit;


		//wait until the chain is done before recursing down new branches
		doors.TryGetValue("WestDoor", out doorStorage);
		if (doorStorage != null && pastSide != "EastDoor")
		{
			yield return StartCoroutine(Generate(doorStorage, pastPosition, "WestDoor"));
		}

		doors.TryGetValue("SouthDoor", out doorStorage);
		if (doorStorage != null && pastSide != "NorthDoor")
		{
			yield return StartCoroutine(Generate(doorStorage, pastPosition, "SouthDoor"));	
		}

		doors.TryGetValue("NorthDoor", out doorStorage);
		if (doorStorage != null && pastSide != "SouthDoor")
		{
			yield return StartCoroutine(Generate(doorStorage, pastPosition, "NorthDoor"));	
		}
	}
}
