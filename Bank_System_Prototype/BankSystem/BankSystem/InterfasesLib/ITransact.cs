namespace InterfasesLib
{
    public interface ITransact
    {
        void Transact<T1>(T1 client1, T1 client2, decimal sum)
            where T1 : IAccount;
    }
}