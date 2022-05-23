using UnityEngine;
using Main;

public class Engine : MonoBehaviour
{
    public GameObject particlePrefab;
    public GameObject boxPrefab;
    public Game game;
    
    void Start()
    {
        this.game = new Game(particlePrefab, boxPrefab);
        this.game.start();
    }

    void Update()
    {
        this.game.update();
    }
}
