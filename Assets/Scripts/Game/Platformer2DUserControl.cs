
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;


namespace UnityStandardAssets._2D
{
	[RequireComponent(typeof (PlatformerCharacter2D))]
	public class Platformer2DUserControl : MonoBehaviour
	{
		private PlatformerCharacter2D m_Character;
		private bool m_Jump;
		private Texture2D tex;
		private int counter = 0;

		private int picture_counter = 0;
		
		
		private void Awake()
		{
			m_Character = GetComponent<PlatformerCharacter2D>();
			tex = new Texture2D(500, 200, TextureFormat.RGB24, false);
		}
		
		
		private void Update()
		{
			if (!m_Jump)
			{
				// Read the jump input in Update so button presses aren't missed.
				m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
				//m_Jump = true;

			}

			StartCoroutine (captureScreen());
		}

		private IEnumerator captureScreen(){
			yield return new WaitForEndOfFrame();
			
			// Read screen contents into the texture
			tex.ReadPixels(new Rect(0, 70, 500, 270), 0, 0);
			
			tex.Apply ();

			//byte[] bytes = tex.EncodeToPNG();
			//System.IO.File.WriteAllBytes(Application.dataPath + "/../Images/SavedScreen" + this.picture_counter + ".png", bytes);
			//this.picture_counter++;

			/*
			Color test = new Color (0.247F, 0.710F, 0.851F, 1.000F);
			if (test.ToString () == bv.GetValue (86600).ToString ()) {
				m_Character.Move(1,false,true);
				//Debug.Log ("HEY HEY HEY HEY HEY HEY HEY HEY!!!!!");
			}
			*/
			
		

			//Debug.Log (test.ToString() + "  Bnana");
			//Debug.Log (bv.GetValue (86500).ToString());
		}

		
		
		private void FixedUpdate()
		{
			// TODO: Move this to captureScreen, so it never makes a move until a new screen has been saved

			Population pop = PopulationControlScript.populationControl.pop;
			//Debug.Log (pop.getGenome(0).nodes[0][7600].oldValue);

			counter++;
			//decide ();


			// Read the inputs.
			//bool crouch = Input.GetKey(KeyCode.LeftControl);
			//float h = CrossPlatformInputManager.GetAxis("Horizontal");
			// Pass all parameters to the character control script.
			m_Character.Move(1, false, m_Jump);
			m_Jump = false;
		}

		private void decide(){
			if (counter % 10 == 0) {
				Color[] bv = tex.GetPixels();
				
				List<double> output = BrainControlScript.brainControl.genome.sendThroughNetwork (bv);


				if (output[0] > 0.5) {
					m_Jump = true;
					Debug.Log ("JUMP");
				} 
				else {
					Debug.Log ("run");
				}



				//m_Jump = true;

			}
		}
	}
	
}



/*
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets._2D
{
	
	[RequireComponent(typeof (PlatformerCharacter2D))]
	public class Platformer2DUserControl : MonoBehaviour
	{
		private PlatformerCharacter2D character;
		private bool jump;
		
		public Texture2D sourceTex;
		
		private void Awake()
		{
			character = GetComponent<PlatformerCharacter2D>();
		}
		
		private void Update()
		{
			if (!jump) {
				// Read the jump input in Update so button presses aren't missed.
				jump = CrossPlatformInputManager.GetButtonDown ("Jump");
			}
		}
		
		private void FixedUpdate()
		{
			// Read the inputs.
			//bool crouch = Input.GetKey(KeyCode.LeftControl);
			//float h = CrossPlatformInputManager.GetAxis("Horizontal");
			// Pass all parameters to the character control script.
			
			character.Move(1, false, jump);
			jump = true;
			
		}
		
	}
}
*/