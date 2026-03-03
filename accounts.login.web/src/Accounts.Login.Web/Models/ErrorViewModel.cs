namespace Accounts.Login.Web.Models;

public class ErrorViewModel
{
    public string? RequestId { get; set; }
    public string Title { get; set; } = "Algo deu errado";
    public string Message { get; set; } = "Não foi possível concluir sua solicitação agora.";
    public string ReturnUrl { get; set; } = "/";
    public string ReturnLabel { get; set; } = "Voltar";

    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
}
