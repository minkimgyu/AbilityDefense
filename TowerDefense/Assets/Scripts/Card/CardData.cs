using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public struct CardData
{
    // 카드 이름
    [SerializeField]
    public enum Name
    {
        GunnerSpawnCard,
        GridMissileTowerSpawnCard,
        BulletTowerSpawnCard,
        GuidedMissileTowerSpawnCard,
        ThrowTowerSpawnCard,
        ProvocationWarriorSpawnCard,
        ProvocationGolemSpawnCard,
        SparkTowerSpawnCard,
        LaserTowerSpawnCard
    }

    [JsonProperty("nameToSpawn")] Entity.Name _nameToSpawn; // 스폰시킬 Entity 이름
    [JsonProperty("areaData")] AreaData _areaData; // 스폰시킬 영역 크기

    [JsonProperty("cardName")] string _cardName; // 카드 이름
    [JsonProperty("cardDescription")] string _cardDescription; // 카드 설명

    [JsonIgnore] public Entity.Name NameToSpawn { get => _nameToSpawn; set => _nameToSpawn = value; }
    [JsonIgnore] public AreaData AreaData { get => _areaData; set => _areaData = value; }
    [JsonIgnore] public string CardName { get => _cardName; set => _cardName = value; }
    [JsonIgnore] public string CardDescription { get => _cardDescription; set => _cardDescription = value; }

    public CardData(
        Entity.Name nameToSpawn,
        AreaData areaData,
        string cardName,
        string cardDescription)
    {
        _nameToSpawn = nameToSpawn;
        _areaData = areaData;
        _cardName = cardName;
        _cardDescription = cardDescription;
    }
}
