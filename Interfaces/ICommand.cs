namespace FileTransfer.Interfaces
{
    public interface ICommand
    {
        void Execute(string[] args);
    }
}