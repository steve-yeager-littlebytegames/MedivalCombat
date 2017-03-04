using System;
using System.Collections.Generic;
using MedivalCombat.API;
using MedivalCombat.API.Components;
using MedivalCombat.Commands;
using MedivalCombat.Implementation.Components;

namespace MedivalCombat.Global
{
    public static class Game
    {
        public static event Action UpdateEvent = () => { };

        private const float FrameTime = 1 / 60f;

        public static readonly List<IEntity> entities = new List<IEntity>();
        private static readonly Queue<CreateUnitCommand> creationCommands = new Queue<CreateUnitCommand>();

        public static int FrameCount { get; private set; }

        public static void Start(string unitDataPath)
        {
            IEntity unit = UnitFactory.Create(0, 0);
            entities.Add(unit);
            unit = UnitFactory.Create(0, 1);
            entities.Add(unit);

            MainLoop();
        }

        private static void MainLoop()
        {
            DateTime lastFrame = DateTime.Now;

            while(true)
            {
                DateTime current = DateTime.Now;
                if(current - lastFrame >= TimeSpan.FromSeconds(FrameTime))
                {
                    lastFrame = current;
                    PerformFrame();
                }
            }
        }

        private static void PerformFrame()
        {
            ++FrameCount;
            UpdateEvent();

            SpawnUnits();
            Logic();
            //Combat();
            Physics();
        }

        private static void SpawnUnits()
        {
            while(creationCommands.Count > 0)
            {
                var command = creationCommands.Dequeue();
                var unit = UnitFactory.Create(command.unitId, command.playerNumber);
                unit.PositionX = command.positionX;
                unit.PositionY = command.positionY;
                entities.Add(unit);
            }
        }

        private static void Logic()
        {
            for(int i = 0; i < entities.Count; i++)
            {
                //entities[i].LogicUpdate();
                ITargetDetector targetDetector = entities[i].GetComponent<ITargetDetector>();
                if(targetDetector != null)
                {
                    if(targetDetector.Target == null)
                    {
                        targetDetector.GetTarget();
                    }
                }
            }
        }

        private static void Combat()
        {
            List<AttackCommand> attacks = new List<AttackCommand>();

            for (int i = 0; i < entities.Count; i++)
            {
                //entities[i].CombatUpdate();
                ITargetDetector targetDetector = entities[i].GetComponent<ITargetDetector>();
                if (targetDetector != null)
                {
                    if(targetDetector.IsInRange())
                    {
                        var hits = entities[i].GetComponent<IAttackComponent>().Attack();
                        if(hits != null)
                        {
                            attacks.AddRange(hits);
                        }
                    }
                }
            }

            // TODO: Resolve combat.
        }

        private static void Physics()
        {
            for (int i = 0; i < entities.Count; i++)
            {
                //entities[i].PhysicsUpdate();
                IMoveComponent mover = entities[i].GetComponent<IMoveComponent>();
                if(mover != null)
                {
                    ITargetDetector targetDetector = entities[i].GetComponent<ITargetDetector>();
                    mover.MoveTowards(targetDetector.Target.PositionX, targetDetector.Target.PositionY);
                }
            }

            // TODO: Resolve physics.
        }

    }
}