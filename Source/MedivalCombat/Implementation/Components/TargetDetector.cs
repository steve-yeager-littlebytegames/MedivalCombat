﻿using System;
using MedivalCombat.API;
using MedivalCombat.Global;

namespace MedivalCombat.Implementation.Components
{
    public class TargetDetector : Component, ITargetDetector
    {
        public TargetDetector(IEntity owner)
            : base(owner)
        {
        }

        public uint Target { get; private set; }
        public UnitTypes TargetTypes { get; set; }
        public int Range { get; set; }

        public void GetTarget()
        {
            Target = NoId;

            int smallestDistance = int.MaxValue;
            for(int i = 0; i < Game.entities.Count; i++)
            {
                if(Owner.PlayerNumber == Game.entities[i].PlayerNumber)
                {
                    continue;
                }

                if((TargetTypes & Game.entities[i].UnitType) != Game.entities[i].UnitType)
                {
                    continue;
                }

                int x = Math.Abs(Game.entities[i].PositionX - Owner.PositionX);
                int y = Math.Abs(Game.entities[i].PositionY - Owner.PositionY);
                int distance = x + y;
                if(distance < smallestDistance)
                {
                    smallestDistance = distance;
                    Target = Game.entities[i].Id;
                }
            }
        }

        public bool IsInRange()
        {
            if(Target == NoId)
            {
                return false;
            }

            IEntity target = (IEntity)GetById(Target);

            int x = Math.Abs(target.PositionX - Owner.PositionX);
            int y = Math.Abs(target.PositionY - Owner.PositionY);
            int distance = x + y;
            return distance <= Range;
        }

        public override void Save(ISnapshot snapshot)
        {
            snapshot.Add("Target", Target);
            snapshot.Add("TargetTypes", TargetTypes);
            snapshot.Add("Range", Range);
        }

        public override void Load(ISnapshot save)
        {
            base.Load(save);
            Target = (uint)save.GetInt("Target");
            TargetTypes = (UnitTypes)save.GetInt("TargetTypes");
            Range = save.GetInt("Range");
        }
    }
}