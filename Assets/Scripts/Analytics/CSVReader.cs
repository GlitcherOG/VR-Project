/*
	CSVReader by Dock. (24/8/11)
	http://starfruitgames.com
 
	usage: 
	CSVReader.SplitCsvGrid(textString)
 
	returns a 2D string array. 
 
	Drag onto a gameobject for a demo of CSV parsing.
*/

using UnityEngine;
using System.Collections;
using System.Linq;
using System.IO;

using TMPro;

public class CSVReader : MonoBehaviour
{
  public string fileName;
  
  public TextMeshProUGUI forText, byText;

  public void Start() => ReadtoTitle(fileName);

  static public void CSVtoDict(string _csv)
  {
    var dict = File.ReadLines(Path.Combine(Application.persistentDataPath, _csv)).Select(line => line.Split(',')).ToDictionary(line => line[0], line => line[1]);

    for (int i = 0; i < dict.Count; i++)
      print($"Key: {dict.ElementAt(i).Key}, Value: {dict.ElementAt(i).Value}");
  }

  public void ReadtoTitle(string _csv)
  {
    var dict = File.ReadLines(Path.Combine(Application.dataPath, _csv)).Select(line => line.Split(',')).ToDictionary(line => line[0], line => line[1]);

    for (int i = 0; i < dict.Count; i++)
      print($"Key: {dict.ElementAt(i).Key}, Value: {dict.ElementAt(i).Value}");

    forText.SetText("for " + dict["Patient"].ToString());
    byText.SetText("by " + dict["CompanyName"].ToString());
  }
}