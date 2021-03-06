using System.Collections;
using System.Collections.Generic;

using UnityEngine;

///<summary>
///Send EventLog to all clients and show them
///</summary>
[BoltGlobalBehaviour()]
public class NetworkCallback : Bolt.GlobalEventListener
{
  private List<string> logMsgs = new List<string>();

  public override void OnEvent(EventLog e)
  {
    logMsgs.Insert(0, e.Message);
  }

  /*private void OnGUI()
  {
    int maxMsgs = Mathf.Min(5, logMsgs.Count);

    GUILayout.BeginArea(new Rect(Screen.width / 2 - 200, Screen.height - 100, 400, 100), GUI.skin.box);
    for (int i = 0; i < maxMsgs; i++)
      GUILayout.Label(logMsgs[i]);
    GUILayout.EndArea();
  }*/
}
