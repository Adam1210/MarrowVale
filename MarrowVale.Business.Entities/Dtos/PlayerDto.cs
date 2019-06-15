using MarrowVale.Business.Entities.Enums;

namespace MarrowVale.Business.Entities.Dtos
{
    public class PlayerDto
    {
        public string Name { get; set; }
        public ClassEnum Class { get; set; }
        public PlayerRaceEnum Race { get; set; }
        public string Gender { get; set; }

        public PlayerDto() { }
    }
}
