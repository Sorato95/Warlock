using UnityEngine;
using System.Collections;

public class Shop : MonoBehaviour
{
    void OnTriggerEnter(Collider collider)
    {
        PlayerController shoppingPlayer = collider.gameObject.GetComponent<PlayerController>();

        if (shoppingPlayer != null)
        {
            DebugConsole.Log("shopping player: " + shoppingPlayer.netId);
            displayShopUI(shoppingPlayer);
        }
    }

    public void OnTriggerExit(Collider collider)
    {
        PlayerController shoppingPlayer = collider.gameObject.GetComponent<PlayerController>();

        if (shoppingPlayer != null)
        {
            DebugConsole.Log("player " + shoppingPlayer.netId + " stopped shopping");
            removeShopUI(shoppingPlayer);
        }
    }

    private void displayShopUI(PlayerController player)
    {

    }

    private void removeShopUI(PlayerController player)
    {

    }
}
