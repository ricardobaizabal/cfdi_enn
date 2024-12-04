
Partial Class portalcfd_Home
    Inherits System.Web.UI.Page

#Region "Load Initial Values"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Title = Resources.Resource.WindowsTitle
        '
        '   Define permisos
        '
        If System.Configuration.ConfigurationManager.AppSettings("usuarios") = 1 And Session("admin") = 0 Then
            If Session("perfilid") <> 1 Then
                lnk4.Enabled = False
                lnk4.ToolTip = "Acceso restringido."
                lnk5.Enabled = False
                lnk5.ToolTip = "Acceso restringido."
                lnk6.Enabled = False
                lnk6.ToolTip = "Acceso restringido."
                lnk9.Enabled = False
                lnk9.ToolTip = "Acceso restringido."
            End If
        End If
        '
        '   Define estados alternos para iconos
        '
        lnk1.Attributes.Add("onmouseover", "this.src='../images/inicio/clientes2.jpg'")
        lnk1.Attributes.Add("onmouseout", "this.src='../images/inicio/clientes.jpg'")
        '
        lnk2.Attributes.Add("onmouseover", "this.src='../images/inicio/productos2.jpg'")
        lnk2.Attributes.Add("onmouseout", "this.src='../images/inicio/productos.jpg'")
        '
        lnk3.Attributes.Add("onmouseover", "this.src='../images/inicio/comprobantes2.jpg'")
        lnk3.Attributes.Add("onmouseout", "this.src='../images/inicio/comprobantes.jpg'")
        '
        lnk4.Attributes.Add("onmouseover", "this.src='../images/inicio/folios2.jpg'")
        lnk4.Attributes.Add("onmouseout", "this.src='../images/inicio/folios.jpg'")
        '
        lnk5.Attributes.Add("onmouseover", "this.src='../images/inicio/reportes2.jpg'")
        lnk5.Attributes.Add("onmouseout", "this.src='../images/inicio/reportes.jpg'")
        '
        lnk6.Attributes.Add("onmouseover", "this.src='../images/inicio/certificados2.jpg'")
        lnk6.Attributes.Add("onmouseout", "this.src='../images/inicio/certificados.jpg'")
        '
        lnk8.Attributes.Add("onmouseover", "this.src='../images/inicio/inventario2.jpg'")
        lnk8.Attributes.Add("onmouseout", "this.src='../images/inicio/inventario.jpg'")
        '
        lnk9.Attributes.Add("onmouseover", "this.src='../images/inicio/proveedores2.jpg'")
        lnk9.Attributes.Add("onmouseout", "this.src='../images/inicio/proveedores.jpg'")
        '
        lnk10.Attributes.Add("onmouseover", "this.src='../images/inicio/empresas2.jpg'")
        lnk10.Attributes.Add("onmouseout", "this.src='../images/inicio/empresas.jpg'")
        '
        lnk11.Attributes.Add("onmouseover", "this.src='../images/inicio/salir2.jpg'")
        lnk11.Attributes.Add("onmouseout", "this.src='../images/inicio/salir.jpg'")
        '
    End Sub

#End Region

End Class
