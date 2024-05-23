using UnityEngine;

public class GameManager : GenericSingleton<GameManager>
{
    public RubyController Ruby => ruby;
    [SerializeField] private RubyController ruby;

    public Vector3 RubyPosition => new(ruby.transform.position.x, ruby.transform.position.y + 0.5f, ruby.transform.position.z);
}