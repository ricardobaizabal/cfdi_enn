
Partial Class portalcfd_inventarios_Default
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If System.Configuration.ConfigurationManager.AppSettings("inventarios") = 0 Then
            Response.Redirect("~/portalcfd/inventarios/informacion.aspx")
        End If
        ''''''''''''''
        'Window Title'
        ''''''''''''''
        Me.Title = Resources.Resource.WindowsTitle
        '
    End Sub
End Class
