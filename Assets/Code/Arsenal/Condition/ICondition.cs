namespace Code.Arsenal.Condition
{
    public interface ICondition
    {
        bool Eval();
    }

    public interface IParamCondition<in T>
    {
        bool Eval(T param);
    }
}