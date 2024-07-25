public class Program
{
    public class Player
    {
        private Armor curArmor;

        public void Equip(Armor armor)
        {
            Console.WriteLine($"플레이어가 {armor.name} 을/를 착용합니다.");
            curArmor = armor;
            curArmor.OnBreaked += UnEquip;
        }

        public void UnEquip()
        {
            Console.WriteLine($"플레이어가 {curArmor.name} 을/를 해제합니다.");
            curArmor.OnBreaked -= UnEquip; // 방어구를 해제했을때 이벤트에 UnEquip을 제거하지 않는 경우,
            //다른 사유로(여기서 더 추가할때) 방어구가 깨질때에 플레이어가 벗으려고 시도하는 상황이 벌어진다.
            //필요 없을때 해제해야 메모리 낭비도 줄일 수 있다
            curArmor = null;

        }

        public void Hit()
        {
            // Armor armor = curArmor;
            Console.WriteLine("맞았습니다.");
            curArmor.DecreaseDurability();
            //Console.WriteLine($"{curArmor.durability}");
           // curArmor.OnBreaked += UnEquip; // 없는데 여러번 벗고있음


        }
    }

    public class Armor
    {
        public string name;        
        private int durability;

        public event Action OnBreaked;

        public Armor(string name, int durability)
        {
            this.name = name;
            this.durability = durability;
        }

        public void DecreaseDurability()
        {
            durability--;
            if (durability <= 0)
            {
                Break();
            }
        }

        private void Break()
        {
            Console.WriteLine("방어구 부서짐");
            if(OnBreaked != null)
            {
                OnBreaked();
            }
           
        }
    }

    static void Main(string[] args)
    {
        Player player = new Player();
        Armor ammor = new Armor("갑옷", 3);

        player.Equip(ammor);

        player.Hit();
        player.Hit();
        player.Hit();
    }
}