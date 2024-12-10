using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    private void Start()
    {
        GameManagement gm = GameManagement.inst;
        if (gm != null) gm.hideHearts();
    }
    public void clilcsound()
    {
        FindObjectOfType<AudioManager>().Play("Select");
    }
    public void resume()
    {
        PauseMenu pm = FindObjectOfType<PauseMenu>();
        if (pm != null) pm.setCanPause(true);
        pm.resumeGame();
    }

}
