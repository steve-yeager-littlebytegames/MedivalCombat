using System.Threading;
using MedivalCombat.Global;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Thread gameThread;

    private void Start()
    {
        Debug.Log("Starting game");
        Game.UpdateEvent += GameOnUpdate;
        gameThread = new Thread(Game.Start);
        gameThread.Start();
    }

    private void OnDestroy()
    {
        Game.End();
        //gameThread.Abort();
    }

    private void GameOnUpdate()
    {
        Debug.Log("Frame: " + Game.FrameCount);
        foreach (var entity in Game.entities)
        {
            Debug.Log(entity);
        }
    }
}