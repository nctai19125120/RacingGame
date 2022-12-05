using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

// CLIENT
// - JOIN_ROOM username\0
// - ANSWER username number\0

// SERVER
// - INFO usernames[]\0
// - START_GAME\0 -> client handle roi chuyen vao phong choi
// - QUESTION question_id a operator b timestamp duration\0
// - RESULT correct_answer True/False point\0
// - RANKING usernames[] points[]\0
// - ELIMINATE\0
// - END\0

public class TestTCPClient : MonoBehaviour
{
    #region private members 	
	private TcpClient socketConnection; 	
	private Thread clientReceiveThread; 	
	#endregion  	
	// Use this for initialization 	
	void Start () {
		ConnectToTcpServer();
	}  	
	// Update is called once per frame
	void Update () {         
		if (Input.GetMouseButtonDown(0)) {             
			SendMessage();         
		}
	}  	
	/// <summary> 	
	/// Setup socket connection. 	
	/// </summary> 	
	private void ConnectToTcpServer () { 		
		try {  			
			clientReceiveThread = new Thread (new ThreadStart(ListenForData)); 			
			clientReceiveThread.IsBackground = true; 			
			clientReceiveThread.Start();  		
		} 		
		catch (Exception e) { 			
			Debug.Log("On client connect exception " + e); 		
		}
	}  	
	/// <summary> 	
	/// Runs in background clientReceiveThread; Listens for incomming data. 	
	/// </summary>     
	private void ListenForData() { 		
		try { 			
			socketConnection = new TcpClient("racing-arena.fly.dev", 6969);  			
			Byte[] bytes = new Byte[1024];             
			while (true) { 				
				// Get a stream object for reading 				
				using (NetworkStream stream = socketConnection.GetStream()) { 					
					int length; 					
					// Read incomming stream into byte arrary. 					
					while ((length = stream.Read(bytes, 0, bytes.Length)) != 0) { 						
						var incommingData = new byte[length]; 						
						Array.Copy(bytes, 0, incommingData, 0, length); 						
						// Convert byte array to string message. 						
						string serverMessage = Encoding.ASCII.GetString(incommingData); 						
						Debug.Log("server message received as: " + serverMessage); 					
					} 				
				} 			
			}         
		}         
		catch (SocketException socketException) {             
			Debug.Log("Socket exception: " + socketException);         
		}     
	}  	
	/// <summary> 	
	/// Send message to server using socket connection. 	
	/// </summary> 	
	private void SendMessage() {         
		if (socketConnection == null) {             
			return;         
		}  		
		try { 			
			// Get a stream object for writing. 			
			NetworkStream stream = socketConnection.GetStream(); 			
			if (stream.CanWrite) {                 
				string clientMessage = "JOIN_ROOM npvan"; 				
				// Convert string message to byte array.
				byte[] clientMessageAsByteArray = Encoding.ASCII.GetBytes(clientMessage + "\0"); 				
				// Write byte array to socketConnection stream.                 
				stream.Write(clientMessageAsByteArray, 0, clientMessageAsByteArray.Length);                 
				Debug.Log("Client sent his message - should be received by server");             
			}
            // byte[] bytes1 = new byte[1024];
            // string data = Encoding.ASCII.GetString(bytes1);
            // Debug.Log("Server sent message: " + data);
			// stream.Close();
            // socketConnection.Close();
        } 		
		catch (SocketException socketException) {             
			Debug.Log("Socket exception: " + socketException);         
		}     
	} 
}
