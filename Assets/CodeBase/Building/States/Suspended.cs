using CodeBase.Items;

namespace CodeBase.Building.States
{
    public class Suspended : IBuildingState
    {
        private readonly Building _building;

        public Suspended(Building building) =>
            _building = building;

        public void Enter()
        {
            _building.BuildingUI.ShowText();
            _building.BuildingUI.SetText(_building.CurrentReasonForStopping);
        }

        public void Exit() =>
            _building.BuildingUI.HideText();

        public void Update()
        {
            if (_building.ProduceItemTypeId == ItemTypeId.Bronze)
            {
                if (!_building.Produced.IsProducedWarehouseFull(_building.BuildingStaticData))
                {
                    _building.Enter<Working>();
                }
            }

            if (_building.ProduceItemTypeId == ItemTypeId.Silver || _building.ProduceItemTypeId == ItemTypeId.Gold)
            {
                if (!_building.Consumed.WarehouseTrigger.IsPlayerNotInConsumedWarehouse)
                {
                    _building.BuildingUI.HideText();
                }
                else
                {
                    _building.BuildingUI.ShowText();
                }
                
                if (!_building.Produced.IsProducedWarehouseFull(_building.BuildingStaticData))
                {
                    if (_building.Consumed.IsProductionPossible(_building.BuildingStaticData))
                    {
                        _building.Consumed.RemoveRequiredItems(_building.BuildingStaticData);
                        _building.Enter<Working>();
                    }
                    else
                    {
                        _building.BuildingUI.SetText(ReasonForStopping.NotEnoughResources);
                    }
                }
            }
        }
    }
}