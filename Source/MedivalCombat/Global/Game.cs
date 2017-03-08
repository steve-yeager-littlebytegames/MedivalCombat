using System;
using System.Collections.Generic;
using MedivalCombat.API;
using MedivalCombat.API.Components;
using MedivalCombat.Commands;
using MedivalCombat.General;
using MedivalCombat.Implementation;
using MedivalCombat.Implementation.Components;
using Newtonsoft.Json;
using Object = MedivalCombat.API.Object;

namespace MedivalCombat.Global
{
    public static class Game
    {
        public static event Action UpdateEvent = () => { };

        private const float FrameTime = 1f;

        public static readonly List<IEntity> entities = new List<IEntity>();
        private static Queue<CreateUnitCommand> creationCommands = new Queue<CreateUnitCommand>();
        private static readonly Queue<CreateUnitCommand> playedCommands = new Queue<CreateUnitCommand>();
        private static Dictionary<int, List<Snapshot>> allSnapshots = new Dictionary<int, List<Snapshot>>();
        private static bool isPlaying;
        private static bool isPaused;
        private static bool isPlayingSnapshot;

        public static int FrameCount { get; private set; }

        public static void Start()
        {
            FrameCount = 0;
            Object.Reset();

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

        public static string CreateSnapshot()
        {
            return JsonConvert.SerializeObject(allSnapshots);
        }

        public static void PlaySnapshot(string snapshotData)
        {
            isPlayingSnapshot = true;
            allSnapshots = JsonConvert.DeserializeObject<Dictionary<int, List<Snapshot>>>(snapshotData);
            Start();
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

            if(isPlayingSnapshot)
            {
                List<Snapshot> snapshots;
                if(allSnapshots.TryGetValue(FrameCount, out snapshots))
                {
                    foreach(var snapshot in snapshots)
                    {
                        Object obj = Object.GetById(snapshot.ObjectId);
                        if(obj != null)
                        {
                            obj.Load(snapshot);
                        }
                        else
                        {
                            int playerNumber = snapshot.Get<int>("PlayerNumber");
                            var unit = UnitFactory.Create(0, playerNumber);
                            entities.Add(unit);
                            unit.Load(snapshot);
                        }
                    }
                }
            }
            else
            {
                SpawnUnits();
                Logic();
                //Combat();
                Physics();

                var snapshots = TakeSnapShot();
                allSnapshots.Add(FrameCount, snapshots);
            }

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
                    if(targetDetector.Target == Object.NoId)
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
                    if(targetDetector.Target == Object.NoId)
                    {
                        continue;
                    }

                    IEntity target = (IEntity)Object.GetById(targetDetector.Target);
                    mover.MoveTowards(target.PositionX, target.PositionY);
                }
            }

            // TODO: Resolve physics.
        }

        private static List<Snapshot> TakeSnapShot()
        {
            List<Snapshot> snapshots = new List<Snapshot>(Object.allObjects.Count);
            foreach(var obj in Object.allObjects)
            {
                Snapshot snapshot = (Snapshot)obj.Save();
                snapshots.Add(snapshot);
            }

            return snapshots;
        }
    }
}