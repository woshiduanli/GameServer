
/// <summary>
/// 技能等级扩展类
/// </summary>
public partial class SkillLevelDBModel
{
    /// <summary>
    /// 根据技能编号和技能等级扩展实体
    /// </summary>
    /// <param name="skillId"></param>
    /// <param name="skillLevel"></param>
    /// <returns></returns>
    public SkillLevelEntity GetEntityBySkillIdAndLevel(int skillId, int skillLevel)
    {
        for (int i = 0; i < m_List.Count; i++)
        {
            if (m_List[i].SkillId == skillId && m_List[i].Level == skillLevel)
            {
                return m_List[i];
            }
        }
        return null;
    }
}