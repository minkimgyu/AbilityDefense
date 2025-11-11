#if UNITY_EDITOR
using UnityEngine;
using System.Collections.Generic;

public class EntityDataGenerator : BaseDataGenerator<Entity.Name>
{
    [ContextMenu("🔧 Generate Entity Data")]
    public override void GenerateData()
    {
        //  자동으로 JsonDataSaver 컴포넌트 추가
        JsonDataSaver jsonDataSaver = new JsonDataSaver();

        if (jsonDataSaver == null)
        {
            Debug.LogError("❌ JsonDataSaver가 연결되지 않았습니다.");
            return;
        }

        EntityData entityData = null;

        switch (_dataName)
        {
            case Entity.Name.Gunner:

               entityData = new GunnerData
               (
                   name: Entity.Name.Gunner,
                   myType: ITarget.Type.Ally,
                   targetTypes: new List<ITarget.Type> { ITarget.Type.Enemy },
                   projectileName: IProjectile.Name.Bullet,
                   attackDamage: new BuffValue<float>(0f, 5f, 100f),
                   targetingRange: new BuffValue<float>(0f, 2f, 100f),
                   attackRate: new BuffValue<float>(0f, 1.2f, 100f),
                   rotationSpeed: 25f
               );

                break;
            case Entity.Name.BulletTower:

                entityData = new BulletTowerData
                (
                    name: Entity.Name.BulletTower,
                    myType: ITarget.Type.Ally,
                    targetTypes: new List<ITarget.Type> { ITarget.Type.Enemy },
                    projectileName: IProjectile.Name.Bullet,
                    attackDamage: new BuffValue<float>(0f, 8f, 100f),
                    targetingRange: new BuffValue<float>(0f, 4f, 100f),
                    attackRate: new BuffValue<float>(0f, 1.5f, 100f),
                   rotationSpeed: 25f
                );

                break;
            case Entity.Name.GuidedMissileTower:

                entityData = new GuidedMissileTowerData
                (
                    name: Entity.Name.GuidedMissileTower,
                    myType: ITarget.Type.Ally,
                    targetTypes: new List<ITarget.Type> { ITarget.Type.Enemy },
                    projectileName: IProjectile.Name.Missile,
                    explosionDamage: new BuffValue<float>(0f, 18f, 100f),
                    explosionRange: new BuffValue<float>(0f, 2f, 100f),
                    targetingRange: new BuffValue<float>(0f, 4f, 100f),
                    attackRate: new BuffValue<float>(0f, 1f, 100f),
                   rotationSpeed: 25f
                );

                break;

            case Entity.Name.Imp:
                entityData = new WalkUnitData
                (
                    name: Entity.Name.Imp,
                    myType: ITarget.Type.Enemy,
                    maxHp: new BuffValue<float>(0f, 30f, 100f),
                    moveSpeed: new BuffValue<float>(0f, 5f, 100f),
                    rotationSpeed: 25f
                );
                break;

            default:
                break;
        }

        // Json 저장
        SaveToJson(entityData);

        Debug.Log("✅ Entity 데이터 생성 및 저장 완료");
    }
}

#endif