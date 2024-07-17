using System;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "NewRoomConfig", menuName = "ScriptableObjects/ModularRooms/Room Config", order = 1)]
public class RoomConfig : ScriptableObject
{
    [SerializeField] private int easyDiffPoints;
    [SerializeField] private int hardDiffPoints;
    
    public int GetEasyDiffPoints => easyDiffPoints;
    public int GetHardDiffPoints => hardDiffPoints;
}
