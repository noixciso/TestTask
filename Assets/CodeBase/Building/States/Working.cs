using CodeBase.Items;
using UnityEngine;

namespace CodeBase.Building.States
{
    public class Working : IBuildingState
    {
        private Building _building;
        private Coroutine _spawnCoroutine;

        public Working(Building building) =>
            _building = building;

        public void Enter() =>
            _spawnCoroutine = _building.StartCoroutine(_building.SpawnItems());

        public void Exit() =>
            _building.StopCoroutine(_spawnCoroutine);

        public void Update()
        {
            if (_building.ProduceItemTypeId == ItemTypeId.Bronze)
            {
                if (_building.Produced.IsProducedWarehouseFull(_building.BuildingStaticData))
                {
                    _building.SetReasonForStopping(ReasonForStopping.WarehouseIsFull);
                    _building.Enter<Suspended>();
                }
            }

            if (_building.ProduceItemTypeId == ItemTypeId.Silver || _building.ProduceItemTypeId == ItemTypeId.Gold)
            {
                if (IsProducedWarehouseFull())
                {
                    _building.SetReasonForStopping(ReasonForStopping.WarehouseIsFull);
                    _building.Enter<Suspended>();
                }
                else if (_building.Consumed.IsProductionPossible(_building.BuildingStaticData) == false)
                {
                    _building.SetReasonForStopping(ReasonForStopping.NotEnoughResources);
                    _building.Enter<Suspended>();
                }
            }
        }

        private bool IsProducedWarehouseFull()
        {
            return _building.Consumed.IsProductionPossible(_building.BuildingStaticData) ||
                   !_building.Consumed.IsProductionPossible(_building.BuildingStaticData)
                   && _building.Produced.IsProducedWarehouseFull(_building.BuildingStaticData);
        }
    }
}