using CodeBase.Infrastructure.Services;
using CodeBase.Items;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
    public interface IGameFactory : IService
    {
        void CreateHud();
        GameObject CreatePlayer(GameObject at);
        Item CreateItem(Item item, Transform parent);
    }
}