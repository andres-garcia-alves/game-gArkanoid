using System;
using System.Collections.Generic;
using System.Text;

using Garkanoid.Entities;

namespace MapEditor
{
    public class cTag
    {
        private cBrick.eBrickType m_eType = cBrick.eBrickType.Normal;
        private cReward.eRewardType m_eReward = cReward.eRewardType.None;

        public cTag(cBrick.eBrickType eType, cReward.eRewardType eReward)
        {
            this.m_eType = eType;
            this.m_eReward = eReward;
        }

        public cBrick.eBrickType Type
        {
            get { return this.m_eType; }
        }

        public cReward.eRewardType Reward
        {
            get { return this.m_eReward; }
        }
    }
}
