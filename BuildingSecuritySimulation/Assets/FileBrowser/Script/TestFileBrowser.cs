using UnityEngine;
using System.Collections;

public class TestFileBrowser : MonoBehaviour {
	//skins and textures
	public GUISkin[] skins;
	public Texture2D file,folder,back,drive;
	
	//initialize file browser
	FileBrowser fb = new FileBrowser();
	string output = "no file";
	// Use this for initialization
	void Start () {
		//setup file browser style
		fb.guiSkin = skins[0]; //set the starting skin
		//set the various textures
		fb.fileTexture = file; 
		fb.directoryTexture = folder;
		fb.backTexture = back;
		fb.driveTexture = drive;
		//show the search bar
		fb.showSearch = true;
		//search recursively (setting recursive search may cause a long delay)
		fb.searchRecursively = true;
	}
	
	void OnGUI(){

		if(fb.draw()){ //true is returned when a file has been selected
			//the output file is a member if the FileInfo class, if cancel was selected the value is null
			output = (fb.outputFile==null)?"cancel hit":fb.outputFile.ToString();
		}
	}
}
