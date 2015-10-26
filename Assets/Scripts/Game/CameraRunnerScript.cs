using UnityEngine;
using System.Collections;

public class CameraRunnerScript : MonoBehaviour {

	public Transform player;
	//private int count = 0;

	void Update () {
	
		//transform.position = new Vector3 (player.position.x + 6, 0, -10);
		transform.position = new Vector3 (player.position.x + 6, 0, -10);
	
	}
	
	/*
	private void OnPostRender() {
		count++;

		if (count > 100) {
			Texture2D tex = new Texture2D(640, 480, TextureFormat.RGB24, false);
			
			// Read screen contents into the texture
			tex.ReadPixels(new Rect(0, 0, 640, 480), 0, 0);
			tex.Apply();

			// Encode texture into PNG
			byte[] bytes = tex.EncodeToPNG();
			Object.Destroy(tex);

			System.IO.File.WriteAllBytes(Application.dataPath + "/../SavedScreen.png", bytes);

			count = 0;

		}

	}
	*/

}
