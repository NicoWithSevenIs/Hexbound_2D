using UnityEngine;

public partial class CharacterController : MonoBehaviour
{

    private Rigidbody2D body;
    private CharacterInstance ch;

    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
        ch = GetComponent<CharacterInstance>();
    }

    private void FixedUpdate()
    {
        var move_spd = ch.CurrentStats.BasicStats.MOV_SPD;
        body.AddForce(new Vector2(movement_axis * move_spd, 0));
    }

}

#region Input Receiver
public partial class CharacterController
{
    private float movement_axis = 0f;

    public void SetMovementAxis(float dir)
    {
        movement_axis = dir;
    }
}
#endregion