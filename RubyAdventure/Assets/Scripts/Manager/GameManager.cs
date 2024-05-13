using UnityEngine;

public class GameManager : GenericSingleton<GameManager>
{
    public RubyController Ruby => ruby;
    [SerializeField] private RubyController ruby;
    [SerializeField] private LevelController levelController;
    public LevelController LevelController => levelController;
}