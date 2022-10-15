using System;
using System.Collections.Generic;
using Zomlypse.Behaviours;

namespace Zomlypse.Singleton
{
    [Serializable]
    public class EntityManager : Singleton
    {
        private List<Entity> entities = new List<Entity>();
        private GameManager manager;

        public override void Initialize(GameManager manager)
        {
            this.manager = manager;
        }

        public Entity AddEntityToPlayer()
        {
            Entity entity = AddEntity();
            manager.Deck.Add(entity);
            return entity;
        }

        public Entity AddEntity()
        {
            Entity entity = new Entity();
            entities.Add(entity);
            return entity;
        }

        public Entity GetEntity(Guid id)
        {
            for (int i = 0; i < entities.Count; i++)
            {
                if (entities[i].Info.Id != id)
                {
                    continue;
                }

                return entities[i];
            }
            return null;
        }

        public bool TryRemoveEntity(Guid id, out Entity entity)
        {
            for (int i = 0; i < entities.Count; i++)
            {
                if (entities[i].Info.Id != id)
                {
                    continue;
                }

                entity = entities[i];
                entities.RemoveAt(i);
                return true;
            }

            entity = null;
            return false;
        }
    }
}
