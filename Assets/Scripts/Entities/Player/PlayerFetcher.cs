using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[DisallowMultipleComponent]
public class PlayerFetcher : MonoBehaviour, IPlayerFetcher
{
    [SerializeField] GameObject player;
    public Transform FetchPlayer()
    {
        return player.transform;
    }
}
