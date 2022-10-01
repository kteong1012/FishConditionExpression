
namespace FishConditionExpression // Note: actual namespace depends on the project name.
{
    public abstract class ACondition
    {
        public abstract bool True();
    }
    public class Or : ACondition
    {
        public List<ACondition> Conditions { get; set; }

        public override bool True()
        {
            return Conditions.Any(x => x.True());
        }
    }
    public class And : ACondition
    {
        public List<ACondition> Conditions { get; set; }
        public override bool True()
        {
            return Conditions.All(x => x.True());
        }
    }
    public class RawCondition : ACondition
    {
        public string name;
        public override bool True()
        {
            return name == "a";
        }
    }
}