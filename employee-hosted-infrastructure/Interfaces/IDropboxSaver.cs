namespace employee_hosted_infrastructure;

public interface IDropboxSaver
{
    Task<bool> SaveToDropbox(byte[] csvFileBytes);
}
