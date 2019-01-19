using System.Collections;
using System.Collections.Generic;
using UnityEngine; // Imports

public class CordinateHandler : MonoBehaviour {

	// Use this for initialization
	public int Cordx; // variable declaration
	public int Cordy;
	public DatabaseHandler DatabaseHandler; // imports the database handler script
	public MapMarkerHandler MapMarkerHandler;
	public PhysicalConnectionHandler PhysicalConnectionHandler1;
	public PhysicalConnectionHandler PhysicalConnectionHandler2;
	public PhysicalConnectionHandler PhysicalConnectionHandler3;
	public PhysicalConnectionHandler PhysicalConnectionHandler4;

	public LoadNewScene LoadNewScene;
	void Start () {
			Cordx = 0; // assigns values to zero
			Cordy = 0;
		}
	
	// Update is called once per frame
	void Update () {
		if ( Input.GetKeyDown(KeyCode.L) == true && Cordx < 3){ // detects inputs using ijkl instead of wasd
			Cordx +=1; // changes the variable
			DatabaseHandler.ReSelect();  // runs the function in database handler that updates the objects.
			MapMarkerHandler.UpdatePosition();
			/* PhysicalConnectionHandler1.DrawConnections();
			PhysicalConnectionHandler2.DrawConnections();
			PhysicalConnectionHandler3.DrawConnections();
			PhysicalConnectionHandler4.DrawConnections();
			*/
		}
		if ( Input.GetKeyDown(KeyCode.J) == true && Cordx > 0){ // detects inputs using ijkl instead of wasd
			Cordx -=1;// changes the variable
			
			DatabaseHandler.ReSelect();  // runs the function in database handler that updates the objects.
			MapMarkerHandler.UpdatePosition();
			/*
			PhysicalConnectionHandler1.DrawConnections();
			PhysicalConnectionHandler2.DrawConnections();
			PhysicalConnectionHandler3.DrawConnections();
			PhysicalConnectionHandler4.DrawConnections();
			*/
			
			
		}
		if ( Input.GetKeyDown(KeyCode.I) == true && Cordy > 0){ // detects inputs using ijkl instead of wasd
			Cordy -=1;// changes the variable
			DatabaseHandler.ReSelect();  // runs the function in database handler that updates the objects.
			MapMarkerHandler.UpdatePosition();
			/*
			PhysicalConnectionHandler1.DrawConnections();
			PhysicalConnectionHandler2.DrawConnections();
			PhysicalConnectionHandler3.DrawConnections();
			PhysicalConnectionHandler4.DrawConnections();
			*/
			
			
		}		
		if ( Input.GetKeyDown(KeyCode.K) == true && Cordy < 3){ // detects inputs using ijkl instead of wasd
			Cordy +=1;// changes the variable
			DatabaseHandler.ReSelect();  // runs the function in database handler that updates the objects.
			MapMarkerHandler.UpdatePosition();
			/*
			PhysicalConnectionHandler1.DrawConnections();
			PhysicalConnectionHandler2.DrawConnections();
			PhysicalConnectionHandler3.DrawConnections();
			PhysicalConnectionHandler4.DrawConnections();
			*/			
			
		}
		if (Input.GetKeyDown(KeyCode.Escape) == true){
			LoadNewScene.LoadSceneByIndex(0);
		}

	}
}
