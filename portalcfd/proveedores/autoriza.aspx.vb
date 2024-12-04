Public Partial Class autoriza
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not (String.IsNullOrEmpty(Request("a")) And Not String.IsNullOrEmpty(Request("id"))) Then
                If Request("a") = 0 Then
                    lblMensaje.Text = "Orden de Trabajo No. " & Request("id").ToString & " Cancelada exitosamente."
                    lblMensaje.ForeColor = Drawing.Color.Red
                Else
                    lblMensaje.Text = "Orden de Trabajo No. " & Request("id").ToString & " Autorizada exitosamente."
                    lblMensaje.ForeColor = Drawing.Color.Green
                End If
            End If
        Catch ex As Exception
            Response.Redirect("~/portalcfd/Salir.aspx")
        End Try
    End Sub

End Class