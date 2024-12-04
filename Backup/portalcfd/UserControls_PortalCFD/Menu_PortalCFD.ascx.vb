Imports System.Data
Imports System.Data.SqlClient
Imports System.Threading
Imports System.Globalization
Partial Class portalcfd_usercontrols_portalcfd_Menu_PortalCFD
    Inherits System.Web.UI.UserControl
    
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If System.Configuration.ConfigurationManager.AppSettings("usuarios") = 1 And Session("admin") = 0 Then
            lblUsuario.Text = "Usuario en sesión: <strong>" & Session("nombre").ToString & "</strong>"
            lblCertificado.Text = "Certificado Expira el: <strong>" & Session("fechaExpiracionCer").ToString & "</strong>"

            If CDate(Session("fechaExpiracionCer")) <= CDate(Date.Today.AddDays(-30).ToString("dd/MM/yyyy")) Then
                lblCertificado.ForeColor = Drawing.Color.Red
            Else
                lblCertificado.ForeColor = Drawing.Color.Green
            End If
            '
            '   Permisos para el menu
            '
            If Session("perfilid") <> 1 Then
                RadMenu1.Items(3).Enabled = False
                'RadMenu1.Items(5).Enabled = False
                RadMenu1.Items(6).Enabled = False
                '
                RadMenu1.Items(3).ToolTip = "Acceso restringido."
                'RadMenu1.Items(5).ToolTip = "Acceso restringido."
                RadMenu1.Items(6).ToolTip = "Acceso restringido."
            End If
        End If
        '
        '   Permisos por módulos contratados
        '
        If System.Configuration.ConfigurationManager.AppSettings("inventarios") = 0 Then
            RadMenu1.Items(3).Text = "Mis Productos"
            RadMenu1.Items(3).NavigateUrl = "~/portalcfd/productos.aspx"
            RadMenu1.Items(3).Items(0).Visible = False
            RadMenu1.Items(3).Items(1).Visible = False
        End If
        '
    End Sub

    Protected Sub FoliosGrid_NeedDataSource(ByVal source As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles FoliosGrid.NeedDataSource
        Dim ObjData As New DataControl
        FoliosGrid.DataSource = ObjData.FillDataSet("exec pMisFolios @cmd=4")
        ObjData = Nothing
    End Sub

    Private Sub lnkConsultarFolios_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkConsultarFolios.Click
        RadWindow1.VisibleOnPageLoad = True
    End Sub

End Class