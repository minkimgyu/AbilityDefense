#if UNITY_EDITOR
using UnityEngine;
using System.Collections.Generic;

public class CardDataGenerator : BaseDataGenerator<CardData.Name>
{
    [ContextMenu("🔧 Generate Card Data")]
    public override void GenerateData()
    {
        // JsonDataSaver 인스턴스 생성
        JsonDataSaver jsonDataSaver = new JsonDataSaver();

        if (jsonDataSaver == null)
        {
            Debug.LogError("❌ JsonDataSaver가 연결되지 않았습니다.");
            return;
        }

        CardData cardData = default;

        switch (_dataName)
        {
            case CardData.Name.GunnerSpawnCard:

                // ✅ 명명된 인자(named arguments)를 사용한 카드 데이터 생성
                cardData = new CardData(
                    nameToSpawn: Entity.Name.Gunner,
                    areaData: AreaData.TwoXTwo,
                    cardName: "슈터",
                    cardDescription: "원거리에서 총을 난사하여 적에게 피해를 입히는 유닛입니다."
                );

                break;
            case CardData.Name.GuidedMissileTowerSpawnCard:

                cardData = new CardData(
                  nameToSpawn: Entity.Name.GuidedMissileTower,
                  areaData: AreaData.TwoXTwo,
                  cardName: "유도 미사일 포탑",
                  cardDescription: "유도 미사일을 발사하여 멀리 떨어진 적을 공격하는 타워입니다."
                );

                break;
            case CardData.Name.BulletTowerSpawnCard:

                cardData = new CardData(
                   nameToSpawn: Entity.Name.BulletTower,
                   areaData: AreaData.TwoXTwo,
                   cardName: "정밀 조준포",
                   cardDescription: "빠르게 탄환을 발사하여 단일 대상을 집중 공격하는 타워입니다."
                );

                break;
            case CardData.Name.GridMissileTowerSpawnCard:

                //cardData = new CardData(
                //    nameToSpawn: Entity.Name.GridMissileTower,
                //    areaData: AreaData.TwoXTwo,
                //    cardName: "미사일 포탑",
                //    cardDescription: "일정 범위에 미사일을 발사해 적을 쓸어버리는 타워입니다."
                //);

                break;
            case CardData.Name.ThrowTowerSpawnCard:

                //cardData = new CardData(
                //    nameToSpawn: Entity.Name.ThrowTower,
                //    areaData: AreaData.TwoXTwo,
                //    cardName: "투척 타워",
                //    cardDescription: "폭탄을 던져 강력한 광역 피해를 입히는 타워입니다."
                //);

                break;
            case CardData.Name.ProvocationWarriorSpawnCard:

                //cardData = new CardData(
                //    nameToSpawn: Entity.Name.ProvocationWarrior,
                //    areaData: AreaData.TwoXTwo,
                //    cardName: "도발 방패 전사",
                //    cardDescription: "적을 도발 능력을 가진 전사입니다."
                //);

                break;
            case CardData.Name.ProvocationGolemSpawnCard:

                //cardData = new CardData(
                //    nameToSpawn: Entity.Name.ProvocationGolem,
                //    areaData: AreaData.ThreeXThree,
                //    cardName: "도발 골렘",
                //    cardDescription: "적을 도발 능력을 가진 골렘입니다."
                //);

                break;
            case CardData.Name.SparkTowerSpawnCard:

                //cardData = new CardData(
                //    nameToSpawn: Entity.Name.SparkTower,
                //    areaData: AreaData.TwoXThree,
                //    cardName: "번개 방출기",
                //    cardDescription: "전기를 방출하여 주변의 여러 적에게 피해를 입히는 타워입니다."
                //);

                break;
            case CardData.Name.LaserTowerSpawnCard:

                //cardData = new CardData(
                //    nameToSpawn: Entity.Name.LaserTower,
                //    areaData: AreaData.ThreeXThree,
                //    cardName: "레이저 포탑",
                //    cardDescription: "강력한 레이저 빔으로 적을 관통하여 피해를 입히는 타워입니다."
                //);

                break;
            default:
                break;
        }

        // JSON 저장
        SaveToJson(cardData);

        Debug.Log("✅ 카드 데이터 생성 및 저장 완료");
    }
}
#endif
