using System;
using System.Collections;
using System.Collections.Generic;
using CodeBase.Building.States;
using CodeBase.Items;
using CodeBase.StaticData;
using CodeBase.UI;
using CodeBase.Warehouse;
using UnityEngine;

namespace CodeBase.Building
{
    [RequireComponent(typeof(ItemSpawner))]
    public class Building : MonoBehaviour
    {
        public BuildingStaticData BuildingStaticData;
        public ItemTypeId ProduceItemTypeId;

        private Dictionary<Type, IBuildingState> _states;
        private IBuildingState _activeState;

        public BuildingUI BuildingUI { get; private set; }
        public ProducedWarehouse Produced { get; private set; }
        public ConsumedWarehouse Consumed { get; private set; }
        public ItemSpawner Spawner { get; private set; }
        public ReasonForStopping CurrentReasonForStopping { get; private set; }

        private void Awake()
        {
            _states = new Dictionary<Type, IBuildingState>()
            {
                [typeof(Working)] = new Working(this),
                [typeof(Suspended)] = new Suspended(this)
            };
        }

        private void Start()
        {
            BuildingUI = GetComponent<BuildingUI>();
            Produced = GetComponentInChildren<ProducedWarehouse>();
            Consumed = GetComponentInChildren<ConsumedWarehouse>();
            Spawner = GetComponent<ItemSpawner>();
            
            if (ProduceItemTypeId == ItemTypeId.Bronze)
            {
                Enter<Working>();
            }
            else
            {
                Enter<Suspended>();
            }
        }

        private void Update() =>
            _activeState.Update();

        public void Enter<TState>() where TState : class, IBuildingState
        {
            TState state = ChangeState<TState>();
            state.Enter();
        }

        public IEnumerator SpawnItems()
        {
            while (true)
            {
                Item item = Spawner.Spawn();
                Produced.AddItemToProduced(item);
                yield return new WaitForSeconds(0.5f);
            }
        }

        public void SetReasonForStopping(ReasonForStopping reasonForStopping) => 
            CurrentReasonForStopping = reasonForStopping;

        private TState ChangeState<TState>() where TState : class, IBuildingState
        {
            _activeState?.Exit();
            TState state = GetState<TState>();
            _activeState = state;
            return state;
        }

        private TState GetState<TState>() where TState : class, IBuildingState =>
            _states[typeof(TState)] as TState;
    }
}