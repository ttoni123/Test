namespace SftpXmlTask.SftpService
{
    public class SftpConfiguration
    {
        public string UserName
        {
            get; set;
        } = null!;

        public string Password
        {
            get; set;
        } = null!;

        public string FilePath
        {
            get; set;
        } = null!;

        public string Host
        {
            get; set;
        } = null!;
    }
}
