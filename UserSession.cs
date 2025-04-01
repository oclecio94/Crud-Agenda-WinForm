namespace Crud_Agenda_WinForm
{
    public static class UserSession
    {
        public static int UserId { get; set; }
        public static string Nome { get; set; }
        public static string Email { get; set; }
        public static bool IsLoggedIn => UserId > 0;

        public static void Logout()
        {
            UserId = 0;
            Nome = string.Empty;
            Email = string.Empty;
        }
    }
}
