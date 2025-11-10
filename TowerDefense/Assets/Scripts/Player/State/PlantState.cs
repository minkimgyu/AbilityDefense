using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlantState : BaseState<PlayerController.State>
    {
        EntityFactory _entityFactory;

        public PlantState(FSM<PlayerController.State> fsm, EntityFactory entityFactory) : base(fsm)
        {
            _entityFactory = entityFactory;
        }

        public override void OnStateEnter(CardData data, Vector3 resultPos)
        {
            // TODO
            // 심기면 이벤트 호출해서 실제 오브젝트 활성화시키기
            // 추가로 Grid Pathfinder 재계산 해주기

            Entity entity = _entityFactory.Create(data.NameToSpawn);
            entity.SetPosition(resultPos);

            Debug.Log("심기");
            _fsm.SetState(PlayerController.State.Idle);
        }
    }
}