using System;
using System.Collections.Generic;
using MedivalCombat.API;
using MedivalCombat.API.Components;
using MedivalCombat.Commands;
using MedivalCombat.General;
using MedivalCombat.Implementation;
using MedivalCombat.Implementation.Components;

namespace MedivalCombat.Global
{
    public static class Game
    {
        public static event Action UpdateEvent = () => { };

        private const float FrameTime = 1f;

        public static readonly List<IEntity> entities = new List<IEntity>();
        private static Queue<CreateUnitCommand> creationCommands = new Queue<CreateUnitCommand>();
        private static readonly Queue<CreateUnitCommand> playedCommands = new Queue<CreateUnitCommand>();
        private static bool isPlaying;
        private static bool isPaused;

        public static int FrameCount { get; private set; }

        public static void Start()
        {
            FrameCount = 0;
            Entity.ResetCount();

            isPaused = false;
            isPlaying = true;
            MainLoop();
        }

        public static void Replay(Replay replay)
        {
            creationCommands = replay.frames;
            Start();
        }

        public static void End()
        {
            isPlaying = false;
            creationCommands.Clear();
            entities.Clear();
        }

        public static void Pause(bool pause)
        {
            isPaused = pause;
        }

        public static Replay CreateReplay()
        {
            return new Replay
            {
                frames = playedCommands
            };
        }

        public static void SpawnUnit(int unitId, int playerNumber, int x, int y)
        {
            CreateUnitCommand command = new CreateUnitCommand
            {
                playerNumber = playerNumber,
                positionX = x,
                positionY = y,
                unitId = unitId,
                frame = FrameCount + 1,
            };
            creationCommands.Enqueue(command);
        }

        private static void MainLoop()
        {
            DateTime lastFrame = DateTime.Now;

            while(isPlaying)
            {
                if(isPaused)
                {
                    continue;
                }

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

            SpawnUnits();
            Logic();
            //Combat();
            Physics();

            UpdateEvent();
        }

        private static void SpawnUnits()
        {
            while(creationCommands.Count > 0)
            {
                if(creationCommands.Peek().frame > FrameCount)
                {
                    break;
                }

                var command = creationCommands.Dequeue();
                playedCommands.Enqueue(command);
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
                    if(targetDetector.Target == Entity.NoId)
                    {
                        targetDetector.GetTarget();
                    }
                }
            }
        }

        private static void Combat()
        {
            List<AttackCommand> attacks = new List<AttackCommand>();

            for(int i = 0; i < entities.Count; i++)
            {
                //entities[i].CombatUpdate();
                ITargetDetector targetDetector = entities[i].GetComponent<ITargetDetector>();
                if(targetDetector != null)
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
            for(int i = 0; i < entities.Count; i++)
            {
                //entities[i].PhysicsUpdate();
                IMoveComponent mover = entities[i].GetComponent<IMoveComponent>();
                if(mover != null)
                {
                    ITargetDetector targetDetector = entities[i].GetComponent<ITargetDetector>();
                    if(targetDetector.Target == Entity.NoId)
                    {
                        continue;
                    }

                    IEntity target = Entity.GetById(targetDetector.Target);
                    mover.MoveTowards(target.PositionX, target.PositionY);
                }
            }

            // TODO: Resolve physics.
        }
    }
}