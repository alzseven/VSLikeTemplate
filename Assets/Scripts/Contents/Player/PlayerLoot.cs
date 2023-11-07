using Contents.Manager;
using UnityEngine;

//TODO: Rename
namespace Contents.Player
{
    public class PlayerLoot : MonoBehaviour
    {
        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.CompareTag("Lootable"))
            {
                //TODO: Should be interface or abstract class
                if (other.TryGetComponent<ExpInstance>(out var exp)) 
                    exp.SetTarget(GameManager.Instance.playerRb);
            }
        }
    }
}
