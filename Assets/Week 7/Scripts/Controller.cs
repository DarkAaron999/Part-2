using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
   public static FootballPlayer SelectedPlayer { get; private set; }

    public static void SetSelectedPlayer(FootballPlayer player)
    {
        if (SelectedPlayer != null)
        {
            SelectedPlayer.Selected(false);
        }
        SelectedPlayer = player;
        SelectedPlayer.Selected(true);
    }
}
