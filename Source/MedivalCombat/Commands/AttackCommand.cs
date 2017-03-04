using MedivalCombat.API;

namespace MedivalCombat.Commands
{
    public class AttackCommand
    {
        private readonly IEntity attacker;
        private readonly IEntity reciever;
        private readonly int damage;

        public AttackCommand(IEntity attacker, IEntity reciever, int damage)
        {
            this.attacker = attacker;
            this.reciever = reciever;
            this.damage = damage;
        }

        public void Execute()
        {
            reciever.GetComponent<IHealth>().DealDamage(damage);
        }
    }
}