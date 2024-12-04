
Partial Class portalcfd_formatos_ImprimeFactura
    Inherits System.Web.UI.Page

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        Dim reporte As New Formatos.formato_cfdi
        reporte.ReportParameters(0).Value = Request("id").ToString
        '
        '
        Me.ReportViewer1.Report = reporte
        Me.ReportViewer1.RefreshReport()
    End Sub
End Class
