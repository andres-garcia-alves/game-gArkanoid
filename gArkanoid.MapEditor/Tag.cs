using gArkanoid.Entities;

namespace MapEditor
{
    public class Tag
    {
        private Brick.eBrickType m_eType = Brick.eBrickType.Normal;
        private Reward.eRewardType m_eReward = gArkanoid.Entities.Reward.eRewardType.None;

        public Tag(Brick.eBrickType eType, Reward.eRewardType eReward)
        {
            this.m_eType = eType;
            this.m_eReward = eReward;
        }

        public Brick.eBrickType Type
        {
            get { return this.m_eType; }
        }

        public Reward.eRewardType Reward
        {
            get { return this.m_eReward; }
        }
    }
}
